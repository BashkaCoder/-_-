using UnityEngine;

/// <summary>Класс, отвечающий за справочную информацию.</summary>
public class InfoData : MonoBehaviour
{
    /// <summary>Изображение справки.</summary>
    public Sprite sprite;

    /// <summary>Информация.</summary>
    public string infoDetails;
    /// <summary>Подпись.</summary>
    public string title ="";

    /// <summary>По нажатию на инфо, передача информации панели справки.</summary>
    public void OnInfoClick()
    {
        InfoManager.Instance.ShowInfoDetails(this);
    }
}
 