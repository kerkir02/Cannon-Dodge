
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed { get; private set; } = 10;
    public float braking {get; private set;} = 0.97f;

    private float yBorder = 4f;
    private float xLBorder = -8f;
    private float xRBorder = 2f;

    private float turnUp = 0f;
    private float turnDown = 180f;
    private float turnLeft = 90f;
    private float turnRight = 270f;

    private float verticalInput;
    private float horizontalInput;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        PlayerMove();
        PlayerBorder();
        PlayerStop();        
    }

    private void PlayerMove()
    {
        //move and rotate boat vertically
        if (verticalInput != 0)
        {
            rb.AddForce(verticalInput * speed * Vector2.up);
            if (verticalInput > 0)
            {
                rb.rotation = turnUp;
            }
            else rb.rotation = turnDown;
        }
        //move nad rotate boat horrizontally
        if (horizontalInput != 0)
        {
            rb.AddForce(horizontalInput * speed * Vector2.right);
            if (horizontalInput > 0)
            {
                rb.rotation = turnRight;
            }
            else rb.rotation = turnLeft;
        }
    }

    private void PlayerBorder()
    {
        //horizontal border for move
        if (transform.position.x < xLBorder)
        {
            transform.position = new Vector2(xLBorder, transform.position.y);
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else if (transform.position.x > xRBorder)
        {
            transform.position = new Vector2(xRBorder, transform.position.y);
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        //vertical border for move
        if (transform.position.y > yBorder)
        {
            transform.position = new Vector2(transform.position.x, yBorder);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
        else if (transform.position.y < -yBorder)
        {
            transform.position = new Vector2(transform.position.x, -yBorder);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
    }

    private void PlayerStop()
    {
        //slow boat
        if (verticalInput == 0 && horizontalInput == 0)
        {
            rb.linearVelocity *= braking;
        }
    }
}
