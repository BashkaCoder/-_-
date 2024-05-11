using TMPro;
using UnityEngine;

/// <summary>Класс, отвечающий за ототбражение управлением времени.</summary>
public class TimeManagementView : MonoBehaviour
{
    /// <summary>Текст времени.</summary>
    [SerializeField] private TextMeshProUGUI timeText;

    /// <summary>Прошедшие минуты.</summary>
    private int minute => TimeManager.Instance.Minute;
    /// <summary>Прошедшие часы.</summary>
    private int hour => TimeManager.Instance.Hour;
    /// <summary>Прошедшие дни.</summary>
    private int day => TimeManager.Instance.Day;
    /// <summary>Прошедшие месяцы.</summary>
    private int month => TimeManager.Instance.Month;

    /// <summary>Событие паузы.</summary>
    public void OnPauseClick()
    {
        TimeManager.Instance.SetPause(true);
    }

    /// <summary>Событие возобновления времени.</summary>
    public void OnResumeClick()
    {
        TimeManager.Instance.SetPause(false);
    }

    /// <summary>Обновить время.</summary>
    private void UpdateTime()
    {
        timeText.text = $"{month:00}:{day:00}:{hour:00}:{minute:00}";
    }

    /// <summary>Срабатывает при включении скрипта.</summary>
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
    }

    /// <summary>Срабатывает при выключении скрипта.</summary>
    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
    }
}

