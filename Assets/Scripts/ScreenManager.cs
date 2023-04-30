using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public GameObject openingScreen;
    public GameObject mainMenuScreen;
    public GameObject settingsScreen;
    public GameObject infoScreen;
    public SoundManager soundManager;

    public void GoToMainMenu()
    {
        StartCoroutine(FadeOutToMainScreen(openingScreen));
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
    }

    public void GoToInfo()
    {
        mainMenuScreen.SetActive(false);
        infoScreen.SetActive(true);
    }
    public void GoToMainMenuTwo()
    {
        openingScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
        settingsScreen.SetActive(false);
        infoScreen.SetActive(false);
    }

    IEnumerator FadeOutToMainScreen(GameObject screen)
    {
        float fadeTime = 0.7f;
        CanvasGroup canvasGroup = screen.GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }

        screen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void GoToFight() {
        soundManager.SwitchBool();
        SceneManager.LoadScene("Rui");
    }

}
