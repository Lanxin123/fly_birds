using UnityEngine;
using System;
using System.Collections;

public class Bird : MonoBehaviour
{
    //得分事件
    public event Action OnHit;

    //死亡事件
    public event Action OnDead;

    //跳跃速度
    public float JumpSpeed = 8f;

    //默认位置
    private Vector3 DefaultPosition;

    //使用重力
    public bool UseGravity
    {
        get { return GetComponent<Rigidbody2D>().gravityScale == 1; }
        set { GetComponent<Rigidbody2D>().gravityScale = value ? 1 : 0; }
    }

    public bool IsVisible
    {
        get { return gameObject.activeSelf; }
        set { gameObject.SetActive(value); }
    }

    void Awake()
    {
        //记录初始位置
        DefaultPosition = this.transform.position;

        //
        OnDead += Bird_OnDead;
    }

    void Update()
    {
        if (Game.Instance.GameState == GameState.Play && transform.position.y < -5f)
        {
            if (OnDead != null)
                OnDead();
        }
    }

    public void Jump()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.up * JumpSpeed;
    }

    public void Die()
    {
        //停止动画
        GetComponent<Animator>().enabled = false;
    }

    public void Restore()
    {
        //速度归零
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.zero;

        //还原
        this.transform.position = DefaultPosition;
        //禁用重力
        UseGravity = false;
        //播动画
        GetComponent<Animator>().enabled = true;
    }

    void Bird_OnDead()
    {
        Die();
    }


    //用于地面，管道的碰撞检测
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Game.Instance.GameState != GameState.Play)
            return;

        string name = collision.gameObject.name;

        if (name == "Land" || name == "PipeUp" || name == "PipeDown")
        {
            if(OnDead!=null)
                OnDead();
        }
    }


    //用于Gap的碰撞检测，用于得分
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Game.Instance.GameState != GameState.Play)
            return;

        GameObject gap = collision.gameObject;
        if (gap.name == "Gap")
        {
            if (OnHit != null)
                OnHit();
        }
    }
}