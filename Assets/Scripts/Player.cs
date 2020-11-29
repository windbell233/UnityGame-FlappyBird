using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public float ForceY;
    public Animator animatorBird;
    public bool death=false;//控制死亡
    public bool MouseWakeUp=false;//控制鼠标点击激活
    //问题：开始界面，点击屏幕任意处就会激活刚体--》用变量来控制点击方法的激活，可能是刚体用法问题或者有禁止鼠标点击的方法
    void Start()
    {
        this.Idle();
    }
    void Update()
    {
        if (death)//每一帧都判断Bird是否为死亡状态，死亡就返回空值结束Update
        {
            return;
        }
        if (MouseWakeUp)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rigidbodyBird.velocity = Vector2.zero;
                rigidbodyBird.AddForce(new Vector2(0, ForceY), ForceMode2D.Force);
            }
        }
    }
    public void Idle()//触发浮动动画
    {
        this.rigidbodyBird.Sleep();//rigidbody失效
        this.animatorBird.SetTrigger("Idle");
    }
    public void Fly()//触发飞行动画
    {
        this.rigidbodyBird.WakeUp();//rigidbody激活
        this.animatorBird.SetTrigger("Fly");
    }
    public void OnTriggerEnter2D(Collider2D collision)//碰撞管道检测，触发器
    {
        Die();
        Debug.Log("碰撞死亡");
    }
    private void OnCollisionEnter2D(Collision2D collision)//碰撞地板检测，碰撞器
    {
        Die();
        Debug.Log("触地死亡");
    }
    public void Die()//死亡方法，控制death的值
    {
        death = true;
    }

}
