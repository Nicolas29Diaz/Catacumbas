using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniJumpScare : MonoBehaviour
{
    public GameObject imageObject; // Arrastra el objeto de imagen en el Inspector
    public AudioClip soundClip;    // Arrastra el clip de sonido en el Inspector
    private AudioSource audioSource;
    private float fadeDuration = 1.0f; // Duración de la transparencia
    Vector3 puntoInicio;
    void Start()
    {
        puntoInicio = gameObject.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de etiquetar tu jugador con "Player"
        {
            gameObject.transform.position = puntoInicio;
            StartCoroutine(ShowImageAndPlaySound());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de etiquetar tu jugador con "Player"
        {
            gameObject.transform.position = puntoInicio;
            StartCoroutine(ShowImageAndPlaySound());
        }
    }

    private IEnumerator ShowImageAndPlaySound()
    {
        // Mostrar imagen
        imageObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        // Bajar transparencia de la imagen
        float alphaValue = 1.0f;
        while (alphaValue > 0)
        {
            alphaValue -= Time.deltaTime / fadeDuration;
            Color tempColor = imageObject.GetComponent<SpriteRenderer>().color;
            tempColor.a = alphaValue;
            imageObject.GetComponent<SpriteRenderer>().color = tempColor;
            yield return null;
        }

        // Play sonido
        audioSource.PlayOneShot(soundClip);

        // Esperar mientras el sonido termina de reproducirse
        yield return new WaitForSeconds(audioSource.clip.length);

        // Reiniciar escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
