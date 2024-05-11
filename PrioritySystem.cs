using UnityEngine;
using UnityEngine.UI;
public enum ItemsPriority
{
    None,
    Storage,
    Store,
    Contracts
}

/// <summary>Класс, отвечающий за...</summary>
public class PrioritySystem : MonoBehaviour
{

    public static PrioritySystem Instance;
    [SerializeField] private Button storageButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button contractButton;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    private void UpdatePriority(ItemsPriority priority)
    {
        if (StoragePresenter.Instance.currentItem != -1)
        {
            InventorySystem.Instance.Items[StoragePresenter.Instance.currentItem].Data.priority = priority;
            return;
        }
    }
    public void OnStorageButtonClick()
    {
        storageButton.GetComponent<Image>().color = Color.blue;
        storeButton.GetComponent<Image>().color = Color.white;
        contractButton.GetComponent<Image>().color = Color.white;
        UpdatePriority(ItemsPriority.Storage);
    }
   
    public void OnStoreButtonClick()
    {
        storageButton.GetComponent<Image>().color = Color.white;
        storeButton.GetComponent<Image>().color = Color.yellow;
        contractButton.GetComponent<Image>().color = Color.white;
        UpdatePriority(ItemsPriority.Store);
    }
    public void OnContractButtonClick()
    {
        storageButton.GetComponent<Image>().color = Color.white;
        storeButton.GetComponent<Image>().color = Color.white;
        contractButton.GetComponent<Image>().color = Color.green;
        UpdatePriority(ItemsPriority.Contracts);
    }


    public void SendItemToSomewhere(ProductData data, int amount)
    {
        if (!InventorySystem.Instance.Items.TryGetValue(data.ID, out InventoryItem invItem) )
        {
            InventorySystem.Instance.Add(data, amount);
        }
        if (data.priority == ItemsPriority.Storage || data.priority == ItemsPriority.None)
        {
            InventorySystem.Instance.Add(data, amount);
        }
        if (data.priority == ItemsPriority.Contracts)
        {
            print("bibubibubiii");
        }
        if (data.priority == ItemsPriority.Store)
        {
            if (Store.Instance.ItemAmount.TryGetValue(data.ID, out int itemAmount))
            {
                if (itemAmount + amount > Store.MAX_CAP)
                {
                    Store.Instance.AddItem(data.ID, Store.MAX_CAP);
                    InventorySystem.Instance.Add(data, itemAmount + amount - Store.MAX_CAP);
                }
                else
                {
                    Store.Instance.AddItem(data.ID, amount);
                }
            }
            else Debug.LogError("товара нет в лавке.");
        }
    }
}
