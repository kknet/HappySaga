using UnityEngine;
using System.Collections;
using System;

public class BirdController : MonoBehaviour
{

    public event EventHandler GameOver;
    public event EventHandler ScoreAdd;

    //当离开Empty Trigger的时候，分发ScoreAdd事件
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("empty"))
        {
            if (ScoreAdd != null)
                ScoreAdd(this, EventArgs.Empty);
        }
    }

    //当开始碰撞的时候，分发GameOver事件
    void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //rigidbody2D.velocity = new Vector2(0, 0);
        if (GameOver != null)
            GameOver(this, EventArgs.Empty);
        this.enabled = false;
    }

}
