using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameProxy GameProxy;
    public MainIndicators MainIndicators;
    public EnemyIndicators EnemyIndicators;
    public Health Health;
    private void OnEnable()
    {
        Health.HitpointMax = EnemyIndicators.Health;
    }
}
