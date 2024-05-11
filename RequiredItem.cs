using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс требуемого ресурса.</summary>
public class RequiredItem : MonoBehaviour
{
    /// <summary>Изображения ресурса.</summary>
    [SerializeField] private Image iconField;
    /// <summary>Текст количества ресурса.</summary>
    [SerializeField] private TextMeshProUGUI quantity;

    /// <summary>Назначить ресурс.</summary>
    public void SetItem(ProductData itemType, int amount)
    {
        iconField.sprite = itemType.Icon;
        quantity.text = amount.ToString();

        //Set text color
        InventoryItem item = InventorySystem.Instance.Get(itemType);
        if (item != null && item.StackSize >= amount)
        {
            quantity.color = Color.green;
        }
        else
        {
            quantity.color = Color.red;
        }

    }
}
