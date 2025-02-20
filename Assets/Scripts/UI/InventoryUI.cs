using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform selectUI;
    [Serializable]
    private class CropButton
    {
        public GameObject unlockedButton;
        public GameObject lockedButton;
        public CropSO stats;
        public Image icon;
    }
    [SerializeField] private CropButton[] cropButtons;
    [SerializeField] private GameObject[] buttons;
    private int currIndex;

    private void OnEnable()
    {
        inventory.OnSelectItem += SelectItem;
        inventory.OnUnlockCrop += UnlockCrop;
    }

    private void Start()
    {
        currIndex = -1;
        ShowCost(0);
    }

    private void SelectItem(int index)
    {
        if(index == currIndex){
            selectUI.gameObject.SetActive(false);
            currIndex = -1;
        }
        else{
            selectUI.gameObject.SetActive(true);
            selectUI.position = buttons[index].transform.position;
            currIndex = index;
        }
    }

    private void UnlockCrop(int index)
    {
        CropButton cropButton = cropButtons[index];

        cropButton.lockedButton.SetActive(false);
        cropButton.unlockedButton.SetActive(true);

        TMP_Text cost = cropButton.unlockedButton.GetComponentInChildren<TMP_Text>();
        cost.text = "$" + cropButton.stats.cost.ToString();

        cropButton.icon.sprite = cropButton.stats.readyIcon;

        ShowCost(index + 1);
    }

    private void ShowCost(int index)
    {
        if(index > cropButtons.Length - 1){
            return;
        }

        CropButton cropButton = cropButtons[index];

        TMP_Text cost = cropButton.lockedButton.GetComponentInChildren<TMP_Text>();
        cost.text = "$" + cropButton.stats.unlockCost.ToString();
    }
}
