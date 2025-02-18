using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance = null;
    public int gold;
    public Action OnAddGold;

    private void Awake()
    {
        instance = this;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnAddGold?.Invoke();
    }
}
