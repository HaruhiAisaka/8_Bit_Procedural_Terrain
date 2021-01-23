using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class SliderInputFieldPair : MonoBehaviour
{
    public Slider slider;
    public IntOnlyInputField intOnlyInputField;

    public float min;
    public float max;
    public bool wholeNumbers;
    public float value;

    void Start()
    {
        slider.minValue = min;
        slider.maxValue = max;
        intOnlyInputField.min = min;
        intOnlyInputField.max = max;
        slider.wholeNumbers = wholeNumbers;
        intOnlyInputField.wholeNumbers = wholeNumbers;
        intOnlyInputField.SetValue(value);
    }

    void OnValidate()
    {
        if (value < min)
        {
            value = min;
        }
        if (value > max)
        {
            value = max;
        }
    }

    void OnGUI()
    {
        float inputFieldCurrentValue = intOnlyInputField.value;
        float sliderCurrentValue = slider.value;
        if (value != inputFieldCurrentValue)
        {
            slider.value = inputFieldCurrentValue;
            value = inputFieldCurrentValue;
        }
        else if (value != sliderCurrentValue)
        {
            intOnlyInputField.SetValue(sliderCurrentValue);
            value = sliderCurrentValue;
        }
    }
}
