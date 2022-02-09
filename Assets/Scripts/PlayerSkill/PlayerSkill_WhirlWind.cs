using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill_WhirlWind : Skill_ID
{
    Player player;
    AudioSource playerAudioSource;
    GameObject whirlwind;

    bool isSkillOn;
    float coolTime = 10.0f;
    float curTime;
    bool isSliderInit = false;
    float skillDuration = 0.0f;
    float maxSkillDuration = 10.0f;

    void Awake()
    {
        player = GetComponent<Player>();
        playerAudioSource = player.GetComponent<AudioSource>();
        whirlwind = Resources.Load<GameObject>("Prefabs/Whirlwind_Ver2");
        isSkillOn = false;
    }
    
    void Update()
    {
        Debug.Log(isSkillOn);
        curTime -= Time.deltaTime;
        if (coolTimeSlider != null)
        {
            if (!isSliderInit)
            {
                coolTimeSlider.maxValue = coolTime;
                isSliderInit = true;
            }
            coolTimeSlider.value = curTime;
        }

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
        isSkillOn = true;

        if (curTime > 0)
        {
            return;
        }
        curTime = coolTime;

        StartCoroutine(ActiveWhirlwind());
    }

    IEnumerator ActiveWhirlwind()
    {
        GameObject bladeEffect = Instantiate(whirlwind);

        player.speed = 5.0f;
        player.playerConfine("Attack");
        player.playerConfine("Dash");
        player.playerConfine("Skill");

        while (isSkillOn)
        {
            Debug.Log("µå°¬³ª");
            bladeEffect.transform.position = player.transform.position;
            skillDuration += Time.deltaTime;

            if (skillDuration >= maxSkillDuration)
            {
                break;
            }
        }

        player.speed = 8.0f;
        player.playerFree();

        isSkillOn = false;
        Destroy(bladeEffect);
        skillDuration = 0.0f;

        yield return null;
    }
}
