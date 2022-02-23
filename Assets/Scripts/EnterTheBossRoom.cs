using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnterTheBossRoom : MonoBehaviour
{
    public GameObject yesButton;
    public GameObject noButton;
    public Text yesText;
    public Text noText;

    public static EnterTheBossRoom enterTheBoss;

    public static EnterTheBossRoom GetInstance()
    {
        if (enterTheBoss == null)
        {
            enterTheBoss = FindObjectOfType<EnterTheBossRoom>();
        }

        return enterTheBoss;

    }

    public GameObject BossHpUI;
    public GameObject TextBox;
    public Transform destination;

    bool isGoPosOn = false;

    Player player;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        player = other.GetComponent<Player>();

        player.playerConfine();
        player.lookUp();
        TextBox.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        animator.SetTrigger("close");

        if (!isGoPosOn)
            //player.playerFree();

        TextBox.SetActive(false);
    }

    public void OpenDoor()
    {
        isGoPosOn = true;

        FadeManager.GetInstance().FadeIn(GameManager.GetInstance().fadeImage_loading);
        player.playerConfine("Skill");
        player.playerConfine();

        animator.SetTrigger("open");
        SoundManager.GetInstance().Play("Sound/LevelSound/DoorCrumbling_Shorter", 0.35f);
        Invoke("DelayPlay", 0.3f);
        TextBox.SetActive(false);
        Invoke("GoToPos", 1.5f);
    }

    void DelayPlay()
    {
        SoundManager.GetInstance().Play("Sound/LevelSound/DoorOpen", 0.4f, Define.Sound.Effect, 0.55f);
    }

    public void GoToPos()
    {
        //BossHpUI.SetActive(true);
        //player.playerFree();
        player.gameObject.transform.position = destination.position;

        TextManager.GetInstance().BossTextOn(0);
    }

    public void FullOpen()
    {
        animator.SetBool("isOpen", true);
        Invoke("Close", 1);
    }

    public void Close()
    {
        animator.SetBool("isOpen", false);
    }

    public void Close_No()
    {
        animator.SetBool("isOpen", false);
        player.playerFree();
    }

    public void FightStart()
    {
        Boss.GetInstance().PatternManager();

        //player.playerFree("Skill");
        //player.playerFree();

        SoundManager.GetInstance().Play("Sound/BGM/BGM_ingameBoss", 0.3f, Define.Sound.Bgm, 0.5f);
    }

    public void MousePointEnter(int buttonType)
    {
        Color textColor;

        switch (buttonType)
        {
            case 0:
                textColor = yesText.color;
                textColor.g -= 1.0f;
                textColor.b -= 1.0f;
                yesText.color = textColor;

                break;

            case 1:
                textColor = noText.color;
                textColor.g -= 1.0f;
                textColor.b -= 1.0f;
                noText.color = textColor;

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
            case 0:
                textColor = yesText.color;
                textColor.g += 1.0f;
                textColor.b += 1.0f;
                yesText.color = textColor;

                break;

            case 1:
                textColor = noText.color;
                textColor.g += 1.0f;
                textColor.b += 1.0f;
                noText.color = textColor;

                break;

            default:
                break;
        }
    }
}
