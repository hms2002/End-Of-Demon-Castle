using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Vampire : MonoBehaviour
{
    Player player;
    PlayerSkill skillManager;
    Boss boss;
    public GameObject absorbBlood;
    public GameObject heal;
    Animator vampireAnimator;

    bool isSkillOn;
    float durationTime;
    public float maxSkillTime = 3.0f;
    public int amountOfRecovery = 10;

    private void Awake()
    {
        player = GetComponent<Player>();
        skillManager = GetComponent<PlayerSkill>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        vampireAnimator = absorbBlood.GetComponent<Animator>();
        skillManager.e += Vampire;

        isSkillOn = false;
    }

    void Vampire()
    {
        isSkillOn = true;
        StartCoroutine("Absorb");
    }

    IEnumerator Absorb()
    {
        while (isSkillOn)
        {

            durationTime += Time.deltaTime;

            if (durationTime > maxSkillTime)
            {
                isSkillOn = false;
                break;
            }

            if (boss.damaged == true)
            {
                Heal();
            }
            
            yield return null;
        }

    }

    void Heal()
    {
        vampireAnimator.SetTrigger("heal");
        player.player_hp += amountOfRecovery;
        boss.damaged = false;
    }
}
