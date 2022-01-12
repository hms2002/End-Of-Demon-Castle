using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    //스킬 키 적용
    public delegate void ActivateSkill();
    public ActivateSkill q;
    public ActivateSkill e;
    public ActivateSkill r;
    public ActivateSkill rMouseButton;
    public ActivateSkill leftShift;

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            q();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            e();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            r();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rMouseButton();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            leftShift();
        }
    }
}
