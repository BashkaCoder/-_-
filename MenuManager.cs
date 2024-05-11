using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Класс, отвечающий за загрузку сцены игры и выход из игры</summary>
public class MenuManager : MonoBehaviour
{

    /// <summary> Событие нажатия кнопки "Играть".</summary>
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Gameplay");
    }

    /// <summary> Событие на нажатие кнопки "Выход".</summary>
    public void OnClickExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
