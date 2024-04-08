using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleplayerMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI SensValueText;
    [SerializeField]
    private Slider SensSlider;

    // Start is called before the first frame update
    void Start()
    {
        SensSlider.maxValue = 10f;

        SensSlider.minValue = 0.1f;

        SensSlider.value = SETTINGS.SENSITIVITY * 100f;
    }

    // Update is called once per frame
    void Update()
    {
        float sensValue = SETTINGS.SENSITIVITY * 100f;

        SensValueText.text = sensValue.ToString("0.00");

        GameManager.instance.setSens(SensSlider.value / 100);
    }
}
