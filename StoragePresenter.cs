using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс, отвечающий за...</summary>
public class StoragePresenter : Menu
{
    public static StoragePresenter Instance;

    [SerializeField] private InventorySystem inventory;
    [SerializeField] private GameObject resourceContainer;
    [SerializeField] private GameObject itemContainer;
    [SerializeField] private ProductionCalculator productionCalculator;
    private Dictionary<ResourcePresenter, ResourceData> resourceInfo = new();
    private Dictionary<ItemPresenter, InventoryItem> itemInfo = new();


    [SerializeField] private GameObject resPrefab;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private GameObject Description;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text growthText;
    [SerializeField] private TMP_Text consumptionText;

    [SerializeField] private GameObject requirments;
    [SerializeField] private List<Image> reqImage;
    [SerializeField] private List<TMP_Text> reqText;
    [SerializeField] private GameObject priority;
    [SerializeField] private Button storagePriorityButton;
    [SerializeField] private Button storePriorityButton;
    [SerializeField] private Button contractPriorityButton;

    [SerializeField] private GameObject specialization;
    [SerializeField] private TMP_Text specializationText;

    [SerializeField] private GraphController graph;
    [SerializeField] private StorageData storageData;

    public int currentItem { get; private set; }
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }


    private void Start()
    {
        SetResources();
        SetItems();

        //SetRandomValues();

        itemContainer.SetActive(false);
    }

    private void SetResources()
    {
        foreach (ResourceData data in inventory.Resources.Values)
        {
            CreateResource(data);
        }
    }

    private void SetItems()
    {
        foreach (InventoryItem item in inventory.InventoryItems)
        {
            CreateProduct(item);
        }
    }

    private void CreateResource(ResourceData data)
    {
        GameObject go = Instantiate(resPrefab); //создание вспомогательного объекта
        go.transform.SetParent(resourceContainer.transform, false);
        ResourcePresenter resourcePresenter = go.GetComponent<ResourcePresenter>();
        resourcePresenter.resourceData = data;
        resourceInfo.Add(resourcePresenter, data);
    }

    private void CreateProduct(InventoryItem item)
    {
        GameObject go = Instantiate(itemPrefab); //создание вспомогательного объекта
        go.transform.SetParent(itemContainer.transform, false);
        ItemPresenter itemPresenter = go.GetComponent<ItemPresenter>();
        itemPresenter.itemData = item;
        itemInfo.Add(itemPresenter, item);
    }


    private void UpdateResources()
    {
        if (inventory.Resources.Count == resourceInfo.Count)
            return;

        foreach (ResourceData newData in inventory.Resources.Values)
        {
            bool found = false;
            foreach (var existingPresenter in resourceInfo.Keys)
            {
                if (existingPresenter.resourceData == newData)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                CreateResource(newData);
            }
        }
    }


    private void UpdateItems()
    {
        print(1355);
        if (inventory.InventoryItems.Count == itemInfo.Count)
            return;
        print(2122142);
        foreach (InventoryItem newItem in inventory.InventoryItems)
        {
            bool found = false;
            foreach (var existingPresenter in itemInfo.Keys)
            {
                if (existingPresenter.itemData == newItem)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                CreateProduct(newItem);
            }
        }
    }

    public void SetResourceContainerActive()
    {
        resourceContainer.SetActive(true);
        itemContainer.SetActive(false);
    }

    public void SetItemContainerActive()
    {
        SetDescriptionInactive();
        resourceContainer.SetActive(false);
        itemContainer.SetActive(true);
    }

    private void OnEnable()
    {
        InventorySystem.OnInventoryChanged += UpdateItems;
        InventorySystem.OnInventoryChanged += UpdateResources;
        //UpdateResourceConsumption();
        //UpdateItemGrowth();
        SetDescriptionInactive();
    }
    private void OnDisable()
    {
        InventorySystem.OnInventoryChanged -= UpdateItems;
        InventorySystem.OnInventoryChanged -= UpdateResources;
        currentItem = -1;
    }

    private void SetDescriptionInactive()
    {
        Description.SetActive(false);
    }

    public void DisplayResourceDetails(ResourceData data)
    {
        Description.SetActive(true);
        priority.SetActive(false);
        specialization.SetActive(false);
        requirments.SetActive(false);
        descriptionText.text = data.Description;
        growthText.text = productionCalculator.GetResourceIncomePerDay(data).ToString();
        consumptionText.text = productionCalculator.GetResourceConsumptionPerDay(data).ToString();
        DrawGraphForResource(data);
        currentItem = -1;
    }

    private void DrawGraphForResource(ResourceData data)
    {
        graph.ClearGraphs();
        graph.AddNewGraph(storageData.ResourceAmount[data]);
        graph.Draw();
    }

    public void DisplayProductDetails(InventoryItem data)
    {
        Description.SetActive(true);
        priority.SetActive(true);
        specialization.SetActive(true);
        requirments.SetActive(true);
        foreach (var im in reqImage)
        {
            im.gameObject.SetActive(false);
        }
        foreach (var text in reqText)
        {
            text.gameObject.SetActive(false);
        }
        for (int i = 0; i < data.Data.Requirements.Count; i++)
        {
            reqImage[i].sprite = InventorySystem.Instance.Resources[data.Data.Requirements[i].ResourceID].Icon;
            reqImage[i].gameObject.SetActive(true);
            reqText[i].text = data.Data.Requirements[i].Amount.ToString();
            reqText[i].gameObject.SetActive(true);
        }


        specializationText.text = data.Data.Type.ToString();
        descriptionText.text = data.Data.Description;
        growthText.text = productionCalculator.GetItemProductionPerDay(data.Data).ToString();

        consumptionText.text = "";

        DrawGraphForProduct(data.Data);

        currentItem = data.Data.ID;
    }

    private void DrawGraphForProduct(ProductData data)
    {
        graph.ClearGraphs();
        graph.AddNewGraph(storageData.ProductAmount[data]);
        graph.Draw();
    }
}
