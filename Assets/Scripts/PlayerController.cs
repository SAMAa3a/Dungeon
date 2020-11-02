﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    Vector2 movement;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        }

        SwitchAnim();
    }

    private void FixedUpdate()
    {
        //rigidbody.position can cover the transform.position
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void SwitchAnim()
    {
        anim.SetFloat("speed", movement.sqrMagnitude);
    }
}
