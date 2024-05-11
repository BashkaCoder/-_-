using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс, отвечающий за поведение вкладки.</summary>
public class TabButton : MonoBehaviour
{
    /// <summary>Группа вкладок.</summary>
    [HideInInspector] public TabGroup TabGroup;
    /// <summary>Кнопка вкладки.</summary>
    private Button button;

    /// <summary>Запуск скрипта.</summary>
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }
    /// <summary>Обработчик клика.</summary>
    private void HandleClick()
    {
        TabGroup.OnTabSelected(this);
    }
}
