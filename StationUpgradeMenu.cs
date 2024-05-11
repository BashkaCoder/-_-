using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>Класс, овтечающий за улучшение оборудования.</summary>
public class StationUpgradeMenu : MonoBehaviour
{
    /// <summary>ТЕкст количества оставшихся улучшений.</summary>
    [SerializeField] private TextMeshProUGUI upgradesLeftText;
    /// <summary>Список улучшений.</summary>
    [SerializeField] private List<UpgradeUI> upgrades;

    /// <summary>Срабатывает при включении.</summary>
    private void OnEnable()
    {
        StationStats.OnStationStatsChanged += UpdateAvailableUpgradesCount;
    }

    /// <summary>Срабатывает при выключении.</summary>
    private void OnDisable()
    {
        StationStats.OnStationStatsChanged -= UpdateAvailableUpgradesCount;
    }

    /// <summary>Обновить количество оставшихся улучшений.</summary>
    private void UpdateAvailableUpgradesCount(StationStats stats)
    {
        upgradesLeftText.text = (stats.MaxUpgrades - stats.CurrentUpgrades).ToString();
    }

    /// <summary>Назначить оборудование.</summary>
    public void SetStation(StationStats stats)
    {
        UpdateAvailableUpgradesCount(stats);
        foreach (UpgradeUI upgrade in upgrades)
            upgrade.UpdateState(stats);
    }
}
