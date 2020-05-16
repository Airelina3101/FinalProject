using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action ClickEvent;
    public event Action<float> AttackEvent;
    public Character Character;

    private void OnEnable()
    {
        Character.Health.DieEvent += OnDead;
        Character.Attack.AttactEvent += OnAttack;
    }
    private void OnDisable()
    {
        Character.Health.DieEvent -= OnDead;
        Character.Attack.AttactEvent -= OnAttack;
    }
    public void OnAttack(float damage)
    {
        GetComponent<Animator>().SetTrigger("Attack");
        AttackEvent?.Invoke(damage);
    }
    private void OnDead()
    {
        Character.MainIndicators.AddGold(Character.EnemyIndicators.AvailableMoney);
    }
    public void OnMouseDown()
    {
        ClickEvent?.Invoke();
    }
}
