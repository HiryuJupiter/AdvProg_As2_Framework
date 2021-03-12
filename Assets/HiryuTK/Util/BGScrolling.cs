﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{
    public float WarpEdge_leftX = -18.219f;
    public float RespawnOffsetX = 36.438f;

    public float fastSpeed = 5f;
    public float slowSpeed = 2f;

    float acceleration = 99f;
    float moveSpeed;
    float targetSpeed;

    private void Start()
    {
        SetTargetSpeed(slowSpeed);
    }

    void Update()
    {
        //Move speed transition
        moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, acceleration * Time.deltaTime);

        //Move
        transform.Translate(new Vector3(-moveSpeed * FlyweightGlobalSpeed.Speed *Time.deltaTime, 0f, 0f));

        //Warp: left to right
        if (transform.position.x < WarpEdge_leftX)
        {
            Vector3 p = transform.position;
            p.x = p.x + RespawnOffsetX;
            transform.position = p;
        }
    }

    #region Setting scrolling speed
    void FasterScroll()
    {
        SetTargetSpeed(fastSpeed);
    }

    void SlowScroll()
    {
        SetTargetSpeed(slowSpeed);
    }

    void SetTargetSpeed(float s)
    {
        targetSpeed = s;
    }
    #endregion
}