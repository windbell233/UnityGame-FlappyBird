using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject pipeline;
    public float MaxRange =5.99f;
    public float MinRange =-0.52f;
    Coroutine coroutine;//协程：创建协程变量来使用
    void Start()
    {
       
        
    }
    void Update()
    {
        
    }
    private IEnumerator GeneratePipelines()   //协程：定义一个协程,来控制生成与关闭
    {
        while (true)
        {
            GeneratePipeline();
            yield return new WaitForSeconds(3f);//使用yield return
        }
    }
    public void StartRun()//开始生成的方法
    {
        coroutine = StartCoroutine(GeneratePipelines());//协程：用协程变量的赋值来启动协程，启动协程的方法
    }
    public void Stop()//停止生成
    {
        
    }

    public void GeneratePipeline()//生成管道的方法
    {
        //使其出现在manager下方，成为副节点
        //  GameObject.Instantiate(pipeline, this.transform);
        //添加参数：随机Y轴值，默认不旋转，注意，transform的参数传入要置于最后
        GameObject.Instantiate(pipeline, new Vector3(10.5f, Random.Range(MinRange, MaxRange)), Quaternion.identity, this.transform);
    }
}
