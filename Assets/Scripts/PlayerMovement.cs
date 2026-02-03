
using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float braking = 0.97f;
    [SerializeField] private List<GameObject> livesList;
    public int livesNumber { get; private set; } = 3;

    private float yBorder = 4f;
    private float xLBorder = -8f;
    private float xRBorder = 2f;

    private float turnRight = 270f;

    private float verticalInput;
    private float horizontalInput;

    private Vector2 direction;
    private bool canTakeDamage;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canTakeDamage = true;
    }

    void Update()
    {
        GameOver();
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
        //move and rotate boat
        if (verticalInput != 0 || horizontalInput != 0)
        {
            direction = new Vector2(horizontalInput, verticalInput).normalized;
            rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + turnRight;
            rb.AddForce(direction * speed, ForceMode2D.Force);
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

    //loose live
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTakeDamage) return;
        if (other.CompareTag("CanonBall") && canTakeDamage)
        {
            canTakeDamage = false;
            other.gameObject.SetActive(false);
            livesNumber--;
            livesList[livesNumber].SetActive(false);
            //Debug.Log("HIT. Lives remain: " + livesNumber);
            Invoke(nameof(ResetDamage), 0.2f);
        }
    }
    private void ResetDamage()
    {
        canTakeDamage = true;
    }

    //game over
    private void GameOver()
    {
        if(livesNumber <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
