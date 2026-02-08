using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Buttons management
    public void Level1Load()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2Load()
    {
        SceneManager.LoadScene(2);
    }
    public void Level3Load()
    {
        SceneManager.LoadScene(3);
    }
    public void MenuLoad()
    {
        SceneManager.LoadScene(0);
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
