using System.Collections.Generic;
using UnityEngine;


/// <summary> Группа вкладок. </summary>
/// <remarks> Позволяет переключать вкладки. </remarks>
public class TabGroup : MonoBehaviour
{
    #region Private Fields
    /// <summary> Кнопки вкладок. </summary>
    [SerializeField] private List<TabButton> tabButtons;
    /// <summary> Вкладки. </summary>
    [SerializeField] private List<Menu> tabs;
    #endregion

    #region MonoBehaviour Methods
    /// <summary> Назначает каждой кнопке группу вкладок. </summary>
    private void Start()
    {
        if (tabButtons == null)
            Debug.LogWarning("Tab buttons are not assigned");
        if (tabs == null)
            Debug.LogWarning("Tabs are not assigned");
        if (tabs == null || tabButtons == null)
            return;

        foreach (TabButton button in tabButtons)
        {
            button.TabGroup = this;
        }
    }
    #endregion

    #region Public Methods
    /// <summary> Обработка выбора вкладки. </summary>
    /// <param name="tabButton"></param>
    public void OnTabSelected(TabButton tabButton)
    {
        int buttonIndex = tabButtons.IndexOf(tabButton);
        for (int i = 0; i < tabButtons.Count; i++)
        {
            if (i == buttonIndex)
            {
                tabs[i].Open();
            }
            else
            {
                tabs[i].Close();
            }
        }
    }
    #endregion
}