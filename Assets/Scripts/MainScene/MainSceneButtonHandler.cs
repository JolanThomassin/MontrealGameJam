using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneButtonHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject loadingScene;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text progressText;
    public void Play()
    {
        loadingScene.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("TestLoading");
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            slider.value = progress;
            progressText.text = "Loading..." + progress * 100f + "%";

            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }


        yield return null;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
