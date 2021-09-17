using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LeftScoreText;
    [SerializeField] private TextMeshProUGUI RightScoreText;
    [SerializeField] private FadeableUI menuUI;
    [SerializeField] private FadeableUI gameoverUI;
    [SerializeField] private TextMeshProUGUI winnerText;

    public void Start()
    {
        menuUI.FadeIn(true);
        gameoverUI.FadeOut(true);
    }

    public void UpdateScoreText(int leftscore, int rightscore)
    {
        LeftScoreText.text = leftscore.ToString();
        RightScoreText.text = rightscore.ToString();

    }

    public void OnGameStart()
    {
        menuUI.FadeOut(false);
        gameoverUI.FadeOut(false);
    }

    public void ShowMenu()
    {
        gameoverUI.FadeOut(false);
        menuUI.FadeIn(false);
    }

    public void ShowGameOver(Paddle.Side side)
    {
        gameoverUI.FadeIn(false);

        if (side == Paddle.Side.Left)
        {
            winnerText.text = "Player One";

        }
        else if (side==Paddle.Side.Right)
        {
            winnerText.text = "Player Two";
        }

    }
}
