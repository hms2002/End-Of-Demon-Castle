using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_ID : MonoBehaviour
{
    public int ID;
    public KeyCode skillKey;
    public Slider coolTimeSlider;

    public virtual void SkillOn()
    {
    }
}
