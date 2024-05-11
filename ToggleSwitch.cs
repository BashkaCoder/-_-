using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>Класс, отвечающий за...</summary>
public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Range(0, 1f)] protected float sliderValue;
    public bool CurrentValue { get; private set; }

    private Slider slider;

    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine animationSliderCoroutine;


    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;


    private ToggleSwitchGroupManager toggleSwitchGroupManager;

    protected Action transitionEffect;

    protected virtual void OnValidate()
    {
        SetupToggleComponents();

        slider.value = sliderValue;
    }

    private void SetupToggleComponents()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.Log("No slider here.", this);
            return;
        }
        slider.interactable = false;

        ColorBlock sliderColors = slider.colors;
        sliderColors.disabledColor = Color.white;
        slider.colors = sliderColors;
        slider.transition = Selectable.Transition.None;
    }

    private void SetupManager(ToggleSwitchGroupManager manager)
    {
        toggleSwitchGroupManager = manager;
    }

    protected virtual void Awake()
    {
        SetupToggleComponents();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Toggling();
    }

    public void SetDefaultState()
    {
        CurrentValue = false;

        slider.value = sliderValue = 0;
        transitionEffect?.Invoke();
    }

    private void Toggling()
    {
        if (toggleSwitchGroupManager != null)
        {
            toggleSwitchGroupManager.ToggleGroup(this);
        }
        else SetStateAndStartAnimation(!CurrentValue);
    }

    private void SetStateAndStartAnimation(bool state)
    {
        CurrentValue = state;

        if (CurrentValue)
            onToggleOn?.Invoke();
        else onToggleOff?.Invoke();

        if (animationSliderCoroutine != null)
            StopCoroutine(animationSliderCoroutine);


        animationSliderCoroutine = StartCoroutine(AnimateSlider());
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = slider.value;
        float endValue = CurrentValue ? 1 : 0;

        float time = 0;
        if (animationDuration > 0)
        {
            while (time < animationDuration)
            {
                time += Time.deltaTime;

                float lerpFactor = slideEase.Evaluate(time / animationDuration);

                slider.value = sliderValue = Mathf.Lerp(startValue, endValue, lerpFactor);

                transitionEffect?.Invoke();

                yield return null;
            }
        }
        slider.value = endValue;
    }

}
