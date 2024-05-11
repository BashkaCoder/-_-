using UnityEngine;

/// <summary>Класс, отвечающий за выбор меню.</summary>
public class SelectMenu : MonoBehaviour
{
    /// <summary>Префаб сотрудника.</summary>
    [SerializeField] private GameObject workerUIPrefab;
    /// <summary>Префаб товара.</summary>
    [SerializeField] private GameObject itemUIPrefab;
    /// <summary>Поле содержания информации.</summary>
    [SerializeField] private Transform content;

    /// <summary>Выбранное оборудование.</summary>
    private StationSelect selectedStation;

    /// <summary>Назначить оборудование.</summary>
    public void SetStation(StationSelect station)
    {
        this.selectedStation = station;
    }

    /// <summary>ОТрисовать предметы.</summary>
    public void DrawItems(Specialization type)
    {
        ClearContent();
        foreach (ProductData item in InventorySystem.Instance.CraftableItems)
        {
            if (item.Type == selectedStation.GetStationType())
            {
                DrawItem(item);
            }
        }
    }

    /// <summary>ОТрисовать предмет.</summary>
    private void DrawItem(ProductData stats)
    {
        GameObject newItem = Instantiate(itemUIPrefab, content);
        newItem.transform.GetComponent<ItemInstance>().SetStats(stats);
    }

    /// <summary>Обновить сотрудников.</summary>
    public void UpdateWorkers()
    {
        ClearContent();
        foreach (EmployeeData emp in EmployeeController.Instance.WorkerList)
        {
            if (emp.IsAvailable && emp.Specialization == selectedStation.GetStationType())
            {
                CreateWorkerUI(emp);
            }
        }
    }

    /// <summary>Очистить содержимое.</summary>
    private void ClearContent()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>Создать UI сотрудника.</summary>
    private void CreateWorkerUI(EmployeeData worker)
    {
        GameObject workerUI = Instantiate(workerUIPrefab, content);
        WorkerInstance workerInstance = workerUI.GetComponent<WorkerInstance>();
        workerInstance.SetStats(worker);
    }

    /// <summary>Выбрать сотрудника.</summary>
    private void SelectWorker(EmployeeData worker)
    {
        selectedStation.ChangeWorker(worker);
        UpdateWorkers();
    }

    /// <summary>Выбрать предмет.</summary>
    private void SelectItem(ProductData item)
    {
        selectedStation.ChangeItem(item);
        DrawItems(selectedStation.GetStationType());
    }

    /// <summary>Создать UI предмета.</summary>
    private void CreateItemUI(ProductData itemData)
    {
        GameObject itemUI = Instantiate(itemUIPrefab, content);
    }

    /// <summary>Срабатывает при включении скрипта.</summary>
    private void OnEnable()
    {
        WorkerInstance.OnWorkerSelected += SelectWorker;
        ItemInstance.OnItemSelected += SelectItem;
    }

    /// <summary>Срабатывает при выключении скрипта.</summary>
    private void OnDisable()
    {
        WorkerInstance.OnWorkerSelected -= SelectWorker;
        ItemInstance.OnItemSelected -= SelectItem;
    }
}
