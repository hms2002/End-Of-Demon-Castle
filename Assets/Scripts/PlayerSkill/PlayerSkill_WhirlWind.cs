using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill_WhirlWind : Skill_ID
{
    Player player;
    SpriteRenderer playerSprite;
    AudioSource playerAudioSource;
    GameObject whirlwind;

    bool isSkillOn;
    float coolTime = 10.0f;
    float howLongUsedSkill = 0.0f;
    float skillDuration = 0.0f;
    float maxSkillDuration = 10.0f;

    //AudioSource 

    void Awake()
    {
        player = GetComponent<Player>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerAudioSource = player.GetComponent<AudioSource>();
        whirlwind = Resources.Load<GameObject>("Prefabs/Whirlwind_Ver2");
        isSkillOn = false;
    }
    
    void Update()
    {
        howLongUsedSkill -= Time.deltaTime;

        if (isSkillOn)
        {
            playerAudioSource.Stop();
        }
    }

    public override void SkillOn()
    {
        if (isSkillOn)
        {
            StopCoroutine(ActiveWhirlwind());
            return;
        }

        /*if (howLongUsedSkill > 0)
        {
            return;
        }*/
        //howLongUsedSkill = coolTime; 다시 눌렀을 때 정지시키기 위해 잠시 보류
        isSkillOn = true;

        StartCoroutine(ActiveWhirlwind());
    }

    IEnumerator ActiveWhirlwind()
    {
        GameObject bladeEffect = Instantiate(whirlwind);

        player.speed = 14.0f;
        player.playerConfine("Attack");
        player.playerConfine("Dash");
        player.playerConfine("Skill");

        while (isSkillOn)
        {
            bladeEffect.transform.position = player.transform.position;
            skillDuration += Time.deltaTime;

            if (skillDuration >= maxSkillDuration)
            {
                break;
            }

            yield return null;
        }

        player.speed = 8.0f;
        player.playerFree();

        isSkillOn = false;
        Destroy(bladeEffect);
        skillDuration = 0.0f;
    }
}
