using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action ChangeEvent;
    public event Action DieEvent;

    public float HitpointMax;

    public float Hitpoints { get; private set; }
    private void OnEnable()
    {
        Hitpoints = HitpointMax;
    }
    public void OnMouseDown()
    {
        if (GameObject.FindWithTag("Enemy"))
        {
            Damage(10);
        }       
        else if (GameObject.FindWithTag("Player"))
        {
            Damage(-10);
        }
    }

    public void Damage(float value)
    {
        Hitpoints = Mathf.Max(0f, Hitpoints - value);
        ChangeEvent?.Invoke();
        if (Hitpoints <= 0f)
        {
            DieEvent?.Invoke();
            GameObject.Destroy(gameObject);
        }
    }
    
}
