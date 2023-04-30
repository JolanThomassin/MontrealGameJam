using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Rui");
    }

    public void ToTheMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
