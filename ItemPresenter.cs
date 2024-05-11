using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс, отвечающий за визуал товара.</summary>
public class ItemPresenter : MonoBehaviour
{
    /// <summary>Информация о товаре.</summary>
    public InventoryItem itemData;
    /// <summary>Поле названия.</summary>
    [SerializeField] private TMP_Text nameText;
    /// <summary>Изображение товара.</summary>
    [SerializeField] private Image itemImage;
    /// <summary>Поле количества товара.</summary>
    [SerializeField] private TMP_Text amountText;

    /// <summary>Инициализация визуала.</summary>
    private void Start()
    {
        UpdateItemFields();
    }
    /// <summary>Обновление всех полей товара.</summary>
    private void UpdateItemFields()
    {
        if (itemData.Data != null)
        {
            nameText.text = itemData.Data.Name;
            itemImage.sprite = itemData.Data.Icon;
            amountText.text = itemData.StackSize.ToString();
        }
    }
    /// <summary>Подписка на события при активации.</summary>
    private void OnEnable()
    {
        itemData.OnItemFieldsChanged += UpdateItemFields;
        if (itemData != null) UpdateItemFields();
    }
    /// <summary>Отписка от событий при отключении объекта.</summary>
    private void OnDisable()
    {
        itemData.OnItemFieldsChanged -= UpdateItemFields;
    }
    /// <summary>Отправка информации в визуал склада.</summary>
    public void OnMoreButtonClick()
    {
        StoragePresenter.Instance.DisplayProductDetails(itemData);
    }
}
