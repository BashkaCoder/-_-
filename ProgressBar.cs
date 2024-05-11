using UnityEngine;

/// <summary>Класс, отвечающий за прогрессбар оборудования.</summary>
public class ProgressBar : MonoBehaviour
{
    /// <summary>Ииндикатор заполнения прогрессбара.</summary>
    [SerializeField] private RectTransform fill;

    /// <summary>Присовить значение прогрессбару.</summary>
    public void SetValue(float newValue)
    {
        fill.localScale = new Vector2(Mathf.Clamp(newValue, 0, 1), fill.localScale.y);
    }
}
