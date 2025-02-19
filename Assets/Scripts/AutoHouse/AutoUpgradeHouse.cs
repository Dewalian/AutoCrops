using System;
using UnityEngine;

public class AutoUpgradeHouse : AutoHouse
{
    [SerializeField] private GameObject[] autoHouses;
    [SerializeField] private Fertilizer fertilizer;
    [SerializeField] private AutoHouseController autoHouseController;
    private int[] upgradeCosts = new int[5];

    protected override void Start()
    {
        SetStats();
    }

    protected override void Update()
    {
        base.Update();
        UpdateUpgradeCosts();
    }

    private void UpdateUpgradeCosts()
    {
        for(int i=0; i<4; i++){
            AutoHouse autoHouse = autoHouses[i].GetComponent<AutoHouse>();
            upgradeCosts[i] = autoHouse.GetStats().upgradeCost;
        }

        upgradeCosts[4] = fertilizer.GetUpgradeCost();
    }

    protected override void Automation()
    {
        if(!onCD){
            onCD = true;
            int cheapestUpgrade = upgradeCosts[0];
            int cheapestIndex = 0;
            bool canUpgrade = true;

            for(int i=0; i<upgradeCosts.Length; i++){
                if(upgradeCosts[i] < cheapestUpgrade){
                    cheapestUpgrade = upgradeCosts[i];
                    cheapestIndex = i;
                }
            }

            if(cheapestIndex == 4){
                fertilizer.Upgrade();
                canUpgrade = fertilizer.CanUpgrade();
            }
            else{
                autoHouseController.UpgradeHouse(cheapestIndex);
                AutoHouse autoHouse = autoHouses[cheapestIndex].GetComponent<AutoHouse>();
                canUpgrade = autoHouse.CanUpgrade();
            }

            if(canUpgrade){
                StartCoroutine(AutomationCD());
            }
            else{
                onCD = false;
            }
        }
    }
}
