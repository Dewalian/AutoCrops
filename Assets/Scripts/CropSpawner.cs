using UnityEngine;

public class CropSpawner : Item
{
    private Crop crop;

    public void SetCrop(Crop crop)
    {
        this.crop = crop;
    }
    
    public override void Activate(Vector3Int tile)
    {
        base.Activate(tile);

        SoilState dirtState = area.GetSoil(tile).soilState;
        
        if(dirtState == SoilState.Ready)
        {
            GameObject cropObj = area.GetSoil(tile).crop.gameObject;
            Destroy(cropObj);
        }
        else if(crop == null)
        {
            Debug.Log("no crop selected");
        }
        else if(dirtState == SoilState.Occupied){
            Debug.Log("occupied");
        }
        else if(GoldManager.instance.gold < crop.GetStats().cost){
            Debug.Log("Not enough money");
        }
        else{
            Crop cropObj = Instantiate(crop, area.GetTileCenter(tile), Quaternion.identity);
            cropObj.Init(area, tile);
        }
    }
}
