using System;
using UnityEngine;

public class AutoHouseController : MonoBehaviour
{
    [SerializeField] private GameObject[] autoHouses;
    private int houseCount;
    public Action<int> OnUnlockHouse;
    public Action<int, int, int, float> OnUpgradeHouse;

    public void UnlockHouse(int index)
    {
        if(index > houseCount){
            return;
        }

        AutoHouse autoHouse = autoHouses[index].GetComponent<AutoHouse>();
        int unlockCost = autoHouse.GetBaseStats().unlockCost;
        if(GoldManager.instance.gold < unlockCost){
            return;
        }

        autoHouse.gameObject.SetActive(true);
        GoldManager.instance.AddGold(-unlockCost);
        houseCount++;

        OnUnlockHouse?.Invoke(index);
    }

    public void UpgradeHouse(int index)
    {
        AutoHouse autoHouse = autoHouses[index].GetComponent<AutoHouse>();
        HouseStats stats = autoHouse.GetStats();

        if(!autoHouse.CanUpgrade()){
            return;
        }

        GoldManager.instance.AddGold(-stats.upgradeCost);
        autoHouse.Upgrade();

        OnUpgradeHouse?.Invoke(index, stats.level, stats.upgradeCost, stats.cd);
    }

    public void ToggleHouse(int index)
    {
        if(autoHouses[index].activeSelf){
            autoHouses[index].SetActive(false);
        }
        else{
            autoHouses[index].SetActive(true);
        }
    }
}
