using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashLight : MonoBehaviour
{
    [Header("References")]
    public Light flashLight;

    public bool activLight;

    [Header("Battery")]
    public float initDurationBattery = 100;
    public float actualDurationBattery;
    public float countBattery = 3;
    public float dischargeVelocity = 1;
    private bool isChangingBattery;

    [Header("UI")]
    public Image battery1;
    public Image battery2;
    public Image battery3;
    public Image battery4;
    public Sprite fullBattery;
    public Sprite emptyBattery;
    public Text percentage;


    void Start()
    {
        activLight = false;
        actualDurationBattery = initDurationBattery;
        isChangingBattery = false;

        //int valueBattery = (int)durationBattery;
        //percentage.text = valueBattery.ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {
        //actualDurationBattery = Mathf.Clamp(actualDurationBattery, 0, 100);
        flashLight.enabled = activLight;

        if (Input.GetKeyDown(KeyCode.F) && actualDurationBattery > 0 && countBattery >= 0 && !isChangingBattery)
        {
            activLight = !activLight;
        }

        if (actualDurationBattery <= 0)
        {
            if (countBattery <= 0)
            {
                activLight = false;
            }
            else if(!isChangingBattery)
            {
                //Esperar.. Cambiar
                StartCoroutine(ChangeBatteryWithDelay());
            }

        }
        else
        {
            ConsumeBattery();
        }

        IntensityLight();
    }

    private void ConsumeBattery()
    {
        if (activLight && actualDurationBattery > 0)
        {
            actualDurationBattery -= dischargeVelocity * Time.deltaTime;
        }
    }


    private IEnumerator ChangeBatteryWithDelay()
    {
        isChangingBattery = true;
        activLight = false;

        yield return new WaitForSeconds(0.5f); // Espera 1 segundo

        countBattery--;
        Debug.Log("batteries: " + countBattery);

        actualDurationBattery = initDurationBattery;

        activLight = true;
        isChangingBattery = false;
    }


    public void IntensityLight()
    {
        // Supongamos que el rango original de intensidad de luz es de 0 a 1 (puedes ajustar esto según tu configuración).
        float minIntensity = 0.0f;
        float maxIntensity = 3.0f;

        // Normaliza la duración de la batería entre 0 y 1.
        float normalizedBatteryDuration = (actualDurationBattery - 0.0f) / initDurationBattery;

        // Calcula la intensidad de luz proporcional a la duración de la batería.
        float normalizedIntensity = Mathf.Lerp(minIntensity, maxIntensity, normalizedBatteryDuration);

        // Asigna la intensidad normalizada a la luz de la linterna.
        flashLight.intensity = normalizedIntensity;


    }



}
