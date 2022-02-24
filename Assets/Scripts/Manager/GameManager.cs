using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
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
        SceneManager.sceneLoaded += SceneSetting;
        
        if(gameManager != null)
        {
            if (gameManager != this)
            {
                Destroy(gameObject);
            }
        }
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
        Destroy(gameObject);
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
    public void Ending()
    {
        FadeManager.GetInstance().FadeIn(fadeImage_loading);
    }

    public IEnumerator IEnding()
    {
        Ending();
        yield return new WaitForSeconds(2);
        TextManager.GetInstance().EndingOn(0);
    }

    public void toMain()
    {
        StartCoroutine("ItoMain");
    }
    private void OnDestroy()
    {
    }
    public IEnumerator ItoMain()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("왜 그래");
                SceneManager.LoadScene(0);
                Destroy(gameObject);
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
