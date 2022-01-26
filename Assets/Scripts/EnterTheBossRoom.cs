﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheBossRoom : MonoBehaviour
{
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

        player.playerFree();

        TextBox.SetActive(false);
    }

    public void OpenDoor()
    {
        animator.SetTrigger("open");
        SoundManager.GetInstance().Play("Sound/LevelSound/DoorCrumbling_Shorter", 0.35f);
        SoundManager.GetInstance().Play("Sound/LevelSound/DoorOpen", 0.4f, Define.Sound.Effect, 0.55f);
        TextBox.SetActive(false);
        Invoke("GoToPos", 1.5f);
    }

    public void GoToPos()
    {
        BossHpUI.SetActive(true);
        player.playerFree();
        player.gameObject.transform.position = destination.position;
    }

    public void FullOpen()
    {
        animator.SetBool("isOpen", true);
        Invoke("Close", 1);
    }

    public void Close()
    {
        animator.SetBool("isOpen", false);
        player.playerFree();    
    }
}
