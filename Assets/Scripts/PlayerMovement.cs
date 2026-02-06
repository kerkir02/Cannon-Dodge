
using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float whirlpoolStopTime = 2f;
    [SerializeField] private float whirlpoolEscapeTime = 2f;
    [SerializeField] private float braking = 0.97f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private List<GameObject> livesList;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip destroySound;
    public int livesNumber { get; private set; } = 3;

    private float mapBorder = 100f;

    private float turnRight = 270f;

    private float verticalInput;
    private float horizontalInput;

    private Vector2 direction;
    private bool canTakeDamage;
    private bool canMove;
    private bool isInWhirlpool;

    private Rigidbody2D rb;
    private AudioSource playerAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        canTakeDamage = true;
        canMove = true;
        isInWhirlpool = false;
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
        if ((verticalInput != 0 || horizontalInput != 0) && canMove)
        {
            direction = new Vector2(horizontalInput, verticalInput).normalized;
            rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + turnRight;
            rb.AddForce(direction * speed, ForceMode2D.Force);
            if(rb.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    private void PlayerBorder()
    {
        //horizontal border for move
        if (transform.position.x < -mapBorder)
        {
            transform.position = new Vector2(mapBorder, transform.position.y);
            //rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else if (transform.position.x > mapBorder)
        {
            transform.position = new Vector2(-mapBorder, transform.position.y);
            //rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        //vertical border for move
        if (transform.position.y > mapBorder)
        {
            transform.position = new Vector2(transform.position.x, -mapBorder);
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
        else if (transform.position.y < -mapBorder)
        {
            transform.position = new Vector2(transform.position.x, mapBorder);
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
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
        if (other.CompareTag("Finish"))
        {
            Debug.Log("You found tresure.");
            canTakeDamage = false;
            canMove = false;
        }
        if (other.CompareTag("Whirlpool"))
        {
            isInWhirlpool = true;
            InWhirlpool();
        }
        if (!canTakeDamage) return;
        if (other.CompareTag("CanonBall") && canTakeDamage)
        {
            playerAudio.PlayOneShot(hitSound, 0.1f);
            canTakeDamage = false;
            other.gameObject.SetActive(false);
            livesNumber--;
            livesList[livesNumber].SetActive(false);
            //Debug.Log("HIT. Lives remain: " + livesNumber);
            Invoke(nameof(ResetDamage), 1f);
        }
    }

    private void ResetDamage()
    {
        canTakeDamage = true;
    }
    //whirlpool
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Whirlpool"))
        {
            isInWhirlpool = false;
        }
    }
    private void BackToMove()
    {
        CancelInvoke(nameof(InWhirlpool));
        canMove = true;
        if (isInWhirlpool)
        {
            Invoke(nameof(InWhirlpool), whirlpoolEscapeTime);
        }

    }
    private void InWhirlpool()
    {
        CancelInvoke(nameof(BackToMove));
        if (isInWhirlpool)
        {
            canMove = false;
            rb.linearVelocity = Vector2.zero;
            Invoke(nameof(BackToMove), whirlpoolStopTime);
        }
    }

    //game over
    private void GameOver()
    {
        if(livesNumber <= 0)
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position, 1f);
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
