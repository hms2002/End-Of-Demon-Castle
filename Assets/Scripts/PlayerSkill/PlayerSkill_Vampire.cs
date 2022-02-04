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

    bool isSkillOn;
    float coolTime = 25.0f;
    float howLongUsedSkill = 0.0f;
    float skillDuration = 0.0f;
    float maxSkillDuration = 3.0f;
    int amountOfRecovery = 10;

    private void Awake()
    {
        player = GetComponent<Player>();
        boss = FindObjectOfType<Boss>();
        healSkill = Resources.Load<GameObject>("Prefabs/Heal");
        absorbBlood = Resources.Load<GameObject>("Prefabs/AbsorbBlood");

        isSkillOn = false;
    }

    private void Update()
    {
        howLongUsedSkill -= Time.deltaTime;
    }

    public override void SkillOn()
    {
        if (isSkillOn == true || howLongUsedSkill > 0)
        {
            return;
        }
        isSkillOn = true;
        howLongUsedSkill = coolTime;

        StartCoroutine("Absorb");
    }

    IEnumerator Absorb()
    {
        GameObject aura = Instantiate(absorbBlood);
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/VampireWave", 0.35f);
        StartCoroutine(PlayVampireLoopSound());

        while (isSkillOn)
        {
            aura.transform.position = player.transform.position;
            skillDuration += Time.deltaTime;

            if (skillDuration >= maxSkillDuration)
            {
                break;
            }

            if (boss.wasHit == true)
            {
                SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/VampireHit", 0.4f);
                tempHeal = Instantiate(healSkill);
                tempHeal.transform.position = player.transform.position;
                player.player_hp += amountOfRecovery;
                Destroy(tempHeal, 0.5f);
                boss.wasHit = false;
            }

            yield return null;
        }

        skillDuration = 0.0f;
        Destroy(aura);
        isSkillOn = false;
    }

    IEnumerator PlayVampireLoopSound()
    {
        yield return new WaitForSeconds(0.6f);
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/VampireLoop", 0.3f);
    }
}
