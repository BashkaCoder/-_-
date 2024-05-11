using UnityEngine;

/// <summary> Класс, представляющий меню.</summary>
public class Menu : MonoBehaviour
{
    /// <summary> Открыть меню.</summary>
    public void Open()
    {
        gameObject.SetActive(true);
    }
    /// <summary> Закрыть меню.</summary>
    public void Close()
    {
        gameObject.SetActive(false);
    }

    /// <summary> Закрытие меню по нажатию "Esc".</summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
}
