using UnityEngine;

public class CanonFire : MonoBehaviour
{
    [SerializeField] private float maxReloadTime = 5f;
    [SerializeField] private float maxRange = 3f;
    [SerializeField] private float cannonBallSpeed = 5f;
    [SerializeField] private float cannonBallRange = 50f;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private GameObject fuse;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private GameObject range;

    private GameObject player;
    private Rigidbody2D cannonRb;
    private Rigidbody2D cannonBallRb;
    private AudioSource cannonAudio;

    private Vector2 direction;
    private float timer;
    private bool IsInRange;
    private bool IsInFireMode;
    private float reloadTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cannonRb = GetComponent<Rigidbody2D>();
        cannonBallRb = cannonBall.GetComponent<Rigidbody2D>();
        cannonAudio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");

        range.transform.localScale *= Random.Range(1f, maxRange); ;
        reloadTime = Random.Range(2f, maxReloadTime);
        timer = reloadTime;
        fuse.SetActive(false);
        cannonBall.SetActive(false);
        IsInRange = false;
        IsInFireMode = false;
    }

    void Update()
    {
        if (IsInRange || IsInFireMode)
        {
            timer -= Time.deltaTime;
        }
        DeactivateCannonBall();
    }

    
    void FixedUpdate()
    {
        if (IsInRange)
        {
            if (timer <= reloadTime * 0.4)
            {
                fuse.SetActive(true);
                IsInFireMode = true;
            }
            else
            {
                Aimcannon();
            }
        }
        if (IsInFireMode && timer <= 0)
        {
            Firecannon();
        }
    }
    //rotate cannon into player
    private void Aimcannon()
    {
        direction = (player.transform.position - transform.position).normalized;
        cannonRb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
    }
    //fire cannon
    private void Firecannon()
    {
        cannonAudio.PlayOneShot(fireSound, 1f);
        Instantiate(fireEffect, firePoint.transform.position, transform.rotation);
        cannonBall.transform.position = firePoint.transform.position;
        cannonBall.SetActive(true);
        cannonBallRb.AddForce(direction * cannonBallSpeed, ForceMode2D.Impulse);
        timer = reloadTime;
        fuse.SetActive(false);
        IsInFireMode = false;
    }
    //deactivate cannon ball outside view
    private void DeactivateCannonBall()
    {
        if (!cannonBall.activeSelf)
        {
            return; 
        }
        Vector2 currentRange = cannonBall.transform.position - transform.position;
        if (currentRange.sqrMagnitude > cannonBallRange * cannonBallRange)
        {
            cannonBall.SetActive(false);
            cannonBallRb.linearVelocity = Vector2.zero;
            cannonBallRb.angularVelocity = 0;
        }
    }
    //is in range?
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsInRange = false;
        }
    }
}
