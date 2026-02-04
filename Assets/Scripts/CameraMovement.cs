using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hearts;

    private Vector3 heartsPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heartsPosition = new Vector3(16f,8f,0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //camera follow player
        transform.position = new Vector3(
            player.transform.position.x, 
            player.transform.position.y, 
            transform.position.z);

        //hearts follow camera
        hearts.transform.position = heartsPosition + new Vector3(
            transform.position.x,
            transform.position.y,
            hearts.transform.position.z);
    }
}
