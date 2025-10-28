using UnityEngine;
using UnityEngine.UI;

public class EnergyCounter : MonoBehaviour
{
    [SerializeField] public float water = 100f;
    [SerializeField] public float energy = 100f;
    [SerializeField]public float waterPerSec = 1f;
   [SerializeField] public float energyPerSec = 1f;

    public Slider waterSlider;
    public Slider energySlider;

    public Text waterText;
    public Text energyText;

    float timer;

    void Start()
    {
        if (waterSlider) waterSlider.maxValue = 100f;
        if (energySlider) energySlider.maxValue = 100f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0f;
            water -= waterPerSec;
            energy -= energyPerSec;
        }

        water = Mathf.Clamp(water, 0f, 100f);
        energy = Mathf.Clamp(energy, 0f, 100f);

        if (waterSlider) waterSlider.value = water;
        if (energySlider) energySlider.value = energy;

        if (waterText) waterText.text = "Water: " + Mathf.RoundToInt(water);
        if (energyText) energyText.text = "Energy: " + Mathf.RoundToInt(energy);
    }
}

