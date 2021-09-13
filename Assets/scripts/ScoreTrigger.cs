using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private Paddle.Side SideThatScored;

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        Ball ball = Collider.GetComponent<Ball>();

        

        if (ball)
        {
            GameController.instance.Score(SideThatScored);//receives info from game controller method named "Score"

        }

    }
    
  
}
