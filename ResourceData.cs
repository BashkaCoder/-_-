using System;
using UnityEngine;

/// <summary>Класс, отвечающий за...</summary>
[CreateAssetMenu(fileName = "NewResourceData", menuName = "GameCore/Production/ResourceData")]
public class ResourceData : ScriptableObject
{
    public int ID;
    public string Name;
    public float Cost;
    public Sprite Icon;
    public string Description;
    public int StackSize { get; private set; }
    public Specialization Type;

    public Action OnResourceFieldsChanged;


    public void AddToStack(int amount)
    {
        StackSize += amount;
        OnResourceFieldsChanged?.Invoke();
    }

    public void RemoveFromStack(int amount)
    {
        if (StackSize - amount >= 0)
        {
            StackSize -= amount;
        }
        else
        {
            Debug.LogError($"Количество ресурса {Name} не может быть меньше нуля");
            StackSize = 0;
        }
        OnResourceFieldsChanged?.Invoke();
    }
}
