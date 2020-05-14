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
        int crit = UnityEngine.Random.Range(0, 100);
        Debug.Log("crit = "+crit);
        if (crit > 90)
        {
            Debug.Log("Промах. Ты лох!");
        }
        else if (crit > MainIndicators.StartCritDamage)
        {
            Hitpoints = Mathf.Max(0f, Hitpoints - value);
            //проверку надо
            GetComponent<Animator>().SetTrigger("Hit");
        }
        else
        {
            Hitpoints = Mathf.Max(0f, Hitpoints - 2 * value);
            //проверку надо
            GetComponent<Animator>().SetTrigger("Hit");
        }
        GameProxy.WoundEvent(Hitpoints);
        if (Hitpoints <= 0f)
        {
            DieEvent?.Invoke();
            GameProxy.DieMonster();
            GameObject.Destroy(gameObject);
        }
    }
    
}
