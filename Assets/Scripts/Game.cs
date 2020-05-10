using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameProxy GameProxy;

    private void OnEnable()
    {
        GameProxy.NewGameEvent += OnNewGame;
    }

    private void OnNewGame()
    {
        GameProxy.ClearState();
    }
}
