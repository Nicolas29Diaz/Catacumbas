using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseLogic : MonoBehaviour
{

    [SerializeField] private GameObject UIPausa;

    [SerializeField] private GameObject UIJugador;

    [SerializeField] private GameObject confirmation;

    [SerializeField] private GameObject settingsMenu;

    [SerializeField] public bool controladorPausa = true;

    public delegate void GamePauseAction(bool isPaused);

    public static event GamePauseAction OnGamePaused;
 


    void Start()
    {

        UIJugador = GameObject.Find("UIJugador");
        confirmation = GameObject.Find("Confirmation");
        settingsMenu = GameObject.Find("BackGroundSettingsMenu");

        UIPausa.SetActive(false);
        StartCoroutine(esperar());
        confirmation.SetActive(false);
        settingsMenu.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        PauseWithCancelButton();
    }

    void PauseWithCancelButton()
    {
        if (Input.GetButtonDown("Cancel") && controladorPausa)
        {
            UIPausa.SetActive(true);
            UIJugador.SetActive(false);
            
            controladorPausa = false;
            OnGamePaused?.Invoke(true);
            
            
        }
        else if (Input.GetButtonDown("Cancel") && !controladorPausa)
        {
            UIPausa.SetActive(false);
            confirmation.SetActive(false);
            settingsMenu.SetActive(false);
            controladorPausa = true;
            UIJugador.SetActive(true);
            
            OnGamePaused?.Invoke(false);
            
        }
    }

    public void continueGameWithButton()
    {
        UIPausa.SetActive(false);
        controladorPausa = true;
        UIJugador.SetActive(true);
        OnGamePaused?.Invoke(false);

    }


    public void SettingsButtonDisplay()
    {
        settingsMenu.SetActive(true);

    }

    public void SettingsButtonHide()
    {
        settingsMenu.SetActive(false);

    }

    public void MenuButtonConfirmation()
    {
        confirmation.SetActive(true);

    }

    public void MenuButtonNotConfirmation()
    {
        confirmation.SetActive(false);

    }


    IEnumerator esperar()
    {
        yield return new WaitForSeconds(0.1f);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }
}
