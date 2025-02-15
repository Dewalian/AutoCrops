using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform selectUI;
    [SerializeField] private Image[] images;
    private int currIndex;
    private int cropCount;

    private void OnEnable()
    {
        inventory.OnAddCrop += AddCrop;
        inventory.OnSelectCrop += SelectCrop;
    }

    private void Start()
    {
        currIndex = -1;
    }

    private void AddCrop(Crop crop)
    {
        cropCount++;
        Image image = images[cropCount-1];
        image.sprite = crop.GetReadyIcon();
        image.SetNativeSize();
        image.gameObject.SetActive(true);
    }

    public void SelectCrop(int index)
    {
        Debug.Log(index + " " + currIndex);
        if(index == currIndex){
            selectUI.gameObject.SetActive(false);
            currIndex = -1;
        }
        else{
            selectUI.gameObject.SetActive(true);
            selectUI.position = images[index].transform.position;
            currIndex = index;
        }
    }
}
