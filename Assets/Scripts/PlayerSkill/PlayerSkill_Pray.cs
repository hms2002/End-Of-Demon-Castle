using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Pray : Skill_ID
{
    Player player;
    Animator animator;

    float skillDuration = 10.0f;
    bool isSkillOn;

    float coolTime = 10.5f;
    float curTime;
    bool isSliderInit = false;


    //#.��ų ������
    GameObject buffICON;
    GameObject tempBuffICON;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();

        //#.��ų ������ �� �ʱ�ȭ
        buffICON = Resources.Load<GameObject>("Prefabs/Buff/Buff_Pray");
        SkillSelectManager.GetInstance().Init += BeforeDEL;
    }

    private void Update()
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

    public override void SkillOn()
    {
        if (curTime > 0)
        {
            return;
        }
        curTime = coolTime;

        if (isSkillOn)
        {
            return;
        }
        isSkillOn = true;

//#.������ Instantiate
        tempBuffICON = Instantiate(buffICON, BuffLayoutSetting.GetInstance().transform);
        BuffLayoutSetting.GetInstance().AddBuff();

        StartCoroutine(ActivePray());
    }

    IEnumerator ActivePray()
    {
        animator.SetBool("GoIdle", false);
        player.playerConfine("Move");
        player.playerConfine("Dash");
        player.playerConfine("Attack");
        player.playerConfine("Skill");
        player.zhonya = true;

        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("prayOn");
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/Pray");
        yield return new WaitForSeconds(skillDuration);

        player.zhonya = false;
        animator.SetBool("GoIdle", true);
        player.playerFree();
        isSkillOn = false;

        //#.������ �����
        Destroy(tempBuffICON);
        BuffLayoutSetting.GetInstance().SubBuff();
    }

    public void BeforeDEL()
    {
        if(isSkillOn)
        {
            Destroy(tempBuffICON);
            BuffLayoutSetting.GetInstance().SubBuff();
            player.zhonya = false;
            animator.SetBool("GoIdle", true);
        }
    }
}
