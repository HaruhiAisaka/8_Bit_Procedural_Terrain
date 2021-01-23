using UnityEngine;
using UnityEngine.UI;

public class NoiseDataUI : MonoBehaviour
{
    [Header("UI Components")]
    public TMPro.TMP_InputField seed;
    public Slider octaves;
    public Slider persistance;
    public Slider lacunarity;
    public Toggle island;

    public void UpdateUI(NoiseData noiseData)
    {
        
    }

    public void UpdateMapGenerator()
    {
        
    }
}