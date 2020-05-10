using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Proxy")]
public class GameProxy : ScriptableObject
{
	public event Action NewGameEvent;
	public event Action EndGameEvent;
	public event Action<int> AddScoreEvent;
	public event Action<Character> PlayerChangeEvent;
	public int Scores { get; private set; }

	public void ClearState()
	{
		Scores = 0;
	}
	public void AddGold(int value)
	{
		Scores += value;
		Debug.Log(Scores);

		AddScoreEvent?.Invoke(value);
	}
	public void NewGame()
	{
		NewGameEvent?.Invoke();
	}
}
