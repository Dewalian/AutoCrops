using System;
using System.Collections.Generic;
using UnityEngine;

public class FertilizeLine : FertilizeAbility
{
    [SerializeField] private bool horizontal;
    [SerializeField] private bool vertical;

    protected override void SetTargetTiles()
    {
        if(horizontal){
            SetHorizontalLine();
        }

        if(vertical){
            SetVerticalLine();
        }
    }

    private void SetHorizontalLine()
    {
        for(int x=0; x<area.width; x++){
            Vector3Int targetTile = new Vector3Int(x, currTile.y, 0);
            if(targetTile == currTile){
                continue;
            }

            targetTiles.Add(targetTile);
        }

        Debug.Log(targetTiles.Count);
    }

    private void SetVerticalLine()
    {
        for(int y=0; y<area.height; y++){
            Vector3Int targetTile = new Vector3Int(currTile.x, y, 0);
            if(targetTile == currTile){
                continue;
            }

            targetTiles.Add(targetTile);
        }
    }
}
