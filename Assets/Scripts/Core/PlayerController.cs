using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EnemyIndicators EnemyIndicators;
    public GameProxy GameProxy;
    public MainIndicators MainIndicators;
    public Game Game;
    public AudioSource AudioSource;
    public AudioClip SoundForAttack;
    public float HitpointsMax;
    public float Regeneration;
    private Enemy _enemy;
    public void OnEnable()
    {
        HitpointsMax = MainIndicators.Hitpoints;
        GameProxy.SpawnMonsterEvent += OnMonster;
        GameProxy.EndGameEvent += OnNewGame;
    }
    public void OnNewGame()
    {
        HitpointsMax = MainIndicators.Hitpoints;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HealPlayer(MainIndicators.Heal);
        }
    }
    public void OnMonster(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.ClickEvent += OnMonsterClicked;
        _enemy.AttackEvent += AttackPlayer;
    }
    public void OnDisable()
    {
        GameProxy.SpawnMonsterEvent -= OnMonster;
        _enemy.ClickEvent -= OnMonsterClicked;
        _enemy.AttackEvent -= AttackPlayer;
    }
    public void OnMonsterClicked()
    {
        int crit = Random.Range(0, 100);
        int damage = MainIndicators.Damage;
        damage = (int)((0.8 + Random.value * 0.2) * damage);
        if (crit > 50)
        {
            damage = 0;
            _enemy.GetComponent<Animator>().SetTrigger("Dodge");
        }
        else
        {
            if (crit < MainIndicators.StartCritChance)
            {
                damage *= 3;
            }
            _enemy.GetComponent<Animator>().SetTrigger("Hit");
        }
        GameProxy.EditHPMonster(damage);
        TurnSoundAttack();
        _enemy.Character.Health.Damage(damage);
    }
    private void TurnSoundAttack()
    {
        AudioSource.PlayOneShot(SoundForAttack);
    }
    public void AttackPlayer(float value)
    {
        GameProxy.EditHpPlayer(value);
        HitpointsMax = Mathf.Max(0f, HitpointsMax - value);
        if (HitpointsMax <= 0)
        {
            GameProxy.EndGame();
        }
    }
    public void HealPlayer(int value)
    {
        GameProxy.EditHpPlayer(-MainIndicators.Heal);
        HitpointsMax = Mathf.Min(MainIndicators.Hitpoints, HitpointsMax + value);
    }
}
