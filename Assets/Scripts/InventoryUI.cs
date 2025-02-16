using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform selectUI;
    [SerializeField] private GameObject[] unlockedFrames;
    [SerializeField] private GameObject[] lockedFrames;
    private int currIndex;

    private void OnEnable()
    {
        inventory.OnSelectItem += SelectItem;
        inventory.OnUnlockCrop += UnlockFrame;
    }

    private void Start()
    {
        currIndex = -1;
    }

    public void SelectItem(int index)
    {
        if(index == currIndex){
            selectUI.gameObject.SetActive(false);
            currIndex = -1;
        }
        else{
            selectUI.gameObject.SetActive(true);
            selectUI.position = unlockedFrames[index].transform.position;
            currIndex = index;
        }
    }

    public void UnlockFrame(int index)
    {
        lockedFrames[index].SetActive(false);
        unlockedFrames[index].SetActive(true);
    }
}
