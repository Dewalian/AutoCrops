using System;
using UnityEngine;

public class AutoUpgradeHouse : AutoHouse
{
    [SerializeField] private GameObject[] autoHouses;
    [SerializeField] private GameObject fertilizer;
    private int[] upgradeCosts = new int[5];

    protected override void Start()
    {
        SetStats();
        UpdateUpgradeCosts();
    }

    private void UpdateUpgradeCosts()
    {
        for(int i=0; i<4; i++){
            AutoHouse autoHouse = autoHouses[i].GetComponent<AutoHouse>();
            upgradeCosts[i] = autoHouse.GetStats().upgradeCost;
        }

        upgradeCosts[4] = fertilizer.GetComponent<Fertilizer>().GetUpgradeCost();
    }

    protected override void Automation()
    {
        if(!onCD){
            onCD = true;
            int cheapestUpgrade = upgradeCosts[0];
            int cheapestIndex = 0;

            for(int i=0; i<upgradeCosts.Length; i++){
                if(upgradeCosts[i] < cheapestUpgrade){
                    cheapestUpgrade = upgradeCosts[i];
                    cheapestIndex = i;
                }
            }

            if(cheapestIndex == 4){
                fertilizer.GetComponent<Fertilizer>().Upgrade();
            }
            else{
                AutoHouse autoHouse = autoHouses[cheapestIndex].GetComponent<AutoHouse>();
                autoHouse.Upgrade();
            }

            UpdateUpgradeCosts();
            StartCoroutine(AutomationCD());
        }
    }
}
