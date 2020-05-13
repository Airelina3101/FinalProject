using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayHUD : MonoBehaviour
{
    public event Action Click;
    public GameProxy GameProxy;
    public MainIndicators MainIndicators;
    public EnemyIndicators EnemyIndicators;
    public Slider HpBar;
    public Text GoldText;
    public Text MainDamageText;

    private void Awake()
    {
        GoldText.text = MainIndicators.Scores.ToString();
        MainDamageText.text = MainIndicators.BaseDamage.ToString();
        HPBarUpdate();
        GameProxy.EditHealthBarEvent += OnHPBarEdit;
        GameProxy.NeedSpawnMonsterEvent += HPBarUpdate;
        MainIndicators.AddScoreEvent += OnScoreAdded;
        MainIndicators.AddMainDamageEvent += OnMainDamageAdded;              
    }
    private void OnScoreAdded(double delta)
    {
        GoldText.text = MainIndicators.Scores.ToString();
    }
    private void OnMainDamageAdded()
    {
        MainDamageText.text = MainIndicators.Damage.ToString();
    }
    public void OnIncreaseDamageButton()
    {
        if (MainIndicators.Scores - MainIndicators.DamagePrice >= 0)
        {
            MainIndicators.AddGold(-MainIndicators.DamagePrice);
            MainIndicators.IncreaseLevel();
        }
    }
    public void HPBarUpdate()
    {
        HpBar.maxValue = EnemyIndicators.Health;
        HpBar.value = EnemyIndicators.Health;
    }
    public void OnHPBarEdit(float delta)
    {
        HpBar.value = delta;
    }
}
