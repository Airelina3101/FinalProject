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
    public AudioSource SoundForAttack;
    private void OnEnable()
    {
        MainIndicators.ClearAll();
        EnemyIndicators.ClearAll();
        GameProxy.NewGameEvent += OnNewGame;
        GameProxy.NeedSpawnMonsterEvent += SpawnMonsters;
        GameProxy.WoundToMonster += TurnSoundAttack;
        GameObject monsterObj = Instantiate(Monsters[0]) as GameObject;
        monsterObj.transform.position = PointOfSpawnMonster.position;
    }

    private void OnNewGame()
    {
        MainIndicators.ClearAll();
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
        yield return null;
    }
    private void TurnSoundAttack()
    {
        //Еще нужна будет проверка на null
        //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        SoundForAttack.PlayOneShot(SoundForAttack.clip);
    }
}
