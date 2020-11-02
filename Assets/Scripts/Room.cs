using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;

    public bool roomLeft, roomRight, roomUp, roomDown;

    public int stepToStart;

    public Text text;

    public int doorNumber = 0;

    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    public void UpdateRoom(float xOffset,float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));

        text.text = stepToStart.ToString();

        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);

            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(18f, 10f), 0);
            for (int i = 0; i < hits.Length; i++)
            {
                if(hits[i].CompareTag("Enemy"))
                {
                    hits[i].gameObject.GetComponent<Enemy>().isAtRoom = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(18f, 10f), 0);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].CompareTag("Enemy"))
                {
                    hits[i].gameObject.GetComponent<Enemy>().isAtRoom = false;
                }
            }
        }
    }
}
