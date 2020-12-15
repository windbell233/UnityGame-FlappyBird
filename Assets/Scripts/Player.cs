using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//引入unity自带的事件使用——unityAction

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public float ForceY;
    public Animator animatorBird;
    public bool death=false;//控制死亡
    public bool MouseWakeUp=false;//控制点击激活
    public delegate void DeathSender();//定义委托
    public event DeathSender OnDeath;//定义角色死亡事件
    private Vector3 InitPos;
    //BUG：开始界面，点击屏幕任意处就会激活刚体--》用变量来控制点击方法的激活，可能是刚体用法问题或者有禁止点击的方法
    public UnityAction<int> OnScore;//unityAction定义一个得分事件
    void Start()
    {
        this.Idle();
        InitPos = this.transform.position;
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
       // this.rigidbodyBird.Sleep();//rigidbody失效
        this.rigidbodyBird.simulated = false;//物理模拟失效
        this.animatorBird.SetTrigger("Idle");
    }
    public void Fly()//触发飞行动画
    {
       // this.rigidbodyBird.WakeUp();//rigidbody激活
        this.rigidbodyBird.simulated = true;//物理模拟激活

        this.animatorBird.SetTrigger("Fly");
    }
    public void OnTriggerEnter2D(Collider2D collision)//碰撞管道检测，触发器
    {
        if (collision.gameObject.name.Equals("ScoreArea"))
        {
            //什么都不做
        }
        else
            Die();
        Debug.Log("碰撞死亡");
    }
    public void OnTriggerExit2D(Collider2D collision)//退出积分区域检测，触发器
    {
        if (collision.gameObject.name.Equals("ScoreArea"))
        {
            if (OnScore != null)
            {
                OnScore(1);
            }
        }
        
        Debug.Log("碰撞死亡");
    }
    private void OnCollisionEnter2D(Collision2D collision)//碰撞地板检测，碰撞器
    {
        Die();
        Debug.Log("触地死亡");
    }
    public void Die()//死亡方法，控制death的值,同时作为角色死亡消息的事件通知发送器
    {
        death = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
    }
    public void Init()
    {
        this.transform.position = InitPos;
        Idle();
        death = false;
    }
}
