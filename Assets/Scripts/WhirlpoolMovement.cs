using UnityEngine;

public class WhirlpoolMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxSize = 1.5f;
    [SerializeField] private float minSize = 0.5f;

    private float mapBorder = 100f;

    private Vector2 direction;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        float newScale = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * newScale;
        speed /= newScale;
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
        rb.linearVelocity = direction * speed;
    }
    //change direction after contact
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            direction = (transform.position - other.transform.position).normalized;
        }
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
