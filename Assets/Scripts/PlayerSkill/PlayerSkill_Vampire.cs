using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Vampire : Skill_ID
{
    Player player;
    Boss boss;
    Cristal[] cristal;
    public GameObject healSkill;
    public GameObject absorbBlood;
    GameObject buffICON;
    GameObject tempHeal;
    GameObject darkFlame;
    AudioSource soundPlayer;
    AudioClip continuousSound;

    bool isSkillOn;
    float skillDuration = 0.0f;
    float maxSkillDuration = 5.5f;
    int amountOfRecovery = 3;

    bool isSliderInit = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        boss = FindObjectOfType<Boss>();
        cristal = FindObjectsOfType<Cristal>();
        healSkill = Resources.Load<GameObject>("Prefabs/Heal");
        darkFlame = Resources.Load<GameObject>("Prefabs/DarkFlame");
        absorbBlood = Resources.Load<GameObject>("Prefabs/AbsorbBlood");
        continuousSound = Resources.Load<AudioClip>("Sound/PlayerSound/SkillSound/VampireLoop");

        //#.스킬 아이콘 및 강제 초기화
        buffICON = Resources.Load<GameObject>("Prefabs/Buff/Buff_Absorption");
        SkillSelectManager.GetInstance().Init += BeforeDEL;

        isSkillOn = false;

        curTime = 0;
        coolTime = 35;
    }

    private void Update()
    {
        curTime -= Time.deltaTime;
        if(coolTimeSlider != null)
        {
            if(!isSliderInit)
            {
                coolTimeSlider.maxValue = coolTime;
                isSliderInit = true;
            }
            coolTimeSlider.value = curTime;
        }
    }

    public override void SkillOn()
    {
        if(curTime > 0)
        {
            return;
        }
        curTime = coolTime;

        if (isSkillOn)
        {
            return;
        }
        isSkillOn = true;

        StartCoroutine("Absorb");
    }
    //임시 변수 강제 초기화를 위해 전역으로 변경
    GameObject aura;
    GameObject tempICON;
    IEnumerator Absorb()
    {
        aura = Instantiate(absorbBlood);
        soundPlayer = aura.GetComponent<AudioSource>();
        tempICON = Instantiate(buffICON, BuffLayoutSetting.GetInstance().transform);
        BuffLayoutSetting.GetInstance().AddBuff();
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/VampireWave", 0.35f);
        StartCoroutine(PlayVampireLoopSound());

        GameObject darkTemp = Instantiate(darkFlame, Player.GetInstance().transform);
        darkFlame.transform.localPosition = new Vector3(0, 0, 0);

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

            for (int i = 0; i < cristal.Length; i++)
            {
                if (cristal[i].wasHit == true)
                {
                    SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/VampireHit", 0.4f);
                    tempHeal = Instantiate(healSkill);
                    tempHeal.transform.position = player.transform.position;
                    player.player_hp += amountOfRecovery;
                    Destroy(tempHeal, 0.5f);
                    cristal[i].wasHit = false;
                }
            }

            yield return null;
        }

        BuffLayoutSetting.GetInstance().SubBuff();
        skillDuration = 0.0f;
        Destroy(aura);
        Destroy(tempICON);
        isSkillOn = false;
    }

    IEnumerator PlayVampireLoopSound()
    {
        yield return new WaitForSeconds(0.5f);
        soundPlayer.clip = continuousSound;
        soundPlayer.volume = 0.3f;
        soundPlayer.Play();
    }

    public void BeforeDEL()
    {
        if(isSkillOn == true)
        {
            Destroy(tempICON);
            BuffLayoutSetting.GetInstance().SubBuff();
            skillDuration = 0.0f;
            Destroy(aura);
        }
    }
}
