using UnityEngine;
using System.Collections.Generic;


/// <summary> Запоминание информации о ресурсах и товарах. </summary>
/// <remarks> Запоминает количество товаров и ресурсов каждый день для отображения на графике. </remarks>
public class StorageDataLogger : MonoBehaviour
{
    #region Private Fields
    /// <summary> Модель данных о товарах и ресурсах. </summary>
    [SerializeField] private StorageData storageData;
    /// <summary> Логика вычисления скорости поступления ресурсов и скорости потребления товаров. </summary>
    [SerializeField] private ProductionCalculator productionCalculator;
    /// <summary> Инвентарь ресурсов. </summary>
    private InventorySystem inventory => InventorySystem.Instance;
    #endregion

    #region MonoBehaviour Music
    /// <summary> Подписывание методов на события. </summary>
    private void OnEnable()
    {
        TimeManager.OnDayChanged += LogData;
    }

    /// <summary> Отписывание методов от событий. </summary>
    private void OnDisable()
    {
        TimeManager.OnDayChanged -= LogData;
    }
    #endregion

    #region Private Fields
    /// <summary> Запись данных. </summary>
    private void LogData()
    {
        LogResourceAmount();
        LogResourceIncome();
        LogProductAmount();
    }

    /// <summary> Запись данных о количестве ресурсов. </summary>
    private void LogResourceAmount()
    {
        foreach (int key in inventory.Resources.Keys)
        {
            int amount = inventory.Resources[key].StackSize;
            LogValue(storageData.ResourceAmount, inventory.Resources[key], amount);
        }
    }

    /// <summary> Запись данных о скорости поступления ресурсов. </summary>
    private void LogResourceIncome()
    {
        foreach (ResourceData res in inventory.Resources.Values)
        {
            int income = productionCalculator.GetResourceIncomePerDay(res);
            LogValue(storageData.ResourceIncome, res, income);
        }
    }

    /// <summary> Запись данных о количестве товаров. </summary>
    private void LogProductAmount()
    {
        foreach (InventoryItem product in inventory.InventoryItems)
        {
            int amount = product.StackSize;
            LogValue(storageData.ProductAmount, product.Data, amount);
        }
    }

    /// <summary> Запись значения. </summary>
    /// <typeparam name="T"> Тип значения. </typeparam>
    /// <param name="dict"> Куда записывать. </param>
    /// <param name="key"> Ключ. </param>
    /// <param name="value"> Значение. </param>
    private void LogValue<T>(Dictionary<T, List<int>> dict, T key, int value)
    {
        if (dict.ContainsKey(key) == false)
        {
            dict.Add(key, new List<int>());
        }

        dict[key].Add(value);
    }
    #endregion
}