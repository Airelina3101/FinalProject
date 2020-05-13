using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action DieEvent;
    public MainIndicators MainIndicators;
    public GameProxy GameProxy;
    public float HitpointMax;

    public float Hitpoints { get; private set; }
    private void OnEnable()
    {
        Hitpoints = HitpointMax;
    }
    public void OnMouseDown()
    {
        Damage(MainIndicators.Damage);
    }

    public void Damage(float value)
    {
        Hitpoints = Mathf.Max(0f, Hitpoints - value);
        GameProxy.WoundEvent(Hitpoints);
        if (Hitpoints <= 0f)
        {
            DieEvent?.Invoke();
            GameProxy.DieMonster();
            GameObject.Destroy(gameObject);
        }
    }
    
}
