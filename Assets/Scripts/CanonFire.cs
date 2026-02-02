using UnityEngine;

public class CanonFire : MonoBehaviour
{
    [SerializeField] private float reloadTime = 10;
    [SerializeField] private float cannonBallSpeed = 5;
    [SerializeField] private float cannonBallBorder = 10;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private GameObject fuse;
    [SerializeField] private Transform firePoint;

    private Vector2 direction;

    private GameObject player;
    private Rigidbody2D cannonRb;
    private Rigidbody2D cannonBallRb;

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cannonRb = GetComponent<Rigidbody2D>();
        cannonBallRb = cannonBall.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        timer = reloadTime;
        fuse.SetActive(false);
        cannonBall.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        DeactivateCannonBall();
    }

    
    void FixedUpdate()
    {
        //aiming -> firing -> reloading
        if (timer <= reloadTime * 0.2)
        {
            if (timer <= 0)
            {
                Firecannon();
            }
            else
            {
                fuse.SetActive(true);
            }
        }
        else if(timer <= reloadTime * 0.8)
        {
            Aimcannon();
        }
    }
    //rotate cannon into player
    private void Aimcannon()
    {
        direction = (player.transform.position - transform.position).normalized;
        cannonRb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
    }
    //
    private void Firecannon()
    {
        cannonBall.transform.position = firePoint.transform.position;
        cannonBall.SetActive(true);
        cannonBallRb.AddForce(direction * cannonBallSpeed, ForceMode2D.Impulse);
        timer = reloadTime;
        fuse.SetActive(false);
    }
    //deactivate cannon ball outside view
    private void DeactivateCannonBall()
    {
        if (Mathf.Abs(cannonBall.transform.position.x) > cannonBallBorder ||
            Mathf.Abs(cannonBall.transform.position.y) > cannonBallBorder)
        {
            cannonBall.SetActive(false);
            cannonBallRb.linearVelocity = Vector2.zero;
            cannonBallRb.angularVelocity = 0;
        }
    }
}
