using UnityEngine;

/// <summary>Класс, отвечающий за закрытие меню.</summary>
public class MenuCloser : MonoBehaviour
{
    [SerializeField]
    /// <summary> Список обхектов к закрытию.</summary>
    private RectTransform[] menusToClose;

    /// <summary> Срабатывает при включении скрипта.</summary>
    private void OnEnable()
    {
        CloseMenus();
    }

    /// <summary> Закрыть набор меню.</summary>
    private void CloseMenus()
    {
        foreach (RectTransform menu in menusToClose)
        {
            menu.gameObject.SetActive(false);
        }
    }
}
