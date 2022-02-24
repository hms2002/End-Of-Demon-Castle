using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_ID : MonoBehaviour
{
    public int ID;
    public float coolTime;
    public float curTime = 0;
    public KeyCode skillKey;
    public Slider coolTimeSlider;
    public bool skillCoolTimeStop = false;
    
    public virtual void SkillOn()
    {
    }
}
