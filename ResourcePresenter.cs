using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



/// <summary>Класс, отвечающий за...</summary>
public class ResourcePresenter : MonoBehaviour
{

    public ResourceData resourceData;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image resourceImage;
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private TMP_Text daysRemainingText;
    [SerializeField] private TMP_Text resourceConsumption;
    [SerializeField] private TMP_Text period;
    [SerializeField] private Image stateImage;
    private int consumtion = 0;
    private int daysRemaining = 0;


    private void Start()
    {
        UpdateResourceFields();
    }

    public void SetResourceGrowth(int growthValue)
    {
        daysRemaining = growthValue;
        daysRemainingText.text = growthValue.ToString();
        UpdateState();
    }
    public void SetResourceConsumption(int consum)
    {
        consumtion = consum;
        resourceConsumption.text = consum.ToString();
        UpdateState();
    }

    public void SetGraph()
    {
        return;
    }


    public void UpdateState()
    {
        int delta = daysRemaining + consumtion;
        if (delta >= 0)
        {
            if (delta == 0)
            {
                stateImage.color = Color.yellow;
            }
            else
            {
                stateImage.color = Color.green;
            }
        }
        else
        {
            stateImage.color = Color.red;
        }
    }

    private void UpdateResourceFields()
    {
        nameText.text = resourceData.Name;
        resourceImage.sprite = resourceData.Icon;
        amountText.text = resourceData.StackSize.ToString();
        //resourceConsumption.text = consumtion.ToString();
    }


    private void OnEnable()
    {

        if (resourceData != null)
        {
            resourceData.OnResourceFieldsChanged += UpdateResourceFields;
            UpdateResourceFields();
        }
    }

    private void OnDisable()
    {
        resourceData.OnResourceFieldsChanged -= UpdateResourceFields;
    }
    public void OnMoreButtonClick()
    {
        StoragePresenter.Instance.DisplayResourceDetails(resourceData);
    }
}
