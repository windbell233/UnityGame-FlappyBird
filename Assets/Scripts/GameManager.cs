using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player playerBird;
    public int score;
    public Text UIScore;
    public Text EndScore;
    public int Score//定义积分属性，使用属性来进行控制，使每次积分被赋值改变时就调用一次文本显示的修改
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UIScore.text = score.ToString();
            EndScore.text = score.ToString();
        }
    }
   public enum GameStatus//创建枚举，控制UI的三种状态
    {
        Ready,
        InGame,
        GameOver
    }
    private GameStatus gameStatus;
    private GameStatus Status//定义属性来简化UI界面多次变化导致UpdateUI在代码中多次写出调用。--封装
    {
        get
        {
            return gameStatus;
        }
        set
        {
            gameStatus = value;
            UpdateUI();
        }
        
    }

    public GameObject PanelReady;
    public GameObject PanelGame;
    public GameObject PanelGameOver;
    public PipelineManager PipelineManager;
   
    void Start()
    {
        PanelReady.SetActive(true);
        playerBird.OnDeath += PlayerBird_OnDeath;//订阅角色死亡事件，输入完+=后可以按TAB补全剩余代码：事件处理方法的定义。
        playerBird.OnScore = OnPlayerScore;//订阅得分事件
    }
    private void OnPlayerScore(int score)
    {
        Score += score;//增加分数
    }

    private void PlayerBird_OnDeath()//角色死亡事件消息接受后的处理方法
    {
        Status = GameStatus.GameOver;
        // UpdateUI();
        this.PipelineManager.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        Status = GameStatus.InGame;
       // UpdateUI();
        PipelineManager.StartRun();
        playerBird.Fly();//启动飞行动画，激活受力刚体，同时结束浮动动画
        playerBird.MouseWakeUp = true;//激活点击，在激活之前，任何点击都不会激活player的刚体
    }
    public void UpdateUI()//控制UI的变化，用一个方法来控制非常方便，比到处散布的控制更有可控性
    {
        PanelReady.SetActive(gameStatus == GameStatus.Ready);
        PanelGame.SetActive(gameStatus == GameStatus.InGame);
        PanelGameOver.SetActive(gameStatus == GameStatus.GameOver);
    }
    public void Restart()
    {
        Status = GameStatus.Ready;
        //UpdateUI();
        PipelineManager.Init();
        playerBird.Init();
        Score = 0;//分数置0
    }
}
