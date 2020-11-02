using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    GameObject mapSprite;

    //earlier than start
    private void OnEnable()
    {
        //GetChild(0) is the first child, the type is transform
        mapSprite = transform.parent.GetChild(0).gameObject;

        mapSprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            mapSprite.SetActive(true);
        }
    }
}
