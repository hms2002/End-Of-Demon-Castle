using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject canvas_mainMenu;
    public GameObject canvas_loading;
    public Image fadeImage_loading;

    public void StartGame()
    {
        StartCoroutine(FadeIn_Loading());
        canvas_loading.SetActive(true);
    }

    IEnumerator FadeIn_Loading()
    {
        Color imageColor = fadeImage_loading.color;

        for (int i = 0; i < 100; i++)
        {
            imageColor.a += 0.01f;
            fadeImage_loading.color = imageColor;
            yield return new WaitForSeconds(0.01f);
        }

        canvas_mainMenu.SetActive(false);
        StartCoroutine(FadeOut_Loading());
    }

    IEnumerator FadeOut_Loading()
    {
        player.playerFree();
        Color imageColor = fadeImage_loading.color;

        for (int i = 200; i > 0; i--)
        {
            imageColor.a -= 0.01f;
            fadeImage_loading.color = imageColor;
            yield return new WaitForSeconds(0.01f);
        }

        canvas_loading.SetActive(false);
    }

    public void SeeOption()
    {
        canvas_mainMenu.SetActive(false);
        player.playerFree();
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ReZero(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
