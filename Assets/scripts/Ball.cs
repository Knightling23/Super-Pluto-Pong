using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 velocity { get; private set; }
    private bool overridePosition;


    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float maxBouncingAngle = 45f;
    [SerializeField]private float resetTime;
    [SerializeField] private float serveAngle = 45f;

  
   private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Serve(Paddle.Side.Left);

    }//

   
    private void FixedUpdate()
    {
        if (overridePosition == false)
        {
            rb.velocity = velocity;
        }
        
    }//
   
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Paddle")
        {
            BounceFromPaddle(collision.collider);

        }
        else
        {
            Bounce();

        }
     

    }//





    private void Bounce()
    {
        velocity = new Vector2(velocity.x,-velocity.y);

    }//




    private void BounceFromPaddle(Collider2D collider)
    {

        float colYExtent = collider.bounds.extents.y;
        float yOffset = transform.position.y - collider.transform.position.y;

        float yRatio = yOffset / colYExtent;

        float bounceAngle = maxBouncingAngle * yRatio * Mathf.Deg2Rad;// we have to convert degrees to radiants

        Vector2 bounceDirection = new Vector2(Mathf.Cos(bounceAngle),Mathf.Sin(bounceAngle));// cos and sin takees it in degree not radiant

        bounceDirection.x *= Mathf.Sign(-velocity.x);//prevents bouncing behind glitch

        velocity = bounceDirection * moveSpeed;
    }//




    public void Reset(Paddle.Side side)
    {
        StartCoroutine(ResetRoutine(side));

    }//




    private IEnumerator ResetRoutine(Paddle.Side side)
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;

        overridePosition = true;
        yield return new WaitForSeconds(resetTime);
        overridePosition = false;

        Serve(side);
    }//


    private void Serve(Paddle.Side side)
    {
        Vector2 serveDirection = new Vector2(Mathf.Cos(serveAngle * Mathf.Deg2Rad), Mathf.Sin(serveAngle * Mathf.Deg2Rad));
        //serveDirection.y = -serveDirection.y;
        if (side == Paddle.Side.Left)
        {
            serveDirection.x = -serveDirection.x;
        }


        velocity = serveDirection * moveSpeed;
    }//


}
