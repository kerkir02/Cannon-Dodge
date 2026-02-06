using UnityEngine;

public class WhirlpoolMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxSize = 1.5f;
    [SerializeField] private float minSize = 0.5f;

    private float mapBorder = 100f;

    private Vector2 direction;
    private bool IsStunned;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        float newScale = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * newScale;
        speed /= newScale;
        IsStunned = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateWhirlpool();
        MoveWhirlpool();
        PlayerBorder();
    }

    //rotate whirlpool
    private void RotateWhirlpool()
    {
        rb.rotation = Mathf.Repeat(rb.rotation + rotationSpeed * Time.fixedDeltaTime, 360f);
    }
    //move whirlpool
    private void MoveWhirlpool()
    {
        if (!IsStunned)
        {
            rb.linearVelocity = direction * speed;
        }
    }
    //change direction after contact
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Land") || collision.collider.CompareTag("Whirlpool"))
        {
            if (IsStunned) return;
            IsStunned = true;
            direction = (transform.position - collision.transform.position).normalized;
            rb.linearVelocity = Vector2.zero;
            Invoke(nameof(MoveAgain), 0.5f);
        }
    }
    private void MoveAgain()
    {
        CancelInvoke(nameof(MoveAgain));
        IsStunned = false;
    }

    private void PlayerBorder()
    {
        //horizontal and vertical border for move
        if (transform.position.magnitude > mapBorder)
        {
            direction = (Vector3.zero - transform.position).normalized;
        }
    }

}
