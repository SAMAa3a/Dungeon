using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Transform[] gameObjects = new Transform[4];
    public GameObject enemyPrefab;

    void Start()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            //int random = (int)Random.Range(0, 2);
            if(Random.Range(0, 2) < 1)
            {
                Instantiate(enemyPrefab, gameObjects[i].transform.position, Quaternion.identity);
            }
        }
    }
}
