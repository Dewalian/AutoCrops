using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class AutoHouse : MonoBehaviour
{
    [SerializeField] private Area area;
    [SerializeField] private HouseSO baseStats;
    protected HouseStats stats;
    protected Marker autoMarker;
    protected bool onCD;
    protected List<Vector3Int> tiles = new List<Vector3Int>();

    private void Awake()
    {
        autoMarker = GetComponent<Marker>();
    }

    protected virtual void Start()
    {
        SetTiles();
        SetStats();
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
        Automation();
    }

    protected void SetStats()
    {
        stats = new HouseStats(1, baseStats.upgradeCost, baseStats.cd);
    }

    public void Upgrade()
    {
        stats.upgradeCost += baseStats.costIncrement;
        stats.cd -= baseStats.cdDecrement;
        stats.level++;
    }

    protected abstract void Automation();
    protected IEnumerator AutomationCD()
    {
        yield return new WaitForSeconds(stats.cd);
        onCD = false;
    }

    protected Vector3Int SearchRandomTile(SoilState state)
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

    public HouseSO GetBaseStats()
    {
        return baseStats;
    }

    public HouseStats GetStats()
    {
        return stats;
    }
}

public class HouseStats
{
    public int level;
    public int upgradeCost;
    public float cd;
    public HouseStats(int level, int upgradeCost, float cd)
    {
        this.level = level;
        this.upgradeCost = upgradeCost;
        this.cd = cd;
    }
}