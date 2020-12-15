using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject pipeline;
    public float MaxRange =5.99f;
    public float MinRange =-0.52f;
    Coroutine coroutine;//协程：创建协程变量来使用
    //创建列表，控制所有的管道数量和行为,优化之前无限生成管道且所有管道不受控制的方法
    List<Pipeline> pipelines = new List<Pipeline>();

    void Start()
    {
       
        
    }
    public void Init()
    {
        for (int i = 0; i < pipelines.Count; i++)
        {
            Destroy(pipelines[i].gameObject);
        }
        pipelines.Clear();//清空列表，防止堆积
    }
    void Update()
    {
        
    }
    private IEnumerator GeneratePipelines()   //协程：定义一个协程,来控制生成与关闭
    {
        for (int i=0;i<3;i++)//限制生成数量
        {
            if (pipelines.Count < 3)
                GeneratePipeline();
            else
            {
                pipelines[i].enabled = true;
                pipelines[i].Init();
            }
                
            yield return new WaitForSeconds(3f);//使用yield return
        }
    }
    public void StartRun()//开始生成的方法--启动协程
    {
          coroutine = StartCoroutine(GeneratePipelines());//协程：用协程变量的赋值来启动协程，启动协程的方法
    }
    public void Stop()//停止生成
    {
     
        StopCoroutine(coroutine);
        for(int i = 0; i < pipelines.Count; i++)
        {
            pipelines[i].enabled = false;//让管道的功能停止工作
         
        }
    }

    public void GeneratePipeline()//生成管道的方法，GameObject类的Instantiate方法，一般用于生成预置体。
    {//添加参数：随机Y轴值，默认不旋转，注意，transform的参数传入要置于最后,使其出现在manager下方，成为副节点,
     //GameObject.Instantiate(pipeline, this.transform);
        if (pipelines.Count < 3)
        {
            GameObject obj = Instantiate(pipeline, new Vector3(10.5f, Random.Range(MinRange, MaxRange)), Quaternion.identity, this.transform);
            Pipeline p = obj.GetComponent<Pipeline>();
            pipelines.Add(p);
        }   
    }
}
