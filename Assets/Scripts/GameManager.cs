using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject instruction;
    //Buttons management
    public void LevelLoad(int index)
    {
        if(SceneManager.GetActiveScene().buildIndex == 0 && index == 1)
        {
            instruction.SetActive(true);
            StartCoroutine(ShowInstruction(index));
            return;
        }
        SceneManager.LoadScene(index);
    }
    IEnumerator ShowInstruction(int index)
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(index);
    }
    public void NextLevelLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
