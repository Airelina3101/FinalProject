using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Proxy")]
public class GameProxy : ScriptableObject
{
	public event Action NewGameEvent;
	public event Action EndGameEvent;
	public event Action NeedSpawnMonsterEvent;
	public event Action<float> EditHealthBarEvent;
	public event Action<Character> PlayerChangeEvent;

	public void NewGame()
	{
		NewGameEvent?.Invoke();
	}
	public void DieMonster()
	{
		NeedSpawnMonsterEvent?.Invoke();
	}
	public void WoundEvent(float Hitpoints)
	{
		EditHealthBarEvent?.Invoke(Hitpoints);
	}
}
