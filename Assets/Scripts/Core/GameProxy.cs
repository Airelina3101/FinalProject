using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Proxy")]
public class GameProxy : ScriptableObject
{
	public event Action NewGameEvent;
	public event Action EndGameEvent;
	public event Action<Enemy> SpawnMonsterEvent;
	public event Action<float> EditHPMonsterEvent;
	public event Action<float> EditHPPlayerEvent;

	public void NewGame()
	{
		NewGameEvent?.Invoke();
	}
	public void EndGame()
	{
		EndGameEvent?.Invoke();
	}
	public void SpawnMonster(Enemy enemy)
	{
		SpawnMonsterEvent?.Invoke(enemy);
	}
	public void EditHPMonster(float damage)
	{
		EditHPMonsterEvent?.Invoke(damage);
	}
	public void EditHpPlayer(float damage)
	{
		EditHPPlayerEvent?.Invoke(damage);
	}
}
