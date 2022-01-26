using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cristal : BreakableObj
{
    Slider HpSlider;
    

    private void Awake()
    {
        HpSlider = transform.parent.GetChild(1).GetChild(0).GetComponent<Slider>();
        anim = GetComponent<Animator>();

        Hp = 175;
        HpSlider.maxValue = Hp;
    }

    public override void breakObj(float damage)
    {
        Hp -= damage;
        if (Hp > 87) { }
        else if (Hp > 0)
        {
            anim.SetTrigger("Break");
        }
        else
        {
            Hp = 0;
            Destroy(transform.parent.gameObject);
        }

        HpSlider.value = Hp;
    }
}
