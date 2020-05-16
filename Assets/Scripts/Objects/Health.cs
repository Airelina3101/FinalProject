using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action DieEvent;
    public event Action<float> HitpointsChangeEvent;
    public float HitpointMax;

    public float Hitpoints { get; private set; }
    private void OnEnable()
    {
        Hitpoints = HitpointMax;
    }
    public void Damage(float value)
    {
        Hitpoints = Mathf.Max(0f, Hitpoints - value);
        HitpointsChangeEvent?.Invoke(Hitpoints);
        if (Hitpoints <= 0f)
        {
            DieEvent?.Invoke();
            Kill();
        }
    } 
    public void Kill()
    {
        GameObject.Destroy(gameObject);
    }
}
