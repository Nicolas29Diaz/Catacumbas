using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BatteryUI : MonoBehaviour
{
    private Slider batterySlider;
    public TextMeshProUGUI countText;
    private void Awake()
    {
        batterySlider = GetComponent<Slider>();
    }
  
    public void ChangeMaxBattery(float maxBattery)
    {
        batterySlider.maxValue = maxBattery;
    }

    public void ChangeActualBattery(float actualBattery)
    {
        batterySlider.value = actualBattery;
    }

    public void changeCountBattery(float countBattery)
    {
        countText.text = (countBattery).ToString();
    }
    
    public void InitBattery(float battery, float countBattery)
    {
        Debug.Log(battery + "  " + countBattery);
        ChangeMaxBattery(battery);
        ChangeActualBattery(battery);
        changeCountBattery(countBattery);
    }
}
