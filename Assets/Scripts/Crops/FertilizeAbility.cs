using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Crop))]
public abstract class FertilizeAbility : MonoBehaviour
{
    protected Crop crop;
    protected Area area;
    protected Vector3Int currTile;
    protected List<Vector3Int> targetTiles = new List<Vector3Int>();
    [SerializeField] protected FertilizerSO fertilizerStats;
    [SerializeField] private bool activateWhenPlanted;
    [SerializeField] private bool activateWhenHarvested;
    [SerializeField] private bool activateWhenFertilized;

    private void Awake()
    {
        crop = GetComponent<Crop>();
    }

    private void OnEnable()
    {
        if(activateWhenFertilized){
            crop.OnFertilized += Activate;
        }
    }

    private void OnDisable()
    {
        if(activateWhenHarvested){
            Activate();
        }
    }

    private void Start()
    {
        SetArea();
        SetTile();

        if(activateWhenPlanted){
            Activate();
        }
    }

    private void SetArea()
    {
        area = crop.GetArea();
    }

    protected virtual void SetTile()
    {
        currTile = crop.GetTile();
    }

    protected abstract void SetTargetTiles();

    protected virtual void Activate()
    {
        SetTargetTiles();
        foreach(Vector3Int tile in targetTiles){
            bool isInBound = area.CheckBound(tile);
            if(!isInBound){
                continue;
            }

            Crop crop = area.GetSoil(tile).crop;
            if(crop != null){
                FertilizerBulletPool.instance.Spawn(currTile, crop.GetTile());
            }
        }
        targetTiles.Clear();
    }
}
