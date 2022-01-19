using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectManager : MonoBehaviour
{
    public GameObject player;
    public SkillSlot[] skillSlot;
    public List<Skill_ID> skillList = new List<Skill_ID>();
    public GameObject slotParent;

    public PlayerSkill playerSkill;
    private void Awake() {
        skillSlot = slotParent.GetComponentsInChildren<SkillSlot>();
        playerSkill = FindObjectOfType<PlayerSkill>();
    }

    public void EndSelect()
    {
        while(skillList.Count > 0)
        {
            Destroy(skillList[0]);
            skillList.RemoveAt(0);
        }

        for(int i = 0; i < skillSlot.Length; i++)
        {
            if(skillSlot[i].skill == null)
            {
                skillList.Add(player.AddComponent<Skill_ID>());
                continue;
            }
            
                int job = skillSlot[i].skill.ID/10;
                int skills = skillSlot[i].skill.ID % 10;
            switch(job)
            {
                case 1:
                    switch (skills)
                    {
                        case 1:
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
                            break;
                    }
                    break;
                case 3:
                    switch (skills)
                    {
                        case 1:
                            break;
                        case 2:
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
                            break;
                    }
                    break;
                case 6:
                    switch (skills)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                    }
                    break;
                default:
                    break;
            }

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