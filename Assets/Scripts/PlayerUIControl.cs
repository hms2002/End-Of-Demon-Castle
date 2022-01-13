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
        playerAnimator.SetBool("OnDead", true);
        canvas_dead.SetActive(true);
        player.playerConfine();
        StartCoroutine(FadeIn_GameOver());
    }

    IEnumerator FadeIn_GameOver()
    {
        yield return new WaitForSeconds(1.5f);

        Color startColor = fadeImage_dead.color;

        for (int i = 0; i < 200; i++)
        {
            startColor.a += 0.01f;
            fadeImage_dead.color = startColor;
            yield return new WaitForSeconds(0.01f);
        }
        retryButton.SetActive(true);
    }
}
