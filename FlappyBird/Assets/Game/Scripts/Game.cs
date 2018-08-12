using UnityEngine;
using System;
using System.Collections;

class Game : MonoBehaviour
{
    #region 单例
    private static Game m_Instance = null;
    public static Game Instace { get { return m_Instance; } }
    void Awake() { m_Instance = this; }
    #endregion

    #region 事件
    public event Action<GameState> OnStateChanged;
    #endregion

    #region 变量
    GameState m_Sate = GameState.Init;

    public GameState GameState
    {
        get { return m_Sate; }
         private set
        {
            m_Sate = value;

            if (OnStateChanged != null)
                OnStateChanged(m_Sate);
        }
    }
    #endregion
    #region 演员
    public Bird Bird = null;
    public Background Background = null;
    public ObstacleLoop ObstacleLoop = null;
    public GameUI GameUI = null;
    public InputController InputController = null;
    #endregion

    void Start()
    {
        //监听
        OnStateChanged += Game_OnStateChanged;
        Bird.OnDead += Bird_OnDead;
        InputController.OnTab += InputController_OnTab;

        //初始进入Init
        GotoInit();
    }

    public void GotoInit()
    {
        this.GameState = GameState.Init;
    }

    public void GotoReady()
    {
        this.GameState = GameState.Ready;
    }

    public void GotoPlay()
    {
        this.GameState = GameState.Play;
    }

    public void GotoOver()
    {
        this.GameState = GameState.Over;
    }

    void Game_OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                InputController.CanTab = false;
                Bird.IsVisible = false;
                Bird.UseGravity = false;
                ObstacleLoop.IsMove = true;
                ObstacleLoop.SetPipesVisible(false);
                Background.RandomShow();
                GameUI.UpdateUI(GameState.Init);
                break;
            case GameState.Ready:
                InputController.CanTab = true;
                Bird.IsVisible = true;
                Bird.UseGravity = false;
                Bird.Restore();
                ObstacleLoop.IsMove = true;
                ObstacleLoop.SetPipesVisible(false);
                Background.RandomShow();
                GameUI.UpdateUI(GameState.Ready);
                break;
            case GameState.Play:
                InputController.CanTab = true;
                Bird.IsVisible = true;
                Bird.UseGravity = true;
                ObstacleLoop.IsMove = true;
                ObstacleLoop.IsShowPipes = true;
                GameUI.UpdateUI(GameState.Play);
                break;
            case GameState.Over:
                InputController.CanTab = false;
                Bird.IsVisible = true;
                Bird.UseGravity = true;
                ObstacleLoop.IsMove = false;
                GameUI.UpdateUI(GameState.Over);
                break;
            default: break;
        }
    }

    void InputController_OnTab()
    {
        //Ready----->Play
        if (this.GameState == GameState.Ready)
            GotoPlay();

        //小鸟跳跃
        Bird.Jump();
    }

    void Bird_OnDead()
    {
        GotoOver();
    }
}