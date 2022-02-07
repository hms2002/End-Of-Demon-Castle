using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject canvas_loading;
    public Image fadeImage_loading;
    public Button enterBossroomBtn;


    public static GameManager GetInstance()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        return gameManager;
    }

    private void Start()
    {
        DontDestroyOnLoad(canvas_loading);
        DontDestroyOnLoad(fadeImage_loading);
        SceneManager.sceneLoaded += SceneSetting;
    }

    void SceneSetting(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //enterBossroomBtn = GameObject.Find("Yes").GetComponent<Button>();
            //enterBossroomBtn.onClick.AddListener(EnterBossRoom);
            FadeManager.GetInstance().FadeOut(fadeImage_loading);
        }
    }

    public void StartGame(int SceneNumber)
    {
        StartCoroutine(IStartGame(SceneNumber));        
    }

    IEnumerator IStartGame(int SceneNumber)
    {
        FadeManager.GetInstance().FadeIn(fadeImage_loading);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneNumber);
    }

    public void EnterBossRoom()
    {
        Debug.Log("Hello");
        enterBossroomBtn.transform.parent.gameObject.SetActive(false);
        FadeManager.GetInstance().FadeIn(fadeImage_loading);
    }

    public void SeeOption()
    {
        //canvas_mainMenu.SetActive(false);
        //player.playerFree();
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
