using System.Collections.Generic;
using UnityEngine;

public class AutoPlantHouse : AutoHouse
{
    [SerializeField] private Inventory inventory;
    private List<Crop> crops;
    private CropSpawner cropSpawner;

    
    private void OnEnable()
    {
        cropSpawner = GetComponent<CropSpawner>();
        inventory.OnUnlockCrop += UpdateUnlockedCrops;
    }

    protected override void Start()
    {
        base.Start();
        UpdateUnlockedCrops(0);
    }

    protected override void Automation()
    {
        if(!onCD && crops.Count > 0){
            onCD = true;
            Vector3Int tile = SearchRandomTile(SoilState.Empty);

            if(tile == new Vector3Int(-1 ,-1 ,-1)){
                onCD = false;
                return;
            }

            int randomIndex = Random.Range(0, crops.Count);

            autoMarker.SetCropSpawner(cropSpawner,  crops[randomIndex]);
            autoMarker.ActivateItem(tile);

            StartCoroutine(AutomationCD());
        }
    }

    private void UpdateUnlockedCrops(int index)
    {
        crops = inventory.GetUnlockedCrops();
    }
}
