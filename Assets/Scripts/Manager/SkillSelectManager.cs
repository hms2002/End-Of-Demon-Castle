using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SkillSelectManager : MonoBehaviour
{
    private static SkillSelectManager skillSelectManager;
    public static SkillSelectManager GetInstance()
    {
        if(skillSelectManager == null)
        {
            skillSelectManager = FindObjectOfType<SkillSelectManager>();
        }
        return skillSelectManager;
    }

    public Action Init;

    public GameObject player;
    public SkillSlot[] skillSlot;
    public List<Skill_ID> skillList = new List<Skill_ID>();
    public GameObject slotParent;

    public Image[] shownSolts;

    GameObject text;

    PlayerSkill playerSkill;

    public void OpenSkillSetting()
    {
        Player.GetInstance().isCanFree = false;
        Debug.Log("FreeCancle : 1");
        Player.GetInstance().playerConfine();
        Debug.Log("FreeConfi : 2");
        Player.GetInstance().playerConfine("Skill");
        Debug.Log("FreeConfi : 3");
        if (Init != null)
        {
            Init();
            SetCursor.GetInstance().ChangeCursor(3);
            Init = null;

        }
    }


    private void Awake() {
        skillSlot = slotParent.GetComponentsInChildren<SkillSlot>();
        playerSkill = FindObjectOfType<PlayerSkill>();
        text = null;
    }

    public void EndSelect()
    {
        Player.GetInstance().isCanFree = true;
        Player.GetInstance().playerFree();
        Player.GetInstance().playerFree("Skill");
        SetCursor.GetInstance().ChangeCursor(0);
        while (skillList.Count > 0)
        {
            Destroy(skillList[0]);
            skillList.RemoveAt(0);
        }

        if(text != null)
        {
            Destroy(text);
            text = null;
        }
        for(int i = 0; i < skillSlot.Length; i++)
        {
            //스킬 이미지 플레이 화면에도 적용
            shownSolts[i].sprite = skillSlot[i].gameObject.GetComponent<Image>().sprite;

            //스킬슬롯 확인 후 컴포넌트 채우기 - switch문 까지 
            if(skillSlot[i].skill == null)
            {
                skillList.Add(player.AddComponent<Skill_ID>());
            }
            else{
                int job = skillSlot[i].skill.ID/10;
                int skills = skillSlot[i].skill.ID % 10;
                switch(job)
                {
                    case 1:
                        switch (skills)
                        {
                            case 1:
                                skillList.Add(player.AddComponent<PlayerSkill_WhirlWind>());
                                break;
                            case 2:
                                skillList.Add(player.AddComponent<PlayerSkill_ChargeAttack>());
                                break;
                            case 3:
                                skillList.Add(player.AddComponent<PlayerSkill_FastSword>());
                                break;
                        }
                        break;
                    case 2:
                        switch (skills)
                        {
                            case 1:
                                skillList.Add(player.AddComponent<PlayerSkill_MultiShot>());
                                break;
                            case 2:
                                skillList.Add(player.AddComponent<PlayerSkill_PortalArrow>());
                                break;
                            case 3:
                                skillList.Add(player.AddComponent<PlayerSkill_SniperArrow>());
                                break;
                        }
                        break;
                    case 3:
                        switch (skills)
                        {
                            case 1:
                                skillList.Add(player.AddComponent<PlayerSkill_ShadowAssault>());
                                break;
                            case 2:
                                skillList.Add(player.AddComponent<PlayerSkill_Venom>());
                                break;
                            case 3:
                                skillList.Add(player.AddComponent<PlayerSkill_BackSteb>());
                                break;
                        }
                        break;
                    case 4:
                        switch (skills)
                        {
                            case 1:
                                skillList.Add(player.AddComponent<PlayerSkill_Explosion>());
                                text = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SKillCountText"));
                                text.transform.SetParent(shownSolts[i].transform);
                                text.transform.localPosition = new Vector3(0, 0, 0);
                                break;
                            case 2:
                                skillList.Add(player.AddComponent<PlayerSkill_FireBreath>());
                                break;
                            case 3:
                                skillList.Add(player.AddComponent<PlayerSkill_Thunder>());
                                break;
                        }
                        break;
                    case 5:
                        switch (skills)
                        {
                            case 1:
                                skillList.Add(player.AddComponent<PlayerSkill_Vampire>());
                                break;
                            case 2:
                                skillList.Add(player.AddComponent<PlayerSkill_PillarOfFire>());
                                break;
                            case 3:
                                skillList.Add(player.AddComponent<PlayerSkill_Berserk>());
                                break;
                        }
                        break;
                    case 6:
                        switch (skills)
                        {
                            case 1:
                                skillList.Add(player.AddComponent<PlayerSkill_Cross_Attack>());
                                break;
                            case 2:
                                skillList.Add(player.AddComponent<PlayerSkill_HealAOE>());
                                break;
                            case 3:
                                skillList.Add(player.AddComponent<PlayerSkill_Pray>());
                                break;
                        }
                        break;
                    default:
                        skillList.Add(player.AddComponent<Skill_ID>());
                        break;
                }
            }

            skillList[i].coolTimeSlider = shownSolts[i].transform.GetChild(0).GetComponent<Slider>();

            if(i == 0)
            {
                playerSkill.rMouseButton = skillList[i].SkillOn;
                skillList[i].skillKey = KeyCode.Mouse1;
            }
            else if(i == 1)
            {
                playerSkill.q = skillList[i].SkillOn;
                skillList[i].skillKey = KeyCode.Q;
            }
            else if(i == 2)
            {
                playerSkill.e = skillList[i].SkillOn;
                skillList[i].skillKey = KeyCode.E;
            }
            else if(i == 3)
            {
                playerSkill.r = skillList[i].SkillOn;
                skillList[i].skillKey = KeyCode.R;
            }
            else if(i == 4)
            {
                playerSkill.leftShift = skillList[i].SkillOn;
                skillList[i].skillKey = KeyCode.LeftShift;
            }
        }
        
    }

    public void CoolTimeInit()
    {
        for(int count = 0; count < skillList.Count; count++)
        {
            skillList[count].curTime = 0;
        }
    }

    public void CoolTimeStop()
    {
        for (int count = 0; count < skillList.Count; count++)
        {
            skillList[count].skillCoolTimeStop = true;
        }
    }

    public void CoolTimeGo()
    {
        for (int count = 0; count < skillList.Count; count++)
        {
            skillList[count].skillCoolTimeStop = false;
        }
    }
}
