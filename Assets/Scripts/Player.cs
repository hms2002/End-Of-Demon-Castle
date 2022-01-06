using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //#.능력치
    public int player_hp;

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
    GameObject sword;
    SpriteRenderer swordSprite;

    void Start(){
        canMove = true;
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curTime = 0;

        sword = transform.GetChild(0).gameObject;
        sword.SetActive(false);
        swordSprite = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update(){
        //#1.플레이어 이동
        playerMove();
        //#2.플레이어 대쉬
        dash();
        //#3.플레이어 공격
        attack();
        //#4.플레이어 사망
        dead();

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

    void dash(){
        curTime -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && curTime <= 0)
        {
            curTime = dashCoolTime;
            animator.SetTrigger("DashOn");
            StartCoroutine("IDash");

        }
    }

    void attack()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 orientation = new Vector2(mousePos.x - transform.position.x, mousePos.y -  transform.position.y).normalized;
            StartCoroutine("IAttack", orientation);
        }
    }

    public void damaged(int damage)
    {
        player_hp -= damage;
        dead();
    }

    void dead()
    {
        if(player_hp <= 0)
            GetComponent<SpriteRenderer>().color = Color.red;
    }

    IEnumerator IDash()
    {
        canMove = false;
        rigid.AddForce(new Vector2(h*dashPower, v*dashPower));
        gameObject.layer = 11;
        yield return new WaitForSeconds(0.3f);
        canMove = true;
        gameObject.layer = 10;
    }

    IEnumerator IAttack(Vector2 orientation)
    {
        sword.SetActive(true);
        canMove = false;

        rigid.velocity = new Vector2(0,0);
        /*        sword.transform.localPosition = new Vector3(orientation.x, orientation.y, 0);
        float Dot = Vector2.Dot(orientation, Vector2.right);
        float Angle = Mathf.Atan2(orientation.x, orientation.y);
        sword.transform.rotation = Quaternion.Euler(new Vector3(0,0,Angle));*/

    
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y,v2.x) * 180/Mathf.PI;
        if(angle < 0) angle += 360;
        if(angle > 45f && angle < 135f) //위를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if (angle > 135f && angle < 225f) //옆를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (angle > 225f && angle < 315f) //아래를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (angle < 45f || angle > 315f) //오른쪽을 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }

        yield return new WaitForSeconds(0.3f);
        
        sword.SetActive(false);
        canMove = true;
    }
}