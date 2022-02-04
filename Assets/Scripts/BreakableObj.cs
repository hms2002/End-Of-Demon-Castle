using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObj : MonoBehaviour
{
    public float Hp = 2;
    protected Animator anim;

    CapsuleCollider2D capsuleCollider2D;

    bool isBroke;
    bool isFirst;
    private void Start() {
        isBroke = false;
        isFirst = true;
        anim = GetComponent<Animator>();

        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    public virtual void breakObj(float damage = 0)
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
            capsuleCollider2D.enabled = false;
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

        capsuleCollider2D.enabled = true;
    }

}