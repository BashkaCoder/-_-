using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Класс возвращения в главное меню</summary>
public class BackToMainMenu : MonoBehaviour
{
    /// <summary>Кнопка возвращения в главное меню</summary>
    [SerializeField] private Button BackToMenu;

    /// <summary>Метод загрузки главного меню</summary>
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Menu");
    }
}