using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayHUD : MonoBehaviour
{
    public GameProxy GameProxy;
    public MainIndicators MainIndicators;
    public EnemyIndicators EnemyIndicators;    
    public Slider HpBarEnemy; 
    public Slider HpBarPlayer;
    public Text GoldText;
    [Header("AttackButton")]
    public Text LvlDamageValueText;
    public Text CostImprovementDamageText;
    [Header("HitpointsButton")]
    public Text LvlHitpointsValueText;
    public Text CostImprovementHitpointsText;
    [Header("HealButton")]
    public Text LvlHealValueText;
    public Text CostImprovementHealText;
    [Header("HealButton")]
    public AudioSource AudioSource;
    public AudioClip OnClickButton;

    private Enemy _enemy;
    private void Awake()
    {
        gameObject.SetActive(false);
        GameProxy.NewGameEvent += OnNewGame;
        GameProxy.EndGameEvent += OnEndGame;

        MainIndicators.EditGoldEvent += OnGoldEdit;
        GameProxy.EditHPMonsterEvent += OnHPBarEnemyEdit;
        GameProxy.EditHPPlayerEvent += OnHPBarPlayerEdit;
        MainIndicators.EditMainDamageEvent += OnMainDamageEdit;
        MainIndicators.EditMainHitpointsEvent += OnMainHitpointsEdit;
        MainIndicators.EditMainHealEvent += OnMainHealEdit;
        GameProxy.SpawnMonsterEvent += OnMonsterSpawn;
    }
    private void OnDestroy()
    {
        GameProxy.NewGameEvent -= OnNewGame;
        GameProxy.EndGameEvent -= OnEndGame;

        MainIndicators.EditGoldEvent -= OnGoldEdit;
        GameProxy.EditHPMonsterEvent -= OnHPBarEnemyEdit;
        GameProxy.EditHPPlayerEvent -= OnHPBarPlayerEdit;
        MainIndicators.EditMainDamageEvent -= OnMainDamageEdit;
        MainIndicators.EditMainHitpointsEvent -= OnMainHitpointsEdit;
        MainIndicators.EditMainHealEvent -= OnMainHealEdit;
        GameProxy.SpawnMonsterEvent -= OnMonsterSpawn;
    }
    private void OnNewGame()
    {
        HpBarPlayer.maxValue = MainIndicators.Hitpoints;
        HpBarPlayer.value = MainIndicators.Hitpoints;
        gameObject.SetActive(true);
    }
    private void OnEndGame()
    {
        gameObject.SetActive(false);
    }
    private void OnGoldEdit(float delta)
    {
        GoldText.text = delta.ToString();
    }
    public void OnHPBarEnemyEdit(float delta) 
    {
        HpBarEnemy.value -= delta;
    }
    public void OnHPBarPlayerEdit(float delta) 
    {
        HpBarPlayer.value -= delta;
    }
    private void OnMainDamageEdit(int lvl,float price)
    {
        CostImprovementDamageText.text = price.ToString();
        LvlDamageValueText.text = lvl.ToString();
    }
    private void OnMainHitpointsEdit(int lvl, float price, int hitpoints)
    {
        HpBarPlayer.maxValue = hitpoints;
        CostImprovementHitpointsText.text = price.ToString();
        LvlHitpointsValueText.text = lvl.ToString();
    }
    private void OnMainHealEdit(int lvl, float price)
    {
        CostImprovementHealText.text = price.ToString();
        LvlHealValueText.text = lvl.ToString();
    }
    public void OnMonsterSpawn(Enemy enemy) 
    {
        _enemy = enemy;
        HpBarEnemy.maxValue = _enemy.Character.Health.HitpointMax;
        HpBarEnemy.value = _enemy.Character.Health.Hitpoints;
        enemy.Character.Health.HitpointsChangeEvent += OnHitpointsChanged;
        enemy.Character.Health.DieEvent += OnDead;
    }
    private void OnHitpointsChanged(float hitpoints)
    {        
        HpBarEnemy.value = hitpoints;
    }
    private void OnDead()
    {
        _enemy.Character.Health.HitpointsChangeEvent -= OnHitpointsChanged;
        _enemy.Character.Health.DieEvent -= OnDead;
        _enemy = null;
    }
    public void OnIncreaseDamageButton()
    {
        if (MainIndicators.Scores - MainIndicators.DamagePrice >= 0)
        {
            AudioSource.PlayOneShot(OnClickButton);
            MainIndicators.AddGold(-MainIndicators.DamagePrice);
            MainIndicators.IncreaseLevelDamage();
        }
    }
    public void OnIncreaseHitpointsButton()
    {
        if (MainIndicators.Scores - MainIndicators.HitpointsPrice >= 0)
        {
            AudioSource.PlayOneShot(OnClickButton);
            MainIndicators.AddGold(-MainIndicators.HitpointsPrice);
            MainIndicators.IncreaseLevelHitpoints();
        }
    }
    public void OnIncreaseHealButton()
    {
        if (MainIndicators.Scores - MainIndicators.HealPrice >= 0)
        {
            AudioSource.PlayOneShot(OnClickButton);
            MainIndicators.AddGold(-MainIndicators.HealPrice);
            MainIndicators.IncreaseLevelHeal();
        }
    }
}
