using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

/// <summary>Класс, отвечающий за UI выбранноо оборудования.</summary>
public class StationSelectUI : MonoBehaviour
{
    /// <summary>Событие выбора оборудования.</summary>
    public static event Action<Room, StationData> OnEquipmentSelected;
    /// <summary>Изображение оборудования.</summary>
    [SerializeField] private Image icon;
    /// <summary>Текст количества.</summary>
    [SerializeField] private TextMeshProUGUI amountText;
    /// <summary>Текст уровня.</summary>
    [SerializeField] private TextMeshProUGUI levelText;

    /// <summary>Кнопка выбора оборудования.</summary>
    private Button button;
    /// <summary>Комната, в которой находится оборудования.</summary>
    private Room room;
    /// <summary>Данные оборудования.</summary>
    private StationData data;

    /// <summary>Назначить комнату.</summary>
    public void SetRoom(Room r)
    {
        room = r;
    }

    /// <summary>Запуск скрипта.</summary>
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    /// <summary>Назначить даныне .</summary>
    public void SetData(StationData data)
    {
        this.data = data;
        icon.sprite = data.Icon;
        amountText.text = StationInventory.Instance.Stations[data].ToString();
        levelText.text = data.Level.ToString();
        UpdateButtonInteractable();
    }

    /// <summary>Обновить кнопку взаимодействия.</summary>
    private void UpdateButtonInteractable()
    {
        GetComponent<Button>().interactable = StationInventory.Instance.Stations[data] > 0;
    }

    /// <summary>Событие нажатия кнопки оборудования.</summary>
    private void OnButtonClick()
    {
        OnEquipmentSelected?.Invoke(room, data);
    }
}
