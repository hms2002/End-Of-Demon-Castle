using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    Player player;
    Animator playerAnimator;
    public GameObject canvas_dead;
    public Image fadeImage_dead;
    public GameObject retryButton;
    public Text retry;

    private void Start()
    {
        player = Player.GetInstance();
        playerAnimator = player.GetComponent<Animator>();
        player.onDead += ShowGameOverScene;
    }
    
    void ShowGameOverScene()
    {
        canvas_dead.SetActive(true);
        player.playerConfine();
        playerAnimator.SetBool("isDie", true);
        playerAnimator.SetTrigger("Die");
        StartCoroutine(FadeIn_GameOver());
    }

    IEnumerator FadeIn_GameOver()
    {
        yield return new WaitForSeconds(1.5f);
        FadeManager.GetInstance().FadeIn(fadeImage_dead);
        yield return new WaitForSeconds(1.5f);
        retryButton.SetActive(true);
    }

    public void ReZero(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void MousePointEnter()
    {
        Color textColor;

        textColor = retry.color;
        textColor.g -= 1;
        textColor.b -= 1;
        retry.color = textColor;
    }
    public void MousePointExit()
    {
        Color textColor;

        textColor = retry.color;
        textColor.g += 1;
        textColor.b += 1;
        retry.color = textColor;
    }
}
