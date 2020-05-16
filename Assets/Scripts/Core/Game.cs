using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameProxy GameProxy;
    public MainIndicators MainIndicators;
    public EnemyIndicators EnemyIndicators;
    public Transform PointOfSpawnMonster;
    public GameObject[] Monsters;
    public PlayerController PlayerController;
    [Header("Voice")]
    public AudioSource AudioSourceLoop;
    public AudioClip BackgroundMusic;

    private Enemy _enemy;

    private void OnEnable()
    {
        GameProxy.NewGameEvent += OnNewGame;        
        GameProxy.EndGameEvent += OnEndGame;
        if (BackgroundMusic!=null && AudioSourceLoop!=null)
        {
            AudioSourceLoop.PlayOneShot(BackgroundMusic);
        }
    }
    private void OnDisable()
    {
        GameProxy.NewGameEvent -= OnNewGame;
        GameProxy.EndGameEvent -= OnEndGame;
    }
    private void OnNewGame()
    {
        MainIndicators.ClearAll();
        EnemyIndicators.ClearAll();
        SpawnMonsters();
    }
    private void OnEndGame()
    {
        _enemy.Character.Health.Kill();
    }
    private void SpawnMonsters()
    {
        StartCoroutine(SpawnMonstersInternal());
    }
    IEnumerator SpawnMonstersInternal()
    {
        EnemyIndicators.IncreaseLevel();
        yield return new WaitForSeconds(0.5f);
        int index = Random.Range(0, Monsters.Length);
        GameObject monsterObj = Instantiate(Monsters[index]) as GameObject;
        monsterObj.transform.position = PointOfSpawnMonster.position;
        _enemy = monsterObj.GetComponent<Enemy>();
        _enemy.Character.Health.DieEvent += OnMonsterDied;

        GameProxy.SpawnMonster(_enemy);

        yield return null;
    }
    private void OnMonsterDied()
    {
        _enemy.Character.Health.DieEvent -= OnMonsterDied;
        _enemy = null;
        SpawnMonsters();
    }
}
