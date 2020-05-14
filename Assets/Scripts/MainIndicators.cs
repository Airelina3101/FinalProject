using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MainIndicators")]
public class MainIndicators : ScriptableObject
{
    public int DamageStartLevel = 0;
    public int BaseDamage = 100;
    public int BaseDamagePrice = 10;
    public double DamagePricePerLevel = 1.07F;
    public int DamagePerLevel = 3;
    public double StartScores = 100;
    public int StartCritDamage = 20;
        
    public event Action<double> AddScoreEvent;
    public double Scores { get; set; }

    private int _additionalLevel;
    public int Level => DamageStartLevel + _additionalLevel;
    public double DamagePrice => Math.Round(BaseDamagePrice * Math.Pow(DamagePricePerLevel, Level));
    public int Damage => BaseDamage + Level * DamagePerLevel;

    public event Action AddMainDamageEvent;
    public event Action NewGameEvent;
    public void ClearAll()
    {
        Scores = StartScores;
        _additionalLevel = 0;
        AddScoreEvent?.Invoke(Scores);
    }
    public void IncreaseLevel()
    {
        _additionalLevel++;
        AddMainDamageEvent?.Invoke();
    }
    public void AddGold(double value)
    {
        Scores += value;

        AddScoreEvent?.Invoke(value);
    }
}
