using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public float speed;
    public float MaxRange = 5.99f;
    public float MinRange = -0.52f;
    private float t = 0;
    void Start()
    {
        //  Destroy(gameObject, 5f);//5s后自毁
        this.Init();
        //InvokeRepeating("Init", 8.5f, 8.6f);//8.5s后调用一次Init，之后每8.5s都会再一次
        //注意：enabled取false似乎无法让 InvokeRepeating停止。
    }

    public void Init()
    {
        this.transform.localPosition = new Vector3(0, Random.Range(MinRange, MaxRange), 0);
    }
    void Update()
    {
        //逐帧位置移动
         transform.position += new Vector3(-speed, 0) * Time.deltaTime;
        //瞬间位置变动

        //transform.position += new Vector3(-7, 0) ;
        t += Time.deltaTime;
        if (t > 8.5f)//控制初始化
        {
            t = 0;
            this.Init();
        }
    }
}
