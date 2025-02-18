using System;
using UnityEngine;

public class Fertilizer : Item
{
    [SerializeField] private int qualityBoost;
    [SerializeField] private float timeBoost;
    [SerializeField] private int qualityIncrement;
    [SerializeField] private float timeIncrement;
    [SerializeField] private int upgradeCost;
    [SerializeField] private int costIncrement;
    private int level;
    public Action<int, int, int, float> OnUpgrade;

    private void Start()
    {
        level = 1;
        OnUpgrade?.Invoke(level, upgradeCost, qualityBoost, timeBoost);
    }

    public override void Activate(Vector3Int tile)
    {
        Crop crop = area.GetSoil(tile).crop;
        if(crop != null){
            crop.Fertilize(timeBoost, qualityBoost);
        }
    }

    public void Upgrade()
    {
        if(GoldManager.instance.gold < upgradeCost){
            return;
        }

        qualityBoost += qualityIncrement;
        timeBoost += timeIncrement;
        upgradeCost += costIncrement;
        level++;

        GoldManager.instance.AddGold(-upgradeCost);

        OnUpgrade?.Invoke(level, upgradeCost, qualityBoost, timeBoost);
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }
}
