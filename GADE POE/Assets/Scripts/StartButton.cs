using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] Slider widthSlider;
    [SerializeField] Slider lengthSlider;
    [SerializeField] Slider unitSlider;
    [SerializeField] Slider buildingSlider;

    public void StartClicked()
    {
        Debug.Log("Start Clicked");
        MapSizeManager.mapX = (int)widthSlider.value;
        MapSizeManager.mapZ = (int)lengthSlider.value;
        RandomPopulation.numUnits = (int)(widthSlider.value * lengthSlider.value * unitSlider.value);
        RandomPopulation.numBuildings = (int)(widthSlider.value * lengthSlider.value * (1.0f - unitSlider.value) * buildingSlider.value);
        RandomPopulation.mapX = (int)widthSlider.value;
        RandomPopulation.mapZ = (int)lengthSlider.value;

        MapSizeManager.updateMap = true;
        RandomPopulation.updatePop = true;

        RoundCounter.round = 0;
        Building.redResources = 0;
        Building.blueResources = 0;
        RoundCounter.running = true;
    }
}
