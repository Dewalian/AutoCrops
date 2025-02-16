using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Crop> crops;
    [SerializeField] private Marker marker;
    [SerializeField] private Item cropSpawner;
    [SerializeField] private Item fertilizer;
    private int cropCount;
    private int selectedIndex;
    private Crop selectedCrop;
    private PlayerInput playerInput;
    public Action<Crop> OnAddCrop;
    public Action<int> OnSelectItem;
    public Action<int> OnUnlockCrop;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Item.SelectFirst.performed += (input) => SelectCropSpawner(0);
        playerInput.Item.SelectFirst.Enable();
        
        playerInput.Item.SelectSecond.performed += (input) => SelectCropSpawner(1);
        playerInput.Item.SelectSecond.Enable();

        playerInput.Item.SelectThird.performed += (input) => SelectCropSpawner(2);
        playerInput.Item.SelectThird.Enable();

        playerInput.Item.SelectFourth.performed += (input) => SelectCropSpawner(3);
        playerInput.Item.SelectFourth.Enable();

        playerInput.Item.SelectFifth.performed += (input) => SelectCropSpawner(4);
        playerInput.Item.SelectFifth.Enable();

        playerInput.Item.SelectSixth.performed += (input) => SelectCropSpawner(5);
        playerInput.Item.SelectSixth.Enable();

        playerInput.Item.SelectFertilizer.performed += (input) => SelectFertilizer(6);
        playerInput.Item.SelectFertilizer.Enable();
    }

    private void OnDisable()
    {
        playerInput.Item.SelectFirst.Disable();
        playerInput.Item.SelectSecond.Disable();
        playerInput.Item.SelectThird.Disable();
        playerInput.Item.SelectFourth.Disable();
        playerInput.Item.SelectFifth.Disable();
        playerInput.Item.SelectSixth.Disable();
    }

    private void Start()
    {
        selectedIndex = -1;

        foreach(Crop c in crops)
        {
            OnAddCrop?.Invoke(c);
        }
    }

    public void SelectCropSpawner(int index)
    {
        if(index+1 > cropCount){
            return;
        }

        if(selectedIndex == index){
            selectedCrop = null;
            marker.SetItem(null);

            selectedIndex = -1;
        }
        else{
            selectedCrop = crops[index];
            marker.SetCropSpawner(cropSpawner, selectedCrop);

            selectedIndex = index;
        }

        OnSelectItem?.Invoke(index);
    }

    public void SelectFertilizer(int index)
    {
        if(selectedIndex == index){
            marker.SetItem(null);
            selectedIndex = -1;
        }
        else{
            marker.SetItem(fertilizer);
            selectedIndex = index;
        }

        OnSelectItem?.Invoke(index);
    }

    public void UnlockCrop(int index)
    {
        if(index > cropCount){
            return;
        }

        cropCount++;
        OnUnlockCrop?.Invoke(index);
    }
}
