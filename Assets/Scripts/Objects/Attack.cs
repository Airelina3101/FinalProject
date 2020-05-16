using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public event Action<float> AttactEvent;
    public float Damage;

    private IEnumerator coroutine;
    private void OnEnable()
    {
        coroutine = WantAttack();
        StartCoroutine(coroutine);
    }
    IEnumerator WantAttack()
    {
        while(true)
        {
            float time = UnityEngine.Random.Range(0F, 2F);
            yield return new WaitForSeconds(time);
            AttactEvent?.Invoke(Damage);
            yield return null;
        }
    }
    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
