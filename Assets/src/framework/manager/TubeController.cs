﻿using UnityEngine;
using System.Collections;
using System;


public class TubeController : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        GameObject.Find("bird").GetComponent<BirdController>().GameOver += OnGameOver;
    }

    void OnDestroy()
    {
        if (GameObject.Find("bird"))
            GameObject.Find("bird").GetComponent<BirdController>().GameOver -= OnGameOver;
    }

    void OnGameOver(object sender, EventArgs e)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //rigidbody2D.velocity = new Vector2(0, 0);
    }

}