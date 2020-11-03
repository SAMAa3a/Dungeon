using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardGenerator : MonoBehaviour
{
    public Transform rewardPoint;
    public GameObject[] rewardPrefabs;

    private void Start()
    {
        if(Random.Range(0, 10) <= 2)
        {
            int index = Random.Range(0, 6);
            if (index > 5) index = 5;

            Instantiate(rewardPrefabs[index], rewardPoint.position, Quaternion.identity);
        }
    }
}
