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
    public bool canSkill;

    private void Start() {
        canSkill = true;
    }
    
    private void Update()
    {
        PlayerSkill[] player = FindObjectsOfType<PlayerSkill>();
        
        if (canSkill == false)
            return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(q != null)
                q();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if(e != null)
                e();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if(r != null)
                r();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if(rMouseButton != null)
                rMouseButton();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(leftShift != null)
                leftShift();
        }
    }
}
