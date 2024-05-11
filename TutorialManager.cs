using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс, отвечающий за управление обучением.</summary>
public class TutorialManager : MonoBehaviour
{
    /// <summary>Instance контроллера.</summary>
    public static TutorialManager Instance;
    /// <summary>Контроллер диалогов.</summary>
    public DialogueManager DialogueManager;
    /// <summary>Черная подложка.</summary>
    public Image BlackScreenImage;
    /// <summary>Родитель подложки.</summary>
    [SerializeField] private RectTransform blackScreenParent;
    /// <summary>Список сценариев.</summary>
    [HideInInspector] public List<TutorialScenario> Scenarios;
    /// <summary>Первый сценарий.</summary>
    [SerializeField] private TutorialScenario firstScenario;
    /// <summary>Флаг старта обучения.</summary>
    [SerializeField] private bool startTutorial;
    /// <summary>Наставник.</summary>
    public Bat Bat;

    /// <summary>Текущий сценарий.</summary>
    private TutorialScenario currentScenario;
    /// <summary>Словарь обхектов взаимодействия.</summary>
    private Dictionary<Transform, Transform> focusedObjects;

    /// <summary>Запуск скрипта.</summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Scenarios = new();
            return;
        }
        Destroy(gameObject);
    }

    /// <summary>Инициализация скрипта.</summary>
    private void Start()
    {
        if (startTutorial)
            StartScenario(firstScenario);
    }

    /// <summary>Начать сценарий.</summary>
    public void StartScenario(TutorialScenario scenario)
    {
        if (currentScenario != null) return;
        currentScenario = scenario;
        scenario.Initialize();
        scenario.Execute(0);
        scenario.OnScenarioCompleted += EndCurretnScenario;
    }
    /// <summary>Сфокусировать объект.</summary>
    public void FocusObject(Transform obj, bool worldPositionStays = true)
    {
        obj.transform.SetParent(blackScreenParent, worldPositionStays);
        BlackScreenImage.enabled = false;
        blackScreenParent.GetComponent<Image>().enabled = true;
    }

    /// <summary>Убрать объект из фокуса.</summary>
    public void RemoveObjectFromFocus(Transform obj, Transform prevParent, bool worldPositionStays = true)
    {
        obj.transform.SetParent(prevParent, worldPositionStays);
        BlackScreenImage.enabled = true;
        blackScreenParent.GetComponent<Image>().enabled = false;
    }

    /// <summary>СФокусировать кнопку.</summary>
    public void FocusButton(Button button, bool worldPositionStays = true)
    {
        focusedObjects.Add(button.transform, button.transform.parent);
        FocusObject(button.transform);
        button.onClick.AddListener(OnFocusedButtonClick);
    }

    /// <summary>Убрать кнопку из фокуса.</summary>
    public void RemoveButtonFromFocus(Button button, Transform prevParent, bool worldPositionStays = true)
    {
        RemoveObjectFromFocus(button.transform, prevParent);
        button.onClick.RemoveListener(OnFocusedButtonClick);
        focusedObjects.Remove(button.transform);
    }

    /// <summary>Событие нажатия кнопки в фокусе.</summary>
    private void OnFocusedButtonClick()
    {
        DialogueManager.OnClickDialogue();
        foreach (Transform b in focusedObjects.Keys)
        {
            RemoveButtonFromFocus(b.GetComponent<Button>(), focusedObjects[b]);
        }
    }

    /// <summary>Закончить текущий сценарий.</summary>
    private void EndCurretnScenario(TutorialScenario scenario)
    {
        currentScenario = null;
    }

    // is called when end==true sentence is finished
    /// <summary>Закончить диалог.</summary>
    public void EndDialogue(int phraseIndex)
    {
        currentScenario.Execute(phraseIndex);
    }
}
