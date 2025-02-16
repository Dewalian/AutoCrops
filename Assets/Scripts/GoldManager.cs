using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance = null;
    public float gold;
    public Action<float> OnAddGold;

    private void Awake()
    {
        instance = this;
    }

    public void AddGold(float amount)
    {
        gold += amount;
        OnAddGold?.Invoke(amount);
    }
}
