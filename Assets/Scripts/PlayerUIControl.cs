using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIControl : MonoBehaviour
{
    public Slider sliderHp;
    Player player;
    Animator playerAnimator;
    public GameObject canvas_dead; 
    public Image fadeImage_dead;
    public Text retry;
    public GameObject retryButton;

    private void Start() {
        player = GetComponent<Player>();
        playerAnimator = player.GetComponent<Animator>();
        player.onDead += ShowGameOverScene;
    }

    private void Update() {
        sliderHp.value = player.player_hp;
    }

    void ShowGameOverScene()
    {
        playerAnimator.SetTrigger("Die");
        canvas_dead.SetActive(true);
        player.playerConfine();
        StartCoroutine(FadeIn_GameOver());
    }

    IEnumerator FadeIn_GameOver()
    {
        yield return new WaitForSeconds(1.5f);

        Color imageColor = fadeImage_dead.color;

        for (int i = 0; i < 200; i++)
        {
            imageColor.a += 0.01f;
            fadeImage_dead.color = imageColor;
            yield return new WaitForSeconds(0.01f);
        }
        retryButton.SetActive(true);
    }

    public void MousePointEnter()
    {
        Color textColor = retry.color;
        textColor.g -= 1;
        textColor.b -= 1f;
        retry.color = textColor;
    }

    public void MousePointExit()
    {
        Color textColor = retry.color;
        textColor.g += 1;
        textColor.b += 1f;
        retry.color = textColor;
    }
}
