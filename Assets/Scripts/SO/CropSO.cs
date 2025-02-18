using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Crop")]
public class CropSO : ScriptableObject
{
    public string cropName;
    public Sprite seedIcon;
    public Sprite[] icons;
    public Sprite readyIcon;
    public float time;
    public int unlockCost;
    public int cost;
    public int price;
}
