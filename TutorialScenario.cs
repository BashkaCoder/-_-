using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс обучающего сценария.</summary>
public abstract class TutorialScenario : MonoBehaviour
{
    /// <summary>Событие окончания сценария.</summary>
    public Action<TutorialScenario> OnScenarioCompleted;
    /// <summary>Флаг завершения сценария.</summary>
    public bool IsCompleted = false;
    /// <summary>Начальная точка наставника.</summary>
    [SerializeField] protected RectTransform beginWaypoint;
    /// <summary>Средняя точка наставника.</summary>
    [SerializeField] protected RectTransform midWaypoint;
    /// <summary>Конечная точка наставника.</summary>
    [SerializeField] protected RectTransform endWaypoint;
    /// <summary>Время передвижения наставника.</summary>
    [SerializeField] protected float moveTime;
    /// <summary>Файл диалогов.</summary>
    [SerializeField] protected TextAsset dialogueXml;
    /// <summary>Следующий сценарий.</summary>
    [SerializeField] protected TutorialScenario nextScenario; // set if next scenario should start rigth after this one
    /// <summary>Индекс фразы сценария.</summary>
    protected int phraseIndex = 0;

    /// <summary>Менеджер обучения.</summary>
    protected TutorialManager manager => TutorialManager.Instance;

    /// <summary>Запуск скрипта.</summary>
    private void Start()
    {
        manager.Scenarios.Add(this);
    }

    /// <summary>Инициализация сценария.</summary>
    public abstract void Initialize();

    /// <summary>ВЫполнить сценарий.</summary>
    public abstract void Execute(int phraseIndex);

    /// <summary>СФокусирвоать кнопки.</summary>
    protected void FocusButtons(Button[] buttons, bool worldPositionStays = true)
    {
        foreach (var button in buttons)
        {
            manager.FocusButton(button, worldPositionStays);
        }
    }
}
