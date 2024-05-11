using UnityEngine;
using UnityEngine.UI;

/// <summary>Класс, отвечающий за...</summary>
public class ToggleSwitchMaterial : ToggleSwitch
{
    [Space]
    [SerializeField] private Image handleImage;
    [SerializeField] private Material handleMaterial;
    [SerializeField] private Texture2D handleOnTexture;
    [SerializeField] private Texture2D handleOffTexture;

    private Material localCopyOfHandleMaterial;

    private bool isHandleImageNotNull;

    private bool isHandleMaterialNotNull;

    protected override void OnValidate()
    {
        base.OnValidate();

        SetupHandleMaterial();
        SetupHandleTexture();
        TransitionImages();
    }

    private void SetupHandleTexture()
    {
        if (handleOnTexture == null)
        {
            Debug.Log("No texture ON here.", this);
            return;
        }
        handleImage.material.SetTexture("_TextureOn", handleOnTexture);


        if (handleOffTexture == null)
        {
            Debug.Log("No texture OFF here.", this);
            return;
        }
        handleImage.material.SetTexture("_TextureOff", handleOffTexture);
    }

    protected override void Awake()
    {
        base.Awake();

        isHandleMaterialNotNull = handleImage.material != null;
        isHandleImageNotNull = handleImage != null;

        SetupHandleMaterial();
        SetupHandleTexture();
        TransitionImages();
    }

    private void SetupHandleMaterial()
    {
        localCopyOfHandleMaterial = new Material(handleMaterial);

        if (isHandleImageNotNull)
            handleImage.material = localCopyOfHandleMaterial;
    }

    private void OnEnable()
    {
        transitionEffect += TransitionImages;
    }

    private void OnDisable()
    {
        transitionEffect -= TransitionImages;
    }

    private void TransitionImages()
    {
        if (isHandleImageNotNull && isHandleMaterialNotNull)
            handleImage.material.SetFloat("_MixValue", sliderValue);
    }
}
