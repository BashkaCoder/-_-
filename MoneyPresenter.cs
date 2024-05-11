using System;
using TMPro;
using UnityEngine;

/// <summary>Класс, отвечающий за отображение количества монет.</summary>
public class MoneyPresenter : MonoBehaviour
{
    #region Private Fields
    /// <summary>Численное отображение денег.</summary>
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private AudioClip[] moneySound;
    [SerializeField] private AudioSource audioSource;
    #endregion

    #region Private Methods

    /// <summary>Вызов апдейта текста в момент активации объекта.</summary>
    private void Start()
    {
        UpdateMoneyValue();
    }

    /// <summary>Апдейт текстового обозначения количества денег.</summary>
    private void UpdateMoneyValue()
    {
        coinText.text = ChangeNumber(Player.Instance.Money.Value);
    }
   
    /// <summary>Изменение отображаемого числа.</summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private string ChangeNumber(decimal amount)
    {
        string value;
        if (amount >= 1000000)
            value = Math.Floor(amount / 1000000).ToString() + "M";
        else if (amount >= 1000)
            value = Math.Floor(amount / 1000).ToString() + "K";
        else
            value = amount.ToString();
        return value;
    }
    /// <summary>Проигрывание звука при изменении количества денег у игрока.</summary>
    private void PlayMoneySound()
    {
        if (moneySound != null && audioSource != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, moneySound.Length);
            audioSource.PlayOneShot(moneySound[randomIndex]);
        }
    }
    /// <summary>Подписка методов при включении и активации объекта.</summary>
    private void OnEnable()
    {
        Money.OnMoneyChanged += UpdateMoneyValue;
        Money.OnMoneyChanged += PlayMoneySound;
    }
    /// <summary>Отписка методов при отключении объекта.</summary>
    private void OnDisable()
    {
        Money.OnMoneyChanged -= UpdateMoneyValue;
        Money.OnMoneyChanged -= PlayMoneySound;
    }
    #endregion
}