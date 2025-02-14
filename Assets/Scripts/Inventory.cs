using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxCapacity;
    [SerializeField] private List<Crop> crops;
    [SerializeField] private Marker marker;
    private Crop selectedCrop;
    private PlayerInput playerInput;
    public Action<Crop> OnAddCrop;

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
    }

    private void OnDisable()
    {
        playerInput.Item.SelectFirst.Disable();
        playerInput.Item.SelectSecond.Disable();
        playerInput.Item.SelectThird.Disable();
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

    private void SelectCrop(int index)
    {
        if(selectedCrop == crops[index]){
            selectedCrop = null;
            marker.SetCrop(null);

            return;
        }

        selectedCrop = crops[index];
        marker.SetCrop(selectedCrop);
    }
}
