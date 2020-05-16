using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public GameProxy GameProxy;
    public void OnTapHereClick()
    {
        GameProxy.NewGame();

        gameObject.SetActive(false);
    }
}
