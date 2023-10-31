using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public InventorySystem InventorySystem;

    [Header("UIElements")]
    public Image velas, biblia, cruz, corazon;

    [Header("SpritesQueCambiaran")]
    // Referencias a los sprites
    public Sprite spriteVelas, spriteBiblia, spriteCruz, spriteCorazon;

    private bool controladorInventario;

    public GameObject UIInventario;

    public AudioClip EfectoSonidoRecoleccion;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    public GameObject ritualGameObject;

    private int controladorRitual;

    public GameObject mensajeParaElAdmin;

    public void OnTriggerEnter(Collider other)
    {
       var item = other.GetComponent<RitualItem>();
        if (item != null)
        {
            InventorySystem.AddItem(item.ItemObject);
            AudioSource[] audioSources = this.GetComponents<AudioSource>();
            audioSources[0].PlayOneShot(EfectoSonidoRecoleccion);
            UIObjectsUpdate(item);
            Destroy(other.gameObject);
            if(InventorySystem.container.Count >= 4){
                ritualGameObject.SetActive(true);
                mensajeParaElAdmin.SetActive(true);
                Invoke("desactivarMensaje", 3f);
            }
        
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            controladorInventario ^= true;
            if(controladorInventario == true)
            {
                UIInventario.SetActive(true);
            }
            else
            {
                UIInventario.SetActive(false);
            }
        }
    }

    public void UIObjectsUpdate(RitualItem item)
    {
        switch (item.ItemObject.itemType)
        {
            case ItemType.velas:
                velas.sprite = spriteVelas;
                break;
            case ItemType.biblia:
                biblia.sprite = spriteBiblia;
                break;
            case ItemType.crucifijo:
                cruz.sprite = spriteCruz;
                break;
            case ItemType.corazon:
                corazon.sprite = spriteCorazon;
                break;
            default:
                break;
        }
    }

    public void OnApplicationQuit()
    {
        InventorySystem.container.Clear();
    }

    void desactivarMensaje()
    {
        mensajeParaElAdmin.SetActive(false);
    }


}
