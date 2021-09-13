using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isAI;
    private Ball ball;
    private BoxCollider2D col;

    
    private Vector2 ForwardDirection;
    private bool firstIncoming;
    private bool overridePosition;
    public enum Side {Left,Right }

    [SerializeField] private Side side;
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float resetTime;
    private float randomYOffset;
    // Update is called once per frame

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        col = GetComponent<BoxCollider2D>();
        if(side == Side.Left)
        {
            ForwardDirection = Vector2.right;

        }

        else if (side == Side.Right)
        {
            ForwardDirection = Vector2.left;

        }

    }

    private void Update()
    {
        if (!overridePosition)
        {
            movePaddle();
        }
        
    }

  

    private void ClampPosition(ref float yPosition)
    {
        float minY = Camera.main.ScreenToWorldPoint(new Vector3(0,0)).y;
        float maxY= Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;

        yPosition = Mathf.Clamp(yPosition,minY,maxY);


    }


    private float GetNewYPosition()//movement, get inputs
    {
        float result = transform.position.y;

        if (isAI)
        {
            if (BallIncoming())
            {
                if (firstIncoming)
                {
                    
                    firstIncoming = false;
                    randomYOffset = GetRandomOffset();
                }

             
                result = Mathf.MoveTowards(transform.position.y, ball.transform.position.y + randomYOffset, movespeed* Time.deltaTime);

            }

            else
            {
                firstIncoming = true;
            }

        }
        else
        {
            float movement = Input.GetAxisRaw("Vertical " + side.ToString()) * movespeed * Time.deltaTime;
            result = transform.position.y + movement;

        }

        return result;


    }

    
    private bool BallIncoming()
    {
        float dotP = Vector2.Dot(ball.velocity, ForwardDirection);
        return dotP < 0f;//check which direction the ball is coming


    }
    


    private float GetRandomOffset()
    {
        float maxOffset =col.bounds.extents.y;
        return Random.Range(-maxOffset, maxOffset);
    }



    private void movePaddle()
    {
        float targetYPosition = GetNewYPosition();//transform.position.y + movement;

        ClampPosition(ref targetYPosition);
        transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);

    }


    public void Reset()
    {
        StartCoroutine(ResetRoutine());

    }



 



    private IEnumerator ResetRoutine()
    {
        overridePosition = true;
        float startPosition = transform.position.y;

        for(float timer = 0; timer < resetTime; timer+= Time.deltaTime)
        {
            float targetPosition = Mathf.Lerp(startPosition, 0f , timer /resetTime );
            transform.position = new Vector3(transform.position.x, targetPosition, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        overridePosition = false;

    }//



}
