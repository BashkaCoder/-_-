using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс, отвечающий за меню оборудования.</summary>
public class StationMenu : MonoBehaviour
{
    /// <summary>Название оборудования.</summary>
    [SerializeField] private TextMeshProUGUI stationNameText;

    [Header("Prefabs")]
    /// <summary>Префаб сотрудника.</summary>
    [SerializeField] private GameObject workerPrefab;
    /// <summary>Префаб товара.</summary>
    [SerializeField] private GameObject itemPrefab;

    [Header("Buttons")]
    /// <summary>Кнопка запуска производства.</summary>
    [SerializeField] private Button startButton;
    /// <summary>Кнопка остановки производства.</summary>
    [SerializeField] private Button stopButton;

    [Header("Parent Objects")]
    /// <summary>РОдитель сотрудника.</summary>
    [SerializeField] private Transform workerParent;
    /// <summary>Родитель товара.</summary>
    [SerializeField] private Transform itemParent;

    /// <summary>ВЫбранное оборудование.</summary>
    private StationSelect selectedStation;
    /// <summary>Производство оборудования.</summary>
    private StationCraft stationCraft => selectedStation.StationCraft;

    /// <summary>Назначить оборудование.</summary>
    public void SetStation(StationSelect station)
    {
        this.selectedStation = station;
        stationNameText.text = station.GetName();
        SetWorker(station.Worker);
        SetItem(station.Item);
        UpdateCraftingButton(stationCraft, stationCraft.IsCrafting);
    }

    /// <summary>Назначить сотрудника.</summary>
    public void SetWorker(EmployeeData worker)
    {
        ClearWorker();
        if (worker == null)
        {
            return;
        }
        //create worker instance
        GameObject newWorker = Instantiate(workerPrefab, workerParent);
        //set stats
        worker.IsAvailable = false;
        newWorker.GetComponent<WorkerInstance>().SetStats(worker);
        newWorker.GetComponent<Button>().enabled = false;
    }

    /// <summary>Открепить сотрудника.</summary>
    public void UnlikWorker()
    {
        selectedStation.UnlinkWorker();
        ClearWorker();
    }

    /// <summary>ОТкрепить товар.</summary>
    public void UnlinkItem()
    {
        selectedStation.UnlinkItem();
        ClearItem();
    }

    /// <summary>Выбрать сотрудника.</summary>
    public void SelectWorker()
    {
        selectedStation.OpenWorkerSelectMenu();
    }

    /// <summary>Назначить товар.</summary>
    public void SetItem(ProductData item)
    {
        ClearItem();
        if (item == null)
            return;
        GameObject newItem = Instantiate(itemPrefab, itemParent, false);
        newItem.transform.GetComponent<ItemInstance>().SetStats(item);
        newItem.GetComponent<Button>().enabled = false;
    }
    /// <summary>ВЫбрать товар.</summary>
    public void SelectItem()
    {
        selectedStation.OpenItemSelectMenu();
    }

    //called when craft button is pressed
    /// <summary>Событие запуска производства.</summary>
    public void OnStartButtonClicked()
    {
        stationCraft.TryToCraft();
    }

    //called when stop crafting button is pressed
    /// <summary>Событие приостановки производства.</summary>
    public void OnPauseButtonClicked()
    {
        stationCraft.SetPause(true);
    }

    /// <summary>Событие отмены производства.</summary>
    public void OnAbortButtonClicked()
    {
        stationCraft.StopCrafting();
    }

    /// <summary>Открыть меню улучшений.</summary>
    public void OpenUpgradeMenu()
    {
        selectedStation.OpenUpgradeMenu();
    }

    /// <summary>Обновить кнопку производства.</summary>
    private void UpdateCraftingButton(StationCraft station, bool isCrafting)
    {
        if (station != stationCraft) return;

        if (isCrafting)//then set button to stop
        {
            startButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);
        }
        else//set button to srart
        {
            startButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
        }
    }

    /// <summary>Очистить детей.</summary>
    private void ClearChildren(Transform parent, Func<Transform, bool> condition)
    {
        foreach (Transform child in parent)
        {
            if (condition(child))
                Destroy(child.gameObject);
        }
    }

    /// <summary>Очистить сотрудника.</summary>
    private void ClearWorker()
    {
        ClearChildren(workerParent, (child) => child.GetComponent<WorkerInstance>() != null);
    }

    /// <summary>Очистить товар.</summary>
    private void ClearItem()
    {
        ClearChildren(itemParent, (child) => child.GetComponent<ItemInstance>() != null);
    }

    /// <summary>Срабатывает при включении.</summary>
    private void OnEnable()
    {
        StationCraft.OnCraftingStatusChanged += UpdateCraftingButton;
    }

    /// <summary>Срабатывает при включении.</summary>
    private void OnDisable()
    {
        StationCraft.OnCraftingStatusChanged -= UpdateCraftingButton;
    }
}
