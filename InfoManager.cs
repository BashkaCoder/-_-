using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс, отвечающий за окно справки.</summary> //TODO ддописать комментарии
public class InfoManager : MonoBehaviour
{
    #region Private Fields
    /// <summary>Лист с информацией.</summary>
    [SerializeField] private List<InfoData> infoDatas;
    /// <summary>Лиcт с кнопками категорий.</summary>
    [SerializeField] private List<Button> categoriesButtons;
    /// <summary>Поле деталей.</summary>
    [SerializeField] private TMP_Text details;
    /// <summary>Изображение справки.</summary>
    [SerializeField] private Image infoImage;
    /// <summary>Название раздела.</summary>
    [SerializeField] private TMP_Text title;
    /// <summary>Область для подробной информации раздела.</summary>
    [SerializeField] private GameObject infoDetails;
    #endregion

    /// <summary>Инстанс менеджера.</summary>
    public static InfoManager Instance;

    #region Private Methods
    /// <summary>Создание единственного инстанса окна справки.</summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    /// <summary> Приведение в начальное состояние окна справки при его открытии.</summary>
    private void OnEnable()
    {
        OnCategoriesButtonClick(0);
        infoDetails.SetActive(false);
    }
    #endregion
    #region Public Methods
    /// <summary> Метод, отвечающий за открытие категории по ее порядковому номеру.</summary>
    /// <param name="cat"> Порядковый номер категории.</param>
    public void OnCategoriesButtonClick(int cat)
    {
        foreach (var but in categoriesButtons)
        {
            but.image.color = Color.white;
        }
        categoriesButtons[cat].image.color = Color.red;
    }

    /// <summary> Отображение деталей выбранной справки.</summary>
    /// <param name="data">Передаваемая информация.</param>
    public void ShowInfoDetails(InfoData data)
    {
        infoDetails.SetActive(true);
        details.text = data.infoDetails;
        title.text = data.title;
        infoImage.sprite = data.sprite;
    }
    #endregion
}
