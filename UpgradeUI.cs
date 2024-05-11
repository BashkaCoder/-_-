using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс отображения улучшений.</summary>
public class UpgradeUI : MonoBehaviour
{
    /// <summary>Сообытие улучшения.</summary>
    public static event Action<UpgradeUI> OnCostButtonPressed;
    /// <summary>Событие покупки улучшения.</summary>
    public static event Action<StationUpgradeEnum, StationStats> OnUpgradeBought;

    /// <summary>Тип улучшения.</summary>
    [SerializeField] private StationUpgradeEnum upgradeType;

    /// <summary>Следующий текст.</summary>
    [SerializeField] private TextMeshProUGUI nextText;
    /// <summary>Текст количества.</summary>
    [SerializeField] private TextMeshProUGUI countText;
    /// <summary>Текст стоимости.</summary>
    [SerializeField] private TextMeshProUGUI costText;

    /// <summary>Подвердить улучшение.</summary>
    [SerializeField] private Button yesButton;
    /// <summary>Отменить улучшение.</summary>
    [SerializeField] private Button noButton;
    /// <summary>Кнопка стоимости улучшения.</summary>
    [SerializeField] private Button costButton;

    /// <summary>Текущее оборудование.</summary>
    private StationStats currentStation;
    /// <summary>Предыдущее значение.</summary>
    private float oldValue;
    /// <summary>Новое значение.</summary>
    private float newValue;

    /// <summary>Обновить состояние.</summary>
    public void UpdateState(StationStats stats)
    {
        currentStation = stats;
        HandleMaxUpgrades(stats);
        SetCount(stats.Upgrades[upgradeType]);
        oldValue = FormulaUtils.Instance.StationPercents(upgradeType, currentStation.Upgrades[upgradeType]);
        SetNextValue(FormulaUtils.Instance.StationPercents(upgradeType, currentStation.Upgrades[upgradeType] + 1));
    }

    /// <summary>Назначить количество.</summary>
    private void SetCount(int new_count)
    {
        countText.text = new_count.ToString() + "/5";
    }

    /// <summary>Назначить следующее значение.</summary>
    private void SetNextValue(float new_value)
    {
        nextText.text = $"{oldValue}% -> {new_value}%";
        newValue = new_value;
    }

    /// <summary>Срабатывает при включении.</summary>
    private void OnEnable()
    {
        costButton.onClick.AddListener(() => { OnCostButtonPressed?.Invoke(this); });
        costButton.onClick.AddListener(HandleCostButtonPress);
        noButton.onClick.AddListener(ResetButtons);
        yesButton.onClick.AddListener(HandleYesButtonPress);
        OnCostButtonPressed += ResetIfOtherPressed;
        StationStats.OnStationStatsChanged += UpdateState;
    }

    /// <summary>Срабатывает при выключении.</summary>
    private void OnDisable()
    {
        costButton.onClick.RemoveAllListeners();
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        StationStats.OnStationStatsChanged -= UpdateState;
        ResetButtons();
    }

    /// <summary>Восстанвоить при ином нажатии.</summary>
    private void ResetIfOtherPressed(UpgradeUI other)
    {
        if (this != other)
            ResetButtons();
    }
    /// <summary>Восстанвоить кнопки.</summary>
    private void ResetButtons()
    {
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        costButton.gameObject.SetActive(true);
    }
    /// <summary>Обработчик кнопки стоимости.</summary>
    private void HandleCostButtonPress()
    {
        costButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    /// <summary>Обработчик подтверждения улучшения.</summary>
    private void HandleYesButtonPress()
    {
        OnUpgradeBought?.Invoke(upgradeType, currentStation);
        Player.Instance.Money.RemoveMoney(200);
        ResetButtons();
        oldValue = FormulaUtils.Instance.StationPercents(upgradeType, currentStation.Upgrades[upgradeType]);
        SetNextValue(FormulaUtils.Instance.StationPercents(upgradeType, currentStation.Upgrades[upgradeType] + 1));
    }

    /// <summary>Обработчик максимального количества улучшений.</summary>
    private void HandleMaxUpgrades(StationStats stats)
    {
        bool status = stats.ReachedMaxUpgrades;
        if (status) DisableButton();
        else EnableButton();
    }
    /// <summary>Включить кнопку.</summary>
    public void EnableButton()
    {
        costButton.interactable = true;
        costText.text = "200";
    }
    /// <summary>Выклчюить кнопку.</summary>
    public void DisableButton()
    {
        costButton.interactable = false;
        costText.text = "MAX";
    }
}
