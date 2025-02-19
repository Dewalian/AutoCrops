using System.Collections.Generic;
using UnityEngine;

public class FertilizeRandom : FertilizeAbility
{
    [SerializeField] private int frequency;
    private List<Vector3Int> tiles = new List<Vector3Int>();

    protected override void SetTile()
    {
        base.SetTile();

        for(int x=0; x<area.width; x++){
            for(int y=0; y<area.height; y++){
                tiles.Add(new Vector3Int(x, y, 0));
            }
        }
    }

    protected override void SetTargetTiles()
    {
        targetTiles.Clear();
        List<Vector3Int> occupiedTiles = new List<Vector3Int>();

        foreach(Vector3Int tile in tiles){
            if(area.GetSoil(tile).soilState == SoilState.Occupied && tile != currTile){
                occupiedTiles.Add(tile);
            }
        }

        for(int i=0; i<frequency; i++){
            if(occupiedTiles.Count <= 0){
                break;
            }

            int randomIndex = Random.Range(0, occupiedTiles.Count);
            Vector3Int randomTile = occupiedTiles[randomIndex];

            targetTiles.Add(randomTile);
            occupiedTiles.Remove(randomTile);
        }
    }
}
