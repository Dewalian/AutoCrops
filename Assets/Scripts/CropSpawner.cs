using UnityEngine;
using UnityEngine.Pool;

public class CropSpawner : Item
{
    private Crop crop;
    private IObjectPool<Crop> cropPool;

    public void SetCrop(Crop crop, IObjectPool<Crop> cropPool)
    {
        this.crop = crop;
        this.cropPool = cropPool;
    }
    
    public override void Activate(Vector3Int tile)
    {
        base.Activate(tile);

        SoilState dirtState = area.GetSoil(tile).soilState;
        
        if(dirtState == SoilState.Ready)
        {
            Crop crop = area.GetSoil(tile).crop;
            cropPool.Release(crop);
        }
        else if(crop == null)
        {
            Debug.Log("no crop selected");
        }
        else if(dirtState == SoilState.Occupied){
            Debug.Log("occupied");
        }
        else{
            cropPool.Get();
        }
    }
}
