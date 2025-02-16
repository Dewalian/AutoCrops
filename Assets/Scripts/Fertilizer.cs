using UnityEngine;

public class Fertilizer : Item
{
    [SerializeField] private float timerBoost;
    [SerializeField] private float qualityBoost;

    public override void Activate(Vector3Int tile)
    {
        Crop crop = area.GetSoil(tile).crop;
        if(crop != null){
            crop.Fertilize(timerBoost, qualityBoost);
        }
    }
}
