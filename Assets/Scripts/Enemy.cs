using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Transform target;
    [SerializeField] private float maxHp;
    public float hp;

    [Header("Hurt")]
    private SpriteRenderer sp;
    public float hurtLength;//how long the effect last
    private float hurtCounter;//how long the effect left, minus by frame

    [HideInInspector]
    public bool isAttacked;
    public GameObject explosionEffect;

    public bool isAtRoom;

    public HealthBar healthBar;

    private void Start()
    {
        hp = maxHp;
        healthBar.ChangeHealth(hp, maxHp);

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isAtRoom)
        {
            FollowPlayer();
        }

        if(hurtCounter <=0)
        {
            sp.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            hurtCounter -= Time.deltaTime;
        }
    }

    private void FollowPlayer()
    {
        transform.position =  Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void TakenDamage(float _amount)
    {
        isAttacked = true;//Only run once
        StartCoroutine(isAttackCo());
        hp -= _amount;

        healthBar.ChangeHealth(hp, maxHp);
        healthBar.DoHealthEffect();

        HurtShader();
        //isAttacked = false;//Only run once

        if(hp <= 0)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void HurtShader()
    {
        sp.material.SetFloat("_FlashAmount", 1);
        hurtCounter = hurtLength;
    }

    IEnumerator isAttackCo()
    {
        yield return new WaitForSeconds(0.1f);
        isAttacked = false;
    }
}
