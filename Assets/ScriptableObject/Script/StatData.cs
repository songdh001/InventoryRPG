using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Speed,
    ProjectileCount,
    Coin,
}

[CreateAssetMenu(fileName = "New StatData", menuName = "Stats/Charcter Stats")]

public class StatData : ScriptableObject
{
    public string characterName;
    public List<StatEntry> stats;
}

[System.Serializable]
public class StatEntry
{
    public StatType statType;
    public float baseValue;
}
