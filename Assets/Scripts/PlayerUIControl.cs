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
    public GameObject canvas_mainMenu;
    public Image fadeImage_dead;
    public GameObject startButton;
    public GameObject optionButton;
    public GameObject exitButton;
    public GameObject retryButton;
    public Text start;
    public Text option;
    public Text exit;
    public Text retry;

    private void Start()
    {
        player = GetComponent<Player>();
        playerAnimator = player.GetComponent<Animator>();
        player.onDead += ShowGameOverScene;
        ShowMainMenu();
    }

    private void Update()
    {
        sliderHp.value = player.player_hp;
    }

    //메인메뉴 세팅
    void ShowMainMenu()
    {
        player.playerConfine();
        canvas_mainMenu.SetActive(true);
    }

    //게임오버씬 세팅
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

        Color imageColor = fadeImage_dead.color;

        for (int i = 0; i < 200; i++)
        {
            imageColor.a += 0.01f;
            fadeImage_dead.color = imageColor;
            yield return new WaitForSeconds(0.01f);
        }
        retryButton.SetActive(true);
    }

    //버튼 색상 변환
    public void MousePointEnter(int buttonType)
    {
        Color textColor;

        switch (buttonType)
        {
            case 0://start
                textColor = start.color;
                textColor.r += 1;
                start.color = textColor;

                break;

            case 1://option
                textColor = option.color;
                textColor.r += 1;
                option.color = textColor;

                break;

            case 2://exit
                textColor = exit.color;
                textColor.r += 1;
                exit.color = textColor;

                break;

            case 3://retry
                textColor = retry.color;
                textColor.g -= 1;
                textColor.b -= 1;
                retry.color = textColor;

                break;

            default:
                break;
        }
    }
    public void MousePointExit(int buttonType)
    {
        Color textColor;

        switch (buttonType)
        {
            case 0://start
                textColor = start.color;
                textColor.r -= 1;
                start.color = textColor;

                break;

            case 1://option
                textColor = option.color;
                textColor.r -= 1;
                option.color = textColor;

                break;

            case 2://exit
                textColor = exit.color;
                textColor.r -= 1;
                exit.color = textColor;

                break;

            case 3://retry
                textColor = retry.color;
                textColor.g += 1;
                textColor.b += 1;
                retry.color = textColor;

                break;

            default:
                break;
        }
    }
}
