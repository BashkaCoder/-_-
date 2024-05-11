using UnityEngine;

/// <summary>Класс, отвечающий за предварительную инициализацию меню. </summary>
public class MenuBootstrap : MonoBehaviour
{
    /// <summary> Объект предприятия.</summary>
    [SerializeField] GameObject Factory;
    /// <summary> Панель сотрудников.</summary>
    [SerializeField] WorkerPanel WorkerPanel;
    /// <summary> Панель развития предприятия.</summary>
    [SerializeField] CharUpgradeMenu CharUpgradeMenu;

    /// <summary> Объект алхимии.</summary>
    [SerializeField] GameObject Alchemy;
    /// <summary> Объект столяров.</summary>
    [SerializeField] GameObject Joinery;
    /// <summary> Объект кузнецов.</summary>
    [SerializeField] GameObject Blacksmithing;

    /// <summary> Список панелей гильдий.</summary>
    [SerializeField] GuildPanel[] GuildPanels;

    /// <summary> Объект банка.</summary>
    [SerializeField] GameObject Bank;

    /// <summary> Запуск скрипта.</summary>
    void Awake()
    {
        InitializeMenus();
    }

    /// <summary> Инициализация набора меню.</summary>
    private void InitializeMenus()
    {
        Factory.SetActive(true);
        Alchemy.SetActive(true);
        Joinery.SetActive(true);
        Blacksmithing.SetActive(true);
        foreach (var guild in GuildPanels) 
        { 
            guild.gameObject.SetActive(true);
            guild.Start();
        }
        WorkerPanel.gameObject.SetActive(true);
        CharUpgradeMenu.gameObject.SetActive(true);
        Bank.SetActive(true);
        Bank.SetActive(false);
        Alchemy.SetActive(false);
        Joinery.SetActive(false);
        Blacksmithing.SetActive(false);
        Factory.SetActive(false);
    }
}
