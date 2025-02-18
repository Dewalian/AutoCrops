using TMPro;
using UnityEngine;

public class FertilizerUI : MonoBehaviour
{
    [SerializeField] private Fertilizer fertilizer;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private TMP_Text qualityBoostText;
    [SerializeField] private TMP_Text timeBoostText;

    private void OnEnable()
    {
        fertilizer.OnUpgrade += SetStats;
    }

    private void SetStats(int level, int upgradeCost, int qualityBoost, float timeBoost)
    {
        levelText.text = "Level " + level.ToString();
        upgradeCostText.text = "$" + upgradeCost.ToString();
        qualityBoostText.text = "+" + qualityBoost.ToString() + "%";
        timeBoostText.text = "+" + timeBoost.ToString() + "s";
    }
}
