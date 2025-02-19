using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AutoHouseUI : MonoBehaviour
{
    [SerializeField] private AutoHouseController autoHouseController;
    [Serializable]
    private class HouseButton
    {
        public GameObject lockedButton;
        public GameObject unlockedButton;
        public TMP_Text levelText;
        public TMP_Text upgradeCostText;
        public TMP_Text freqPerSecText;
        public HouseSO baseStats;
    }

    [SerializeField] private HouseButton[] houseButtons;

    private void OnEnable()
    {
        autoHouseController.OnUnlockHouse += UnlockHouse;
        autoHouseController.OnUpgradeHouse += SetStats;
    }

    private void OnDisable()
    {
        autoHouseController.OnUnlockHouse -= UnlockHouse;
        autoHouseController.OnUpgradeHouse -= SetStats;
    }

    private void Start()
    {
        ShowCost(0);

        for(int i=0; i<houseButtons.Length; i++){
            HouseButton houseButton = houseButtons[i];
            SetStats(i, 1, houseButton.baseStats.upgradeCost, houseButton.baseStats.cd);
        }
    }

    private void SetStats(int index, int level, int upgradeCost, float cd)
    {
        HouseButton houseButton = houseButtons[index];

        houseButton.levelText.text = "Level " + level.ToString();
        houseButton.upgradeCostText.text = "$" + upgradeCost.ToString();
        houseButton.freqPerSecText.text = (1 / cd).ToString("F2") + "/s";
    }

    private void UnlockHouse(int index)
    {
        HouseButton houseButton = houseButtons[index];

        houseButton.lockedButton.SetActive(false);
        houseButton.unlockedButton.SetActive(true);

        ShowCost(index + 1);
    }

    private void ShowCost(int index)
    {
        if(index >= houseButtons.Length){
            return;
        }

        HouseButton houseButton = houseButtons[index];

        TMP_Text cost = houseButton.lockedButton.GetComponentInChildren<TMP_Text>();
        cost.text = "$" + houseButton.baseStats.unlockCost.ToString();
    }
}
