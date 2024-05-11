using System;

/// <summary>Класс денег.</summary>
public class Money
{
    /// <summary> Событие изменения денег.</summary>
    public static event Action OnMoneyChanged;
    /// <summary> Значение денег.</summary>
    private int moneyValue = 0;
    /// <summary> СВойство денег.</summary>
    public int Value
    {
        get => moneyValue;
        private set
        {
            moneyValue = value;
        }
    }

    /// <summary> Добавить деньги.</summary>
    public void AddMoney(int amount)
    {
        moneyValue += amount;
        OnMoneyChanged?.Invoke();
    }

    /// <summary> Оотнять деньги.</summary>
    public void RemoveMoney(int amount)
    {
        if (moneyValue >= amount)
        {
            moneyValue -= amount;
            OnMoneyChanged?.Invoke();
        }
        else
        {
            amount -= moneyValue;
            moneyValue = 0;
            Bank.Instance.CreateNewDebt(amount);
        }
    }

}
