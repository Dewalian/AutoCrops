using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Image[] images;
    private int cropCount;

    private void OnEnable()
    {
        inventory.OnAddCrop += AddCrop;
    }

    private void AddCrop(Crop crop)
    {
        cropCount++;
        Image image = images[cropCount-1];
        image.sprite = crop.GetSeedIcon();
        image.SetNativeSize();
        image.gameObject.SetActive(true);
    }
}
