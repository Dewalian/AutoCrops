using System.Collections;
using UnityEngine;

public class AutoHarvestHouse : AutoHouse
{
    protected override void Automation()
    {
        if(!onCD){
            onCD = true;
            Vector3Int tile = SearchRandomTile(SoilState.Ready);

            if(tile == new Vector3Int(-1 ,-1 ,-1)){
                onCD = false; 
                return;
            }

            StartCoroutine(AutoHarvestDelay(tile));
        }
    }

    private IEnumerator AutoHarvestDelay(Vector3Int tile)
    {
        yield return new WaitForSeconds(0.3f);
        autoMarker.ActivateItem(tile);

        StartCoroutine(AutomationCD());
    }
}
