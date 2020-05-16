using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    public GameProxy GameProxy;
    public void Awake()
    {
        GameProxy.EndGameEvent += OnEndGame;
        gameObject.SetActive(false);
    }
    private void OnEndGame()
    {
        gameObject.SetActive(true);
    }
    public void OnClickNewGame()
    {
        gameObject.SetActive(false);
        GameProxy.NewGame();       
    }
}
