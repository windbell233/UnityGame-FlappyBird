using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerBird;
   public enum GameStatus//创建枚举，控制UI的三种状态
    {
        Ready,
        InGame,
        GameOver
    }
    private GameStatus gameStatus;

    public GameObject PanelReady;
    public GameObject PanelGame;
    public GameObject PanelGameOver;
    public PipelineManager PipelineManager;
    void Start()
    {
        PanelReady.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        gameStatus = GameStatus.InGame;
        UpdateUI();
        PipelineManager.StartRun();
        playerBird.Fly();//启动飞行动画，同时结束浮动动画
        playerBird.MouseWakeUp = true;//激活鼠标点击方法
    }
    public void UpdateUI()//控制UI的变化，用一个方法来控制非常方便，比到处散布的控制更有可控性
    {
        PanelReady.SetActive(gameStatus == GameStatus.Ready);
        PanelGame.SetActive(gameStatus == GameStatus.InGame);
        PanelGameOver.SetActive(gameStatus == GameStatus.GameOver);
    }
    
}
