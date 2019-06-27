

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool GameHasEnded = false;

    public GameObject gameWonUI;

    public GameObject gameOverUI;
    



    public void GameWin()
    {
        gameWonUI.SetActive(true);
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        
      //  Invoke("Restart", 3f);
      //  Restart();
    }

   /* void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/



}
