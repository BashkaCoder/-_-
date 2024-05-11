using System.Collections;
using UnityEngine;

/// <summary> Класс, отвечающий за визуальное отображение наставника в обучении.</summary>
public class Bat : MonoBehaviour
{
    [Header("Movement")]
    /// <summary> Точность движения.</summary>
    [SerializeField] private float movePrecision = 0.1f;
    /// <summary> Анимационная кривая.</summary>
    [SerializeField] private AnimationCurve curve;
    /// <summary> Трансформа наставника.</summary>
    private RectTransform rectTransform;
    /// <summary> Текущее положение.</summary>
    private Vector3 current => rectTransform.position;
    /// <summary> Пройденное время.</summary>
    private float elapsedTime;

    /// <summary> Инициализация класса.</summary>
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary> Передвинуть наставника.</summary>
    public void MoveBat(Vector3 begin, Vector3 end, float time)
    {
        StartCoroutine(Move(begin, end, time));
    }

    /// <summary> Корутина передвижения.</summary>
    private IEnumerator Move(Vector3 begin, Vector3 end, float time)
    {
        elapsedTime = 0f;
        rectTransform.position = begin;
        while (Vector3.Distance(current, end) > movePrecision)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / time;
            rectTransform.position = Vector3.Lerp(begin, end, curve.Evaluate(t));
            yield return null;
        }
    }
}
