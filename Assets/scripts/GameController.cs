using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

   [SerializeField] private UIManager UIManager;
   [SerializeField] private int scoreToWin = 2;
   [SerializeField] private int leftScore;
   [SerializeField]  private int rightScore;
   public static GameController instance { get; private set; }//sends data from one script to another
   private Ball ball;
   [SerializeField] private bool inmenu;

   [SerializeField] private Paddle leftPaddle;
   [SerializeField] private Paddle rightPaddle;

    private Paddle.Side serveside;
    private void Awake()
    {
        instance = this;
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        inmenu = true;//puts game in the menu screen
        leftPaddle.isAI = rightPaddle.isAI = true;// this sets the game to start in the menu when the game starts
        DoMenu();
    }


    private void DoMenu()
    {
        inmenu = true;
        leftPaddle.isAI = rightPaddle.isAI = true;
        leftScore = rightScore = 0;
        UIManager.UpdateScoreText(leftScore, rightScore);
        ResetGame();
    }


    public void Score(Paddle.Side side)
    {
        if(side == Paddle.Side.Left)
        {
            leftScore++;
        }
        else if(side == Paddle.Side.Right)
        {
            rightScore++;
        }
        UIManager.UpdateScoreText(leftScore, rightScore);
        serveside = side;
        if (IsGameOver())
        {

            if (inmenu)
            {
                ResetGame();
                leftScore = rightScore = 0;
            }
            else
            {
                ball.gameObject.SetActive(false);
                UIManager.ShowGameOver(side);
            }
        }
        else
        {
            ResetGame();
        }
    }




    private bool IsGameOver()
    {
        bool result = false;

        if (leftScore>=scoreToWin || rightScore >= scoreToWin)
            result = true;

        return result;
    }



    private void ResetGame()
    {
        ball.gameObject.SetActive(true);
        ball.Reset(serveside);
        leftPaddle.Reset();
        rightPaddle.Reset();


    }

    #region UIMethods
    public void StartOneplayer()
    {
       
        leftPaddle.isAI = false;
        rightPaddle.isAI = true;
        InitialGame();
    }

    public void StartTwoPlayers()
    {
        leftPaddle.isAI = false;
        rightPaddle.isAI = false;
        InitialGame();
       

    }

    private void InitialGame()
    {
        inmenu = false;
        leftScore = rightScore = 0;// this will reset the scores while player is inside the menu
        UIManager.UpdateScoreText(leftScore,rightScore);
        ResetGame();
        UIManager.OnGameStart();

    }

    public void Quitbutton()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }

    #endregion


    public void GoToMenu()
    {
        UIManager.ShowMenu();
        DoMenu();
       
    }

    public void Replay()
    {
        InitialGame();
        UIManager.OnGameStart();
    }

}
