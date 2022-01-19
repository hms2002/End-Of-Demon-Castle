using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Vampire : Skill_ID
{
    Player player;
    Boss boss;
    public GameObject healSkill;
    public GameObject absorbBlood;
    GameObject tempHeal;
    Animator vampireAnimator;

    bool isSkillOn;
    float durationTime;
    public float maxSkillTime = 10.0f;
    public int amountOfRecovery = 10;

    private void Awake()
    {
        player = GetComponent<Player>();
        boss = FindObjectOfType<Boss>();
        healSkill = Resources.Load<GameObject>("Prefabs/Heal");
        absorbBlood = Resources.Load<GameObject>("Prefabs/AbsorbBlood");
        vampireAnimator = absorbBlood.GetComponent<Animator>();

        isSkillOn = false;
    }

    public override void SkillOn()
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
                tempHeal = Instantiate(healSkill);
                tempHeal.transform.position = player.transform.position;
                player.player_hp += amountOfRecovery;
                Destroy(tempHeal, 0.5f);
                boss.wasHit = false;
            }
            
            yield return null;
        }

        durationTime = 0.0f;
        Destroy(aura);
        isSkillOn = false;
    }
}
