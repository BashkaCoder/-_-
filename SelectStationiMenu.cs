using UnityEngine;

/// <summary>Класс, отвечающий за выбор меню оборудования.</summary>
public class SelectStationiMenu : MonoBehaviour
{
    /// <summary>Родитель панели содержимого.</summary>
    [SerializeField] private Transform contentParent;
    /// <summary>Префаб отображения оборудования.</summary>
    [SerializeField] private GameObject StationUIPrefab;

    /// <summary>Комната.</summary>
    private Room room;
    /// <summary>Инвентарб оборудования.</summary>
    private StationInventory inventory => StationInventory.Instance;

    /// <summary>Назначить комнату.</summary>
    public void SetRoom(Room r)
    {
        room = r;
    }

    /// <summary>Обновить содержимое.</summary>
    private void UpdateContent()
    {
        ClearContent();
        foreach (StationData station in inventory.Stations.Keys)
        {
            AddStation(station);
        }
    }

    /// <summary>Очистить содержимое.</summary>
    private void ClearContent()
    {
        foreach (Transform t in contentParent)
        {
            Destroy(t.gameObject);
        }
    }

    /// <summary>Добавить оборудование.</summary>
    private void AddStation(StationData stats)
    {
        var station = Instantiate(StationUIPrefab, contentParent, false);
        StationSelectUI ui = station.GetComponent<StationSelectUI>();
        ui.SetData(stats);
        ui.SetRoom(room);
    }

    /// <summary>Закрыть панель.</summary>
    private void Close(Room r, StationData d)
    {
        GetComponent<Menu>().Close();
    }

    /// <summary>Установить оборудование.</summary>
    private void PlaceStation(Room r, StationData data)
    {
        if (inventory.Stations[data] > 0)
            inventory.Stations[data]--;
    }

    /// <summary>Срабатывает при включении скрипта.</summary>
    private void OnEnable()
    {
        UpdateContent();
        StationSelectUI.OnEquipmentSelected += Close;
        StationSelectUI.OnEquipmentSelected += PlaceStation;
    }

    /// <summary>Срабатывает при выключении скрипта.</summary>
    private void OnDisable()
    {
        StationSelectUI.OnEquipmentSelected -= Close;
        StationSelectUI.OnEquipmentSelected += PlaceStation;
    }
}
