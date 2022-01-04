using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //#.플레이어 이동
    public int speed;
    float h;
    float v;
    bool canMove;
    //#.플레이어 대쉬
    public float dashCoolTime;
    public int dashPower;
    float curTime;
    Rigidbody2D rigid;
    Animator animator;

    // Start is called before the first frame update
    void Start(){
        canMove = true;
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
        if(Input.GetKeyDown(KeyCode.Space) && curTime <= 0)
        {
            curTime = dashCoolTime;
            animator.SetTrigger("DashOn");
            StartCoroutine("dash");
        }
    }

    private void FixedUpdate() {
        //#1.플레이어 이동
        if(canMove)
            rigid.velocity = new Vector2(h,v) * speed;
    }

    void playerMove()
    {
        if(!canMove)
            return;
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

    IEnumerator dash()
    {
        canMove = false;
        rigid.AddForce(new Vector2(h*dashPower, v*dashPower));
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }
}
