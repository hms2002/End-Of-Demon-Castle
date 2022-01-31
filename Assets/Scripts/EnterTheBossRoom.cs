using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnterTheBossRoom : MonoBehaviour
{
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
    Player player;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;
            
        player = other.GetComponent<Player>();

        player.playerConfine();
        player.lookUp();
        TextBox.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        animator.SetTrigger("close");

        //player.playerFree();

        TextBox.SetActive(false);
    }

    public void OpenDoor()
    {
        FadeManager.GetInstance().FadeIn(GameManager.GetInstance().fadeImage_loading);
        player.playerConfine("Skill");
        player.playerConfine();

        animator.SetTrigger("open");
        SoundManager.GetInstance().Play("Sound/LevelSound/DoorCrumbling_Shorter", 0.35f);
        SoundManager.GetInstance().Play("Sound/LevelSound/DoorOpen", 0.4f, Define.Sound.Effect, 0.55f);
        TextBox.SetActive(false);
        Invoke("GoToPos", 1.5f);
    }

    public void GoToPos()
    {
        BossHpUI.SetActive(true);
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
        //player.playerFree();    
    }

    public void FightStart()
    {
        Boss.GetInstance().PatternManager()

        player.playerFree("Skill");
        player.playerFree();

        SoundManager.GetInstance().Play("Sound/BGM/BGM_ingameBoss", 0.5f, Define.Sound.Bgm, 0.5f);
    }
}
