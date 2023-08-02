using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<GameObject> enemies;

    [SerializeField] private int spawEnemies;

    public void SpawEnemies()
    {
        spawEnemies++;
        if(spawEnemies < enemies.Count) 
            enemies[spawEnemies].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler player = other.GetComponent<PlayerControler>();

        if(player != null)
        {
            enemies[0].SetActive(true);
        }
    }

}
