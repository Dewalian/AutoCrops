using UnityEngine;

public class AutoFertilizeHouse : AutoHouse
{
    [SerializeField] private Fertilizer fertilizer;

    protected override void Automation()
    {
        if(!onCD){
            onCD = true;
            Vector3Int tile = SearchRandomTile(SoilState.Occupied);

            if(tile == new Vector3Int(-1 ,-1 ,-1)){
                onCD = false; 
                return;
            }

            autoMarker.SetItem(fertilizer);
            autoMarker.ActivateItem(tile);

            StartCoroutine(AutomationCD());
        }
    }
}
