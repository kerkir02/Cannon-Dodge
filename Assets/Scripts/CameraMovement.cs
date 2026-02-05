using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hearts;
    [SerializeField] private GameObject wavesEffect;

    private Vector3 heartsPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heartsPosition = new Vector3(16f,8f,0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraFollow();
        HeartsFollow();
        WavesFollow();
    }
    //camera follow player
    private void CameraFollow()
    {
        transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z);
    }
    //hearts follow camera
    private void HeartsFollow()
    {
        hearts.transform.position = heartsPosition + new Vector3(
            transform.position.x,
            transform.position.y,
            hearts.transform.position.z);
    }
    //waves follow camera
    private void WavesFollow()
    {
        wavesEffect.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            wavesEffect.transform.position.z);
    }
}
