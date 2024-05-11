
using System.Collections.Generic;
using UnityEngine;


/// <summary> Данные о количестве товаров и ресурсов. </summary>
/// <remarks> Сохраняет данные о ресурсах и товарах для отображения на графике. </remraks>
public class StorageData : MonoBehaviour
{
    #region Public Fields
    /// <summary> Количество ресурсов. </summary>
    public Dictionary<ResourceData, List<int>> ResourceAmount;
    /// <summary> Скорость поступления ресурсов. </summary>
    public Dictionary<ResourceData, List<int>> ResourceIncome;
    /// <summary> Количество товаров. </summary>
    public Dictionary<ProductData, List<int>> ProductAmount;
    /// <summary> Цена товаров. </summary>
    public Dictionary<ProductData, List<int>> ProductConst;
    #endregion

    /// <summary> Вызыв инициализации при запуске сцены. </summary>
    private void Awake()
    {
        Initialize();
    }

    /// <summary> Инициализация. </summary>
    private void Initialize()
    {
        ResourceAmount = new();
        ResourceIncome = new();
        ProductAmount = new();
        ProductConst = new();
    }
}