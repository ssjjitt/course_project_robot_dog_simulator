using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [Header("Кнопка старта/стопа")]
    public Button toggleButton;

    [Header("Текст для вывода времени")]
    public TMP_Text timerText;

    private bool isRunning = false;
    private float elapsedTime = 0f;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleStopwatch);
        UpdateTimerText(0f);
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText(elapsedTime);
        }
    }

    void ToggleStopwatch()
    {
        isRunning = !isRunning;
    }

    void UpdateTimerText(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);
        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}
