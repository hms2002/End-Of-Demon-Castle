using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //#.플레이어 이동
    float h;
    float v;
    public int speed;
    //#.플레이어 대쉬
    float curTime;
    public float dashCoolTime;
    public int dashPower;
    Rigidbody2D rigid;
    Animator animator;

    // Start is called before the first frame update
    void Start(){
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curTime = 0;
    }

    // Update is called once per frame
    void Update(){
        //#1.플레이어 이동
        playerMove();
        //#2.플레이어 대쉬
        curTime -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift) && curTime <= 0)
        {
            curTime = dashCoolTime;
            animator.SetTrigger("DashOn");
            speed+=dashPower;
        }
    }

    private void FixedUpdate() {
        //#1.플레이어 이동
        rigid.velocity = new Vector2(h,v) * speed;
    }

    void playerMove()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");  

    
        if(animator.GetInteger("hAxisRaw") != h){
            animator.SetBool("isChange", true);
            animator.SetInteger("hAxisRaw", (int)h);
        }
        else if(animator.GetInteger("vAxisRaw") != v){
            animator.SetBool("isChange", true);
            animator.SetInteger("vAxisRaw", (int)v);
        }
        else
            animator.SetBool("isChange", false);

        if(h == 0 && v == 0)
            animator.SetBool("GoIdle", true);
        else
            animator.SetBool("GoIdle", false);
    }

    public void dashEnd()
    {
        speed -= dashPower;
    }
}
