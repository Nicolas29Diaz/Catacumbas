using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent (typeof(Animator))]
public class ButtonControls : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("El sonido que se reproducir� cuando se haga clic en el bot�n.")]
    public AudioSource buttonAudioSource;

    [Header("Controlador De Menu principal")]
    [Tooltip("A�adir el menu principal")]
    public GameObject mainMenuUI;

    [Header("Controlador De Menu Settings")]
    [Tooltip("A�adir el menu Settings")]
    public GameObject settingsMenuUI;

    [Header("Controlador De Menu Credits")]
    [Tooltip("A�adir el menu Credits")]
    public GameObject creditsMenuUi;

    public void Start()
    {
        buttonAudioSource = GetComponent<AudioSource>();
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        creditsMenuUi.SetActive(false);

    }

    public void Awake()
    {
        mainMenuUI = GameObject.Find("BackGroundMainMenu");
        settingsMenuUI = GameObject.Find("BackGroundSettingsMenu");
        creditsMenuUi = GameObject.Find("CreditsMenu");
        
    }
    public void HoverSound()
    {
        buttonAudioSource.Play();
    }

    public void changeUi(int proximaEscena)
    {
        switch (proximaEscena)
        {
            case 0:
                settingsMenuUI.SetActive(false);
                creditsMenuUi.SetActive(false);
                mainMenuUI.SetActive(true);
            break;
                
            case 1:
                settingsMenuUI.SetActive(true);
                mainMenuUI.SetActive(false);
            break;
            case 2:
                creditsMenuUi.SetActive(true);
                mainMenuUI.SetActive(false);
            break;

            default:
            Debug.Log("Valor no v�lido");
            break;
        }
    }
}
