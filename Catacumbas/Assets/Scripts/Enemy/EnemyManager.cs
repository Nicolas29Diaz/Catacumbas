using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class EnemyManager : MonoBehaviour
{
    [Header("Parámetros")]
    public float walkingSpeed = 2.0f; // Velocidad al caminar
    public GameObject[] waypoints; // Lista de waypoints
    public int currentWaypointIndex = 0; // Índice del waypoint actual
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private AudioSource AudSource;
    private FOV_AIManager OjosManager;
    private HearingLogic OidosManager;
    [Header("Sonidos")]
    public AudioClip[] AudiosEnemy;
    private float life = 80f;
    private bool isGamePaused = false;





    private void Start()
    {
        OjosManager = GetComponent<FOV_AIManager>();
        OidosManager = GetComponent<HearingLogic>();
        waypoints = GameObject.FindGameObjectsWithTag("WayPoints");
        //OrganizarWaypoints();
        AudSource = GetComponent<AudioSource>();
        AudSource.clip = AudiosEnemy[0];
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = walkingSpeed; // Inicialmente, establece la velocidad de caminar
        navMeshAgent.stoppingDistance = 1.0f; // Distancia de parada
    }
    float enemySpeed;

    private void OnEnable()
    {
        PauseLogic.OnGamePaused += HandleGamePause;
    }

    private void OnDisable()
    {
        PauseLogic.OnGamePaused -= HandleGamePause;
    }

    private void HandleGamePause(bool pauseStatus)
    {
        isGamePaused = pauseStatus;
    }
    private void Update()
    {
        if (!isGamePaused)
        {
            // Obtén la velocidad actual del enemigo desde el NavMeshAgent
            Vector3 enemyVelocity = navMeshAgent.velocity;
            // Calcula la magnitud de la velocidad (la velocidad total)
            enemySpeed = enemyVelocity.magnitude;

            SetState();
            //CalcularCercania();
            //Patrol();
        }


        //CheckPlace();
        //Patrol();



    }


    bool PlaceChecked = false;
    bool isMovible = true;
    void SetState()
    {
            if (OjosManager.isLookingPlayer)
            {
                FollowPlayer();
                Debug.Log("siguinedo");
            }
            else if (OidosManager.ShotHeard && !OjosManager.isLookingPlayer )
            {
                CheckPlace(OidosManager.LastKnownPosition);
                Debug.Log("buscando");
            }
            else if (!OidosManager.ShotHeard && !OjosManager.isLookingPlayer)
            {
                CalcularCercania();
                Debug.Log("Patrol");
            } 
    }
    void FollowPlayer()
    {
        Vector3 playerPosition = OjosManager.LastKnownPosition;
        navMeshAgent.speed = walkingSpeed * 1.5f;
        float distancia = Vector3.Distance(transform.position, playerPosition); 
        if (distancia <= 5f)
        {
            Debug.Log("Attacking");
            //navMeshAgent.speed = 0;
            Attack();
        }
        else
        {
            MoveToWaypoint(playerPosition);
        }
        
    }

    void CalcularCercania()
    {
        Transform playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        float distancia = Vector3.Distance(transform.position, playerPosition.position);
        bool isFarAway = distancia > 50f;


        if (isFarAway)
        {
            Transform waypointCercano = null;
            float distanciaMinima = Mathf.Infinity;

            // Recorre todos los waypoints disponibles
            foreach (GameObject waypointObj in waypoints)
            {
                Transform waypointTransform = waypointObj.transform;
                float distanciaWaypoint = Vector3.Distance(playerPosition.position, waypointTransform.position);

                // Comprueba si esta es la distancia más cercana encontrada hasta ahora
                if (distanciaWaypoint < distanciaMinima)
                {
                    distanciaMinima = distanciaWaypoint;
                    waypointCercano = waypointTransform;
                }
            }// Verifica si el NPC ha alcanzado el waypoint actual
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                transform.position = waypointCercano.position;
                OrganizarWaypoints();
                //MoveToWaypoint(waypointCercano);
            }   
        }
        else
        {
            Patrol();
        }
    }
    // Método para cambiar a caminar
    void Patrol()
    {
        // Verifica si el NPC ha alcanzado el waypoint actual
        if (waypoints[currentWaypointIndex].GetComponent<WaypointsLogic>().isArrived)
        {
            // Avanza al siguiente waypoint
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Si llega al último waypoint, reinicia la ruta
            }
        }
        navMeshAgent.speed = walkingSpeed;
        // Mueve el NPC hacia el waypoint actual
        MoveToWaypoint(waypoints[currentWaypointIndex].transform.position);
    }
    public bool isCheck = false;
    void CheckPlace(Vector3 playerPosition)
    {
            MoveToWaypoint(playerPosition);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                OidosManager.ShotHeard = false;
                Debug.Log("revisado");
            }
    }
    void MoveToWaypoint(Vector3 t)
    {
        anim.SetFloat("Speed", enemySpeed);
        navMeshAgent.SetDestination(t);

    }
    void OrganizarWaypoints()
    {
        // Obtiene la posición actual del objeto
        Vector3 posicionActual = transform.position;

        // Ordena el arreglo de waypoints en función de la distancia desde la posición actual
        System.Array.Sort(waypoints, (waypointA, waypointB) =>
        {
            float distanciaA = Vector3.Distance(posicionActual, waypointA.transform.position);
            float distanciaB = Vector3.Distance(posicionActual, waypointB.transform.position);
            //return distanciaB.CompareTo(distanciaA); // Compara en orden descendente
            return distanciaA.CompareTo(distanciaB); // Compara en orden ascendente

        });
    }

    //animaciones
    public void GetShoot(float Damage)
    {
        life -= Damage;
        anim.SetBool("isShoot", true);
        Invoke("ResetearVariableDeDisparo", 2);
        isMovible = false;

    }
    void Attack()
    {
        anim.SetBool("isAttacking", true);
        Invoke("ResetearVariableDeAtaque", 1);
    }
    void Dead()
    {
        navMeshAgent.speed = 0;
        AudSource.clip = AudiosEnemy[4];
        anim.SetBool("isDead", true);
        Invoke("ResetearVariableDeMuerte", 1);
    }
    void Scream()
    {
        navMeshAgent.speed = 0;
        AudSource.clip = AudiosEnemy[3];
        anim.SetBool("isRoar", true);
        Invoke("ResetearVariableDeGrito", 1);
    }

    private void ResetearVariableDeDisparo()
    {
        anim.SetBool("isShoot", false);
        navMeshAgent.speed = walkingSpeed;
        isMovible = true;
    }
    private void ResetearVariableDeMuerte()
    {
        anim.SetBool("isDead", false);
        navMeshAgent.speed = walkingSpeed;
    }
    private void ResetearVariableDeAtaque()
    {
        anim.SetBool("isAttacking", false);
        navMeshAgent.speed = walkingSpeed;
    }
    private void ResetearVariableDeGrito()
    {
        anim.SetBool("isRoar", false);
        navMeshAgent.speed = walkingSpeed;
    }


}
