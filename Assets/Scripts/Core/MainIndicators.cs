using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MainIndicators")]
public class MainIndicators : ScriptableObject
{
    public int StartLevel = 0;

    public int BaseDamage = 100;
    public int DamagePerLevel = 3;
    public int BaseDamagePrice = 10;
    public float DamagePricePerLevel = 1.07F;

    public int BaseHitpoints = 200;
    public int HitpointsPerLevel = 5;
    public int BaseHitpointsPrice = 10;
    public float HitpointsPricePerLevel = 1.07F;

    public int BaseHeal = 10;
    public float HealPerLevel = 1.1F;
    public int BaseHealPrice = 10;
    public float HealPricePerLevel = 1.07F;

    public float StartScores = 100;

    public int StartCritChance = 20;    
    
    public float Scores { get; set; }

    private int _additionalLevelDamage;
    private int _additionalLevelHitpoints;
    private int _additionalLevelHeal;
    public int LevelDamage => StartLevel + _additionalLevelDamage;
    public float DamagePrice => Mathf.Round(BaseDamagePrice * Mathf.Pow(DamagePricePerLevel, LevelDamage));
    public int Damage => BaseDamage + LevelDamage * DamagePerLevel;

    public int LevelHitpoints => StartLevel + _additionalLevelHitpoints;
    public float HitpointsPrice => Mathf.Round(BaseHitpointsPrice * Mathf.Pow(HitpointsPricePerLevel, LevelHitpoints));
    public int Hitpoints => BaseHitpoints + LevelHitpoints * HitpointsPerLevel;

    public int LevelHeal => StartLevel + _additionalLevelHeal;
    public float HealPrice => Mathf.Round(BaseHealPrice * Mathf.Pow(HealPricePerLevel, LevelHeal));
    public int Heal => (int)(BaseHeal + LevelHeal * HealPerLevel);

    public event Action<float> EditGoldEvent;
    public event Action<int, float> EditMainDamageEvent;
    public event Action<int, float, int> EditMainHitpointsEvent;
    public event Action<int, float> EditMainHealEvent;

    public void ClearAll()
    {
        Scores = StartScores;
        _additionalLevelDamage = StartLevel;
        _additionalLevelHitpoints = StartLevel;
        _additionalLevelHeal = StartLevel;
        EditGoldEvent?.Invoke(Scores);
        EditMainDamageEvent?.Invoke(LevelDamage, DamagePrice);
        EditMainHitpointsEvent?.Invoke(LevelHitpoints, HitpointsPrice, Hitpoints);
        EditMainHealEvent?.Invoke(LevelHeal, HealPrice);
    }
    public void IncreaseLevelDamage()
    {
        _additionalLevelDamage++;
        EditMainDamageEvent?.Invoke(LevelDamage, DamagePrice);
    }
    public void IncreaseLevelHitpoints()
    {
        _additionalLevelHitpoints++;
        EditMainHitpointsEvent?.Invoke(LevelHitpoints, HitpointsPrice, Hitpoints);
    }
    public void IncreaseLevelHeal()
    {
        _additionalLevelHeal++;
        EditMainHealEvent?.Invoke(LevelHeal, HealPrice);
    }
    public void AddGold(float value)
    {
        Scores += value;
        EditGoldEvent?.Invoke(Scores);
    }
}
