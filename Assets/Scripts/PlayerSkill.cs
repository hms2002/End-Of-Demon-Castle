using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    delegate void ActivateSkill();

    ActivateSkill Q;
    ActivateSkill E;
    ActivateSkill R;
    ActivateSkill F;
    ActivateSkill LeftShift;

    float flameTimer;

    private void Awake()
    {
        Q = FireBreath;
        E = Explosion;
        R = IcePrison;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Q();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            E();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            R();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            F();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            LeftShift();
        }
    }

    //피격 스킬
    public void FireBreath()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKeyUp(KeyCode.Q) || /* or 5초 카운트다운*/)
            {
                //원상복귀
            }

            //불 쏘기
        }
        Debug.Log("불 발사~!");
    }

    public void Explosion()
    {
        Debug.Log("펑");
    }

    public void IcePrison()
    {
        Debug.Log("으름 감옥~!");
    }

    //버프 스킬
}
