using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Heal : MonoBehaviour
{
    Player player;
    public GameObject heal;
    Animator animator;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = heal.GetComponent<Animator>();
    }

    //플레이어 힐
    public void Heal(float amount)
    {
        GameObject healEffect = Instantiate(heal);
        healEffect.transform.position = player.transform.position;
        Debug.Log("힐이 " + amount + "만큼 됨.");
        player.player_hp += amount;
        
        Destroy(healEffect, 0.5f);
    }
}
