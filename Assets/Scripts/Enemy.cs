using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Character Character;
    public int Gold = 10;

    private void OnEnable()
    {
        Character.Health.DieEvent += OnDead;
    }
    private void OnDead()
    {
        Character.GameProxy.AddGold(Gold);
    }
}
