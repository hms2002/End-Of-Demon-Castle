using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillSelectManager : MonoBehaviour
{
    public GameObject player;
    public SkillSlot[] skillSlot;
    public List<Skill_ID> skillList = new List<Skill_ID>();
    public GameObject slotParent;

    public Image[] shownSolts;

    GameObject text;

    PlayerSkill playerSkill;
    private void Awake() {
        skillSlot = slotParent.GetComponentsInChildren<SkillSlot>();
        playerSkill = FindObjectOfType<PlayerSkill>();
        text = null;
    }

    public void EndSelect()
    {
        while(skillList.Count > 0)
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
                                skillList.Add(player.AddComponent<PlayerSkill_FireBreath>());
                                break;
                            case 2:
                                skillList.Add(player.AddComponent<PlayerSkill_Explosion>());
                                text = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SKillCountText"));
                                text.transform.SetParent(shownSolts[i].transform);
                                text.transform.localPosition = new Vector3(0, 0, 0);
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
}
