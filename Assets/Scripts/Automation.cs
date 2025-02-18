using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Automation : MonoBehaviour
{
    [SerializeField] private Area area;
    [SerializeField] private HouseSO autoPlantStats;
    [SerializeField] private HouseSO autoHarvestStats;
    [SerializeField] private Inventory inventory;
    private Marker autoMarker;
    private CropSpawner cropSpawner;
    private bool autoPlantOnCD;
    private bool autoHarvestOnCD;
    private List<Vector3Int> tiles = new List<Vector3Int>();
    private List<Crop> crops;

    private void Awake()
    {
        autoMarker = GetComponent<Marker>();
        cropSpawner = GetComponent<CropSpawner>();
    }

    private void OnEnable()
    {
        inventory.OnUnlockCrop += UpdateUnlockedCrops;
    }

    private void Start()
    {
        UpdateUnlockedCrops(0);
        SetTiles();
    }

    private void SetTiles()
    {
        for(int x=0; x<area.width; x++){
            for(int y=0; y<area.height; y++){
                tiles.Add(new Vector3Int(x, y, 0));
            }
        }
    }

    private void Update()
    {
        AutoPlant();
        AutoHarvest();
    }

    private void AutoPlant()
    {
        if(!autoPlantOnCD && crops.Count > 0){
            autoPlantOnCD = true;
            Vector3Int tile = SearchRandomTile(SoilState.Empty);

            if(tile == new Vector3Int(-1 ,-1 ,-1)){
                Debug.Log("a;sldkfj");
                autoPlantOnCD = false;
                return;
            }

            int randomIndex = Random.Range(0, crops.Count);

            autoMarker.SetCropSpawner(cropSpawner,  crops[randomIndex]);
            autoMarker.ActivateItem(tile);

            StartCoroutine(AutoPlantCD());
        }
    }

    private void AutoHarvest()
    {
        if(!autoHarvestOnCD){
            autoHarvestOnCD = true;
            Vector3Int tile = SearchRandomTile(SoilState.Ready);

            if(tile == new Vector3Int(-1 ,-1 ,-1)){
                autoHarvestOnCD = false; 
                return;
            }

            StartCoroutine(AutoHarvestDelay(tile));
        }
    }

    private IEnumerator AutoHarvestDelay(Vector3Int tile)
    {
        yield return new WaitForSeconds(0.3f);
        autoMarker.ActivateItem(tile);

        StartCoroutine(AutoHarvestCD());
    }

    private Vector3Int SearchRandomTile(SoilState state)
    {
        List<Vector3Int> tilesCopy = new List<Vector3Int>();
        
        for(int i=0; i<tiles.Count; i++){
            tilesCopy.Add(tiles[i]);
        }
        
        for(int i=0; i<tiles.Count; i++){
            int randomIndex = Random.Range(0, tilesCopy.Count);
            Vector3Int randomTile = tilesCopy[randomIndex];

            if(area.GetSoil(randomTile).soilState == state){
                return randomTile;
            }

            tilesCopy.Remove(randomTile);
        }

        return new Vector3Int(-1, -1, -1);
    }

    private IEnumerator AutoPlantCD()
    {
        yield return new WaitForSeconds(autoPlantStats.cd);
        autoPlantOnCD = false;
    }

    private IEnumerator AutoHarvestCD()
    {
        yield return new WaitForSeconds(autoHarvestStats.cd);
        autoHarvestOnCD = false;
    }

    private void UpdateUnlockedCrops(int index)
    {
        crops = inventory.GetUnlockedCrops();
    }
}
