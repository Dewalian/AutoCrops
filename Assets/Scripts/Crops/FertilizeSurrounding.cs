using System.Collections.Generic;
using UnityEngine;

public class FertilizeSurrounding : FertilizeAbility
{
    protected override void SetTargetTiles()
    {
        for(int x=-1; x<2; x++){
            for(int y=-1; y<2; y++){
                if(x == 0 && y == 0){
                    continue;
                }
                targetTiles.Add(currTile + new Vector3Int(x, y, 0));
            }
        }
    }
}
