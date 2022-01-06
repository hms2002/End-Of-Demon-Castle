using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObj : MonoBehaviour
{
    Animator anim;
    bool isBroke;
    private void Start() {
        isBroke = false;
        anim = GetComponent<Animator>();
    }
    public virtual void breakObj()
    {
        if(isBroke)
            return;
        isBroke = true;
        anim.SetTrigger("Break");
        Invoke("restore", 3);
    }

    public void restore()
    {
        isBroke = false;
        anim.SetTrigger("Restore");
    }
}