using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float moveX, moveY;//moveH,moveV

    [Header("PlayerAttribute")]
    public float moveSpeed;
    public float slashSpeed = 1.0f;
    public float slashScale = 1.6f;
    public float criticRate = 0.05f;
    public float cirticBonus = 1.5f;
    public float strength = 0;

    [Header("AboutHealth")]
    public HealthBar healthBar;
    public LayerMask touchEnemy;
    private float hp = 0;
    private float maxHp = 200;
    private bool isAttacked = false;


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
        anim = GetComponent<Animator>();

        //health init
        hp = maxHp;
        healthBar.ChangeHealth(hp, maxHp);
    }

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        moveY = Input.GetAxis("Vertical") * moveSpeed;
        Flip();

        //switch anim
        anim.SetFloat("speed", new Vector2(moveX, moveY).sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX, moveY);

        //Use ovelapBox to check enemies
        CheckEnemies();
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

    private void CheckEnemies()
    {
        if (Physics2D.OverlapBox(transform.position + new Vector3(0.03374965f, -0.07559937f, 0),
            new Vector2(0.4573055f, 0.7138029f), 0, touchEnemy))
        {
            if (!isAttacked)
            {
                isAttacked = true;
                StartCoroutine(Timer());
                hp -= 20f;
                healthBar.ChangeHealth(hp, maxHp);

                //rb.velocity = new Vector2(rb.velocity.x + 3f, rb.velocity.y + 3f);

                if(hp <= 0)
                {
                    Destroy(gameObject);
                    FindObjectOfType<SceneFader>().FadeTo();
                }

                healthBar.DoHealthEffect();
            }
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.3f);
        isAttacked = false;
    }
}
