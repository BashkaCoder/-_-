using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>Класс, отвечающий за...</summary>
public class SwitchSprites : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Material material;
    [SerializeField] private Texture2D onTexture;
    [SerializeField] private Texture2D offTexture;


    [SerializeField, Range(0, 1f)] private float spriteValue;

    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine animationSliderCoroutine;

    private Material localCopyOfMaterial;

    private bool isImageNotNull;

    private bool isMaterialNotNull;


    private Action transitionEffect;

    public bool IsActive { get; private set; }


    private void OnValidate()
    {
        SetupMaterial();
        SetupTexture();
        TransitionImages();
    }


    private void Awake()
    {
        isMaterialNotNull = image.material != null;
        isImageNotNull = image != null;

        SetupMaterial();
        SetupTexture();
        TransitionImages();
    }


    private void SetupTexture()
    {
        if (onTexture == null)
        {
            Debug.Log("No texture ON here.", this);
            return;
        }
        image.material.SetTexture("_TextureOn", onTexture);


        if (offTexture == null)
        {
            Debug.Log("No texture OFF here.", this);
            return;
        }
        image.material.SetTexture("_TextureOff", offTexture);
    }

    private void SetupMaterial()
    {
        localCopyOfMaterial = new Material(material);

        if (isImageNotNull)
            image.material = localCopyOfMaterial;
    }

    private void TransitionImages()
    {
        if (isImageNotNull && isMaterialNotNull)
            image.material.SetFloat("_MixValue", spriteValue);
    }

    private IEnumerator AnimateSlider()
    {
        //GetComponent<Button>().interactable = false;
        float startValue = spriteValue;
        float endValue = IsActive ? 1 : 0;

        float time = 0;
        if (animationDuration > 0)
        {
            while (time < animationDuration)
            {
                time += Time.deltaTime;

                float lerpFactor = slideEase.Evaluate(time / animationDuration);

                spriteValue = Mathf.Lerp(startValue, endValue, lerpFactor);

                transitionEffect?.Invoke();

                yield return null;
            }
        }
        spriteValue = endValue;
        //GetComponent<Button>().interactable = true;
    }

    private void OnEnable()
    {
        transitionEffect += TransitionImages;
    }

    private void OnDisable()
    {
        transitionEffect -= TransitionImages;
    }

    public void OnButtonClick()
    {
        SetStateAndStartAnimation(!IsActive);
    }
    private void SetStateAndStartAnimation(bool state)
    {
        IsActive = state;

        if (animationSliderCoroutine != null)
            StopCoroutine(animationSliderCoroutine);

        animationSliderCoroutine = StartCoroutine(AnimateSlider());
    }
}
