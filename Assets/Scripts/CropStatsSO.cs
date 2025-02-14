using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Crop Stats")]
public class CropStatsSO : ScriptableObject
{
    public string cropName;
    public Sprite seedIcon;
    public Sprite[] icons;
    public Sprite readyIcon;
    public float time;
    public float basePrice;
}
