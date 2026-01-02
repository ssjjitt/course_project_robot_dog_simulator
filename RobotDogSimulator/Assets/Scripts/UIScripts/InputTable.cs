using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InputTable : MonoBehaviour
{
    [Header("Поля ввода для таблицы")]
    public List<TMP_InputField> tableInputFields;

    [Header("Поле ввода внизу")]
    public TMP_InputField bottomInputField;

    [Header("Текст для вывода среднего")]
    public TextMeshProUGUI averageText;

    private void Start()
    {
        averageText.text = "Среднее: 0.00";
    }

    private int currentIndex = 0;

    public void AddDataToTable()
    {
        string inputData = bottomInputField.text;
        if (string.IsNullOrEmpty(inputData)) return;

        tableInputFields[currentIndex].text = inputData;
        currentIndex = (currentIndex + 1) % tableInputFields.Count;
        bottomInputField.text = "";

        UpdateAverage();
    }

    public void ClearAllFields()
    {
        foreach (var input in tableInputFields)
        {
            input.text = "";
        }
        bottomInputField.text = "";
        averageText.text = "Среднее: 0.00";
        currentIndex = 0; 
    }
    private void UpdateAverage()
    {
        float sum = 0f;
        int count = 0;
        foreach (var input in tableInputFields)
        {
            if (float.TryParse(input.text, out float val))
            {
                sum += val;
                count++;
            }
        }
        float average = count > 0 ? sum / count : 0f;
        averageText.text = "Среднее: " + average.ToString("F2");
    }
}
