using UnityEngine;
using TMPro;

public class BottleWorking : MonoBehaviour
{
    [SerializeField] private TMP_Text clue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clue.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            clue.gameObject.SetActive(true);
            Debug.Log("You found the clue.");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            clue.gameObject.SetActive(false);
            Debug.Log("You left the clue.");
        }
    }
}
