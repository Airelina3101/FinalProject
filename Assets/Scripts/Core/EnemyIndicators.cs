using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyIndicators")]
public class EnemyIndicators : ScriptableObject
{
    public float BaseHealth = 1000;
    public int StartLevel = 0;
    public int Money = 10;
    public float HealthIncreasePerLevel = 1.07F;
    public float MoneyIncreasePerLevel = 2.5F;
    public float StartDamage = 10;

    public int Level => StartLevel + _additionalLevel;
    public float Health => BaseHealth * Mathf.Pow(HealthIncreasePerLevel, Level);
    public int AvailableMoney => (int)(Money + Level * MoneyIncreasePerLevel);
    private int _additionalLevel = 0;

    public event Action MonsterAddLevelEvent;

    public void IncreaseLevel()
    {
        _additionalLevel++;
        MonsterAddLevelEvent?.Invoke();        
    }
    public void ClearAll()
    {
        _additionalLevel = 0;
    }
}
