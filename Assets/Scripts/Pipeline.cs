using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Destroy(gameObject, 5f);//5s后自毁
      
    }

   
    void Update()
    {
        //逐帧位置移动
         transform.position += new Vector3(-speed, 0) * Time.deltaTime;
        //瞬间位置变动
        //transform.position += new Vector3(-7, 0) ;
    }
}
