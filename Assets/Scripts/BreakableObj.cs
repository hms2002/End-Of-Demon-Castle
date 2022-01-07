using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObj : MonoBehaviour
{
    public int Hp = 2;
    Animator anim;
    bool isBroke;
    bool isFirst;
    private void Start() {
        isBroke = false;
        isFirst = true;
        anim = GetComponent<Animator>();
    }
    public virtual void breakObj()
    {
        if (isBroke)
            return;
        if (isFirst)
        {
            isFirst = false;
            Hp -= 1;
            anim.SetTrigger("Hit");
        }
        else
        {
            Hp -= 1;
            isFirst = true;
        }
        if (Hp <= 0)
        {
            isBroke = true;
            anim.SetTrigger("Break");
            Invoke("restore", 3);
        }
    }

    public void restore()
    {
        isBroke = false;
        anim.SetTrigger("Restore");
        Hp = 2;
       
    }

}