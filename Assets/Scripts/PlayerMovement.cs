using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX, moveY;//moveH,moveV

    [SerializeField]
    private float moveSpeed;

    //private float hp = 0;
    //private float maxHp = 200;
    //private bool isAttacked = false;
    //public HealthBar healthBar;

    public static PlayerMovement instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //hp = maxHp;
        //healthBar.ChangeHealth(hp, maxHp);
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        moveY = Input.GetAxis("Vertical") * moveSpeed;
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX, moveY);
    }


    private void Flip()
    {
        //if(moveX > 0)
        if(transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //else if(moveX < 0)
        if(transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.gameObject.CompareTag("EnemyTouch"))
    //    {
    //        if(!isAttacked)
    //        {
    //            isAttacked = true;
    //            StartCoroutine(Timer());
    //            hp -= 20f;
    //            healthBar.ChangeHealth(hp, maxHp);
    //            healthBar.DoHealthEffect();
    //        }
    //    }
    //}

    //IEnumerator Timer()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    isAttacked = false;
    //}
}
