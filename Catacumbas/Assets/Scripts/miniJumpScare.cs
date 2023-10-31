using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniJumpScare : MonoBehaviour
{
    public GameObject imageObject; // Arrastra el objeto de imagen en el Inspector
    private AudioClip soundClip;    // Arrastra el clip de sonido en el Inspector
    public AudioSource audioSource;
    private float fadeDuration = 1.0f; // Duración de la transparencia
    public CharacterController JugadorController;
    public GameObject GameOver;
    Vector3 puntoInicio;
    void Start()
    {
        puntoInicio = gameObject.transform.position;
        soundClip = audioSource.GetComponent<AudioSource>().clip;
        audioSource.Stop();
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Asegúrate de etiquetar tu jugador con "Player"
        {
            gameObject.transform.position = puntoInicio;
            // Mostrar imagen
            imageObject.SetActive(true);
            // Bajar transparencia de la imagen
   
          

            // Play sonido
            audioSource.PlayOneShot(soundClip);

            // Esperar mientras el sonido termina de reproducirse
            JugadorController.enabled = false;
            
            StartCoroutine(ShowImageAndPlaySound());
        }
    }

    private IEnumerator ShowImageAndPlaySound()
    {
        
        yield return new WaitForSeconds(3.0f);
        GameOver.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        // Reiniciar escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
