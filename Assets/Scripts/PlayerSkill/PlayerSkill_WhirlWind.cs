using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill_WhirlWind : Skill_ID
{
    Player player;
    AudioSource playerAudioSource;
    GameObject whirlwind;
    GameObject bladeEffect;

    bool isSkillOn;
    bool isSliderInit = false;
    float skillDuration = 0.0f;
    float maxSkillDuration = 10.0f;

    void Awake()
    {
        player = GetComponent<Player>();
        playerAudioSource = player.GetComponent<AudioSource>();
        whirlwind = Resources.Load<GameObject>("Prefabs/Whirlwind_Ver2");
        isSkillOn = false;
        coolTime = 10.0f;

        //#.강제 초기화
        SkillSelectManager.GetInstance().Init += BeforeDEL;
    }
    
    void Update()
    {
        if (skillCoolTimeStop == true)
            return;
        if (isSkillOn)
        {
            playerAudioSource.Stop();
        }
        else
        {
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
        }

        if (bladeEffect != null)
        {
            bladeEffect.transform.position = player.transform.position;
        }
    }

    public override void SkillOn()
    {

        if (curTime > 0)
        {
            return;
        }
        isSkillOn = true;
        curTime = coolTime;

        StartCoroutine("ActiveWhirlwind");
    }

    IEnumerator ActiveWhirlwind()
    {
        bladeEffect = Instantiate(whirlwind);

        player.playerConfine("Attack");
        player.playerConfine("Dash");
        player.playerConfine("Skill");

        yield return new WaitForSeconds(0.01f);

        while (isSkillOn)
        {
            skillDuration += Time.deltaTime;

            if (Input.GetKeyDown(skillKey) || skillDuration >= maxSkillDuration)
            {
                break;
            }

            yield return null;
        }

        player.playerFree();

        isSkillOn = false;
        Destroy(bladeEffect);
        skillDuration = 0.0f;
    }
    
    public void BeforeDEL()
    {
        player.playerFree();
        if(bladeEffect != null)
            Destroy(bladeEffect);
    }
}
