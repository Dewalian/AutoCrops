using TMPro;
using UnityEngine;

public class FertilizerUI : MonoBehaviour
{
    [SerializeField] private Fertilizer fertilizer;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private TMP_Text qualityBoostText;
    [SerializeField] private TMP_Text timeBoostText;
    private int level;

    private void OnEnable()
    {
        fertilizer.OnUpgrade += SetStats;
    }

    private void SetStats(int level, int upgradeCost, int qualityBoost, float timeBoost)
    {
        this.level++;

        if(this.level >= fertilizer.GetMaxLevel()){
            levelText.text = "Level Max";
            upgradeCostText.text = "Max";
        }
        else{
            levelText.text = "Level " + level.ToString();
            upgradeCostText.text = "$" + upgradeCost.ToString();
            qualityBoostText.text = "+" + qualityBoost.ToString() + "%";
            timeBoostText.text = "+" + timeBoost.ToString() + "s";
        }

    }
}
