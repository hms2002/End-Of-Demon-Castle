using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_BackSteb : Skill_ID
{
    SetCursor setCursor;

    void Start()
    {
        setCursor = Camera.main.gameObject.GetComponent<SetCursor>();
    }

    void Update()
    {
        
    }

    public override void SkillOn()
    {
        setCursor.ChangeCursor("backSteb");
    }
}
