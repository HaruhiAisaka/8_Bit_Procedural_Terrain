using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class IntOnlyInputField : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;
    public float min;
    public float max;
    public bool wholeNumbers;

    public float value;

    public void SetValue(float value)
    {
        this.value = value;
        inputField.text = value.ToString();
    }

    void OnGUI()
    {
        if (wholeNumbers)
        {
            inputField.text = Regex.Replace(inputField.text, @"[^0-9]", "");
        }
        else 
        {
            String text = inputField.text;
            int index = text.IndexOf(".");
            if (index < 0)
            {
                inputField.text = Regex.Replace(inputField.text, @"[^0-9]", "");
            }
            else 
            {
                String beforeDecimal = inputField.text.Substring(0,index);
                Debug.Log(beforeDecimal);
                beforeDecimal = Regex.Replace(beforeDecimal, @"[^0-9]", "");
                String afterDecimal =inputField.text.Substring(index + 1, index + 2);
                Debug.Log(afterDecimal);
                afterDecimal = Regex.Replace(afterDecimal, @"[^0-9]", "");
                inputField.text = beforeDecimal + "." + afterDecimal;
            }
        }
        String valueString = inputField.text;
        if (valueString.Length != 0)
        {
            float value = float.Parse(valueString, System.Globalization.CultureInfo.InvariantCulture);
            value = ValidateInput(value);
            this.value = value;
        }
    }

    private float ValidateInput(float value)
    {
        if (value < min)
        {
            value = min;
        }
        if (value > max)
        {
            value = max;
        }
        return value;
    }
}
