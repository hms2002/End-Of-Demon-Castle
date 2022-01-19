using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Vampire : MonoBehaviour
{
    Player player;
    Boss boss;
    PlayerSkill skillManager;
    PlayerSkill_Heal healSkill;
    public GameObject absorbBlood;
    Animator vampireAnimator;

    bool isSkillOn;
    float durationTime;
    public float maxSkillTime = 10.0f;
    public int amountOfRecovery = 10;

    private void Awake()
    {
        player = GetComponent<Player>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        skillManager = GetComponent<PlayerSkill>();
        healSkill = GetComponent<PlayerSkill_Heal>();
        vampireAnimator = absorbBlood.GetComponent<Animator>();

        skillManager.e += Vampire;

        isSkillOn = false;
    }

    void Vampire()
    {
        if (isSkillOn)
        {
            return;
        }
        isSkillOn = true;

        StartCoroutine("Absorb");
    }

    IEnumerator Absorb()
    {
        GameObject aura = Instantiate(absorbBlood);

        while (isSkillOn)
        {
            aura.transform.position = player.transform.position;
            durationTime += Time.deltaTime;

            if (durationTime >= maxSkillTime)
            {
                break;
            }

            if (boss.wasHit == true)
            {
                healSkill.Heal(amountOfRecovery);
                boss.wasHit = false;
            }
            
            yield return null;
        }

        durationTime = 0.0f;
        Destroy(aura);
        isSkillOn = false;
    }
}
