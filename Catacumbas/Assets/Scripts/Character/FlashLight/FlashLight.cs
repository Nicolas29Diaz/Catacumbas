using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashLight : MonoBehaviour
{
    [Header("References")]
    public Light flashLight;
    public BatteryUI batteryUI;
    private bool activLight;

    [Header("Battery")]
    public float initDurationBattery = 100;
    public float actualDurationBattery = 0f;
    public float countBattery = 3;
    public float dischargeVelocity = 1;
    private bool isChangingBattery;

    private bool isGamePaused = false;

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
    void Start()
    {
        activLight = false;
        actualDurationBattery = initDurationBattery;
        isChangingBattery = false;

        //UI
        Debug.Log(actualDurationBattery + "  " +  countBattery);
        batteryUI.InitBattery(actualDurationBattery,countBattery);


        //int valueBattery = (int)durationBattery;
        //percentage.text = valueBattery.ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused)
        {

            //actualDurationBattery = Mathf.Clamp(actualDurationBattery, 0, 100);
            flashLight.enabled = activLight;

            if (Input.GetKeyDown(KeyCode.F) && actualDurationBattery > 0 && countBattery >= 0 && !isChangingBattery)
            {
                activLight = !activLight;
                //Debug.Log("F");
            }
            ///*&& actualDurationBattery <= 0*/ acá es por su queremos que se pueda recargar así no se haya agotado la bateria
            else if (Input.GetKeyDown(KeyCode.R) /*&& actualDurationBattery <= 0*/ && countBattery > 0 && !isChangingBattery)
            {
                //Debug.Log("R");

                StartCoroutine(ChangeBatteryWithDelay());

            }

            if (actualDurationBattery <= 0)
            {
                activLight = false;

                if (countBattery <= 0)
                {
                    activLight = false;
                }
                //else if(!isChangingBattery)
                //{
                //    //Esperar.. Cambiar

                //}

            }
            else
            {

                ConsumeBattery();
            }

            IntensityLight();
            batteryUI.changeCountBattery(countBattery);
            batteryUI.ChangeActualBattery(actualDurationBattery);
        }

    }

    private void ConsumeBattery()
    {
        if (activLight && actualDurationBattery > 0)
        {
            actualDurationBattery -= dischargeVelocity * Time.deltaTime;
            //UI
            //batteryUI.ChangeActualBattery(actualDurationBattery);
        }
    }


    private IEnumerator ChangeBatteryWithDelay()
    {
        isChangingBattery = true;
        //activLight = false;

        yield return new WaitForSeconds(0.5f); // Espera 

        countBattery--;
      
        //Debug.Log("batteries: " + countBattery);

        actualDurationBattery = initDurationBattery;

        //activLight = true;
        isChangingBattery = false;
    }


    public void IntensityLight()
    {
        // Supongamos que el rango original de intensidad de luz es de 0 a 1 (puedes ajustar esto según tu configuración).
        float minIntensity = 0.5f;
        float maxIntensity = 3.0f;

        // Normaliza la duración de la batería entre 0 y 1.
        float normalizedBatteryDuration = actualDurationBattery / initDurationBattery;

        // Calcula la intensidad de luz proporcional a la duración de la batería.
        float normalizedIntensity = Mathf.Lerp(minIntensity, maxIntensity, normalizedBatteryDuration);

        // Asigna la intensidad normalizada a la luz de la linterna.
        flashLight.intensity = normalizedIntensity;


    }





}
