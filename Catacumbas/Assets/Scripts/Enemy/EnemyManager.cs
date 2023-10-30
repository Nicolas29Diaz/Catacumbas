using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class EnemyManager : MonoBehaviour
{
    [Header("Parámetros")]
    public float walkingSpeed = 2.0f; // Velocidad al caminar
    //public float runningSpeed = 5.0f; // Velocidad al correr
    public float acceleration = 8.0f; // Ajusta la aceleración
    private GameObject[] waypoints; // Lista de waypoints
    private int currentWaypointIndex = 0; // Índice del waypoint actual
    private NavMeshAgent navMeshAgent;
    private Animator anim;



    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("WayPoints");
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = walkingSpeed; // Inicialmente, establece la velocidad de caminar
        navMeshAgent.stoppingDistance = 1.0f; // Distancia de parada
        navMeshAgent.acceleration = acceleration;
    }
    float enemySpeed;
    private void Update()
    {
        // Obtén la velocidad actual del enemigo desde el NavMeshAgent
        Vector3 enemyVelocity = navMeshAgent.velocity;

        // Calcula la magnitud de la velocidad (la velocidad total)
        enemySpeed = enemyVelocity.magnitude;

        // Verifica si el NPC ha alcanzado el waypoint actual
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Avanza al siguiente waypoint
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Si llega al último waypoint, reinicia la ruta
            }
        }

        // Mueve el NPC hacia el waypoint actual
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        // Establece el destino del NavMeshAgent al waypoint actual
        anim.SetFloat("Speed", enemySpeed);
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].transform.position);
    }

    // Método para cambiar a caminar
    public void SetWalking()
    {
        navMeshAgent.speed = walkingSpeed;
    }

    // Método para cambiar a correr
    public void SetRunning()
    {
        //navMeshAgent.speed = runningSpeed;
    }

    // Método para detenerse
    public void SetIdle()
    {
        navMeshAgent.speed = 0.0f;
    }
}
