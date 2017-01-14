﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{

    public Vector2 velocity = Vector2.zero;
    public Vector3 startPos;

    private Rigidbody2D body2d;

    void Awake()
    {
        startPos = transform.position;
        body2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        body2d.velocity = velocity;
        if (transform.position.x >= (startPos.x + 1800))
        {
            transform.position = startPos;
        }
    }
}
