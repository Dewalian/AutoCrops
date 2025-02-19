using System;
using UnityEngine;

public class Fertilizer : Item
{
    [SerializeField] private FertilizerSO stats;
    [SerializeField] private int maxLevel;
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
        UpdateStats();
    }

    public override void Activate(Vector3Int tile)
    {
        Soil soil = area.GetSoil(tile);
        if(soil.crop != null && soil.soilState == SoilState.Occupied){
            soil.crop.Fertilize(qualityBoost, timeBoost);
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
        UpdateStats();
    }

    private void UpdateStats()
    {
        stats.qualityBoost = qualityBoost;
        stats.timeBoost = timeBoost;
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }

    public bool CanUpgrade()
    {
        if(level >= maxLevel){
            return false;
        }

        if(GoldManager.instance.gold < upgradeCost){
            return false;
        }

        return true;
    }
}
