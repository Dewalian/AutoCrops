using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxCapacity;
    [SerializeField] private List<Crop> crops;
    [SerializeField] private Marker marker;
    private Crop selectedCrop;
    private PlayerInput playerInput;
    public Action<Crop> OnAddCrop;
    public Action<int> OnSelectCrop;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Item.SelectFirst.performed += (input) => SelectCrop(0);
        playerInput.Item.SelectFirst.Enable();
        
        playerInput.Item.SelectSecond.performed += (input) => SelectCrop(1);
        playerInput.Item.SelectSecond.Enable();

        playerInput.Item.SelectThird.performed += (input) => SelectCrop(2);
        playerInput.Item.SelectThird.Enable();

        playerInput.Item.SelectFourth.performed += (input) => SelectCrop(3);
        playerInput.Item.SelectFourth.Enable();

        playerInput.Item.SelectFifth.performed += (input) => SelectCrop(4);
        playerInput.Item.SelectFifth.Enable();

        playerInput.Item.SelectSixth.performed += (input) => SelectCrop(5);
        playerInput.Item.SelectSixth.Enable();
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
        foreach(Crop c in crops)
        {
            OnAddCrop?.Invoke(c);
        }
    }

    private void AddCrop(Crop crop)
    {
        crops.Add(crop);
        OnAddCrop?.Invoke(crop);
    }

    public void SelectCrop(int index)
    {
        if(selectedCrop == crops[index]){
            selectedCrop = null;
            marker.SetCrop(null);
        }
        else{
            selectedCrop = crops[index];
            marker.SetCrop(selectedCrop);
        }

        OnSelectCrop?.Invoke(index);
    }
}
