using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayHUD : MonoBehaviour
{
    public GameProxy GameProxy;
    public Text GoldText;

    private void Awake()
    {
        GameProxy.AddScoreEvent += OnScoreAdded;
    }
    private void OnScoreAdded(int delta)
    {
        GoldText.text = GameProxy.Scores.ToString();
    }
}
