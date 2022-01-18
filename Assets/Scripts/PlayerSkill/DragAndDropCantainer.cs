using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//DragPlus, SkillSlot과 연계됨
public class DragAndDropCantainer : MonoBehaviour
{
    public Image image;
    public Skill_ID skill;
    //public DragPlus beforeSkill;


    void Start()
    {
        gameObject.SetActive(false);
    }
}
