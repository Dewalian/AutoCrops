using UnityEngine;

[CreateAssetMenu (menuName = "Scriptables/House")]
public class HouseSO : ScriptableObject
{
    public string houseName;
    public int unlockCost;
    public int upgradeCost;
    public int costIncrement;
    public int cd;
    public int cdDecrement;
}
