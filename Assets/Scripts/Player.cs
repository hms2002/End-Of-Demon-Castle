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
    int playerOrient;
    //#.플레이어 대쉬
    public float dashCoolTime;
    public int dashPower;
    float curDashCoolTime;
    Rigidbody2D rigid;
    Animator animator;
    //#.플레이어 공격
    public float atkCoolTime = 0.3f;
    bool canAttack;
    bool onAttack;
    public GameObject attackEffect;
    public Transform atkPos;
    public Vector2 boxSize;
    GameObject sword;
    SpriteRenderer swordSprite;

    void Start(){
        canMove = true;
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curDashCoolTime = 0;

        sword = transform.GetChild(0).gameObject;
        sword.SetActive(false);
        swordSprite = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
    
        onAttack = false;
        canAttack = true;
    }

    void Update(){
        //#1.플레이어 이동
        playerMove();
        //#2.플레이어 대쉬
        dash();
        //#3.플레이어 공격
        attack();
        //#4.플레이어 사망체크
        dead();

    }

    private void FixedUpdate() {
        //#1.플레이어 이동
        if(canMove)
        {
            if(Mathf.Abs(h) == 1 && Mathf.Abs(v) == 1)
            {
                rigid.velocity = new Vector2(h,v) * (speed/1.5f);
            }
            else
            {  
                rigid.velocity = new Vector2(h,v) * speed;
            }
        }
    }

    void playerMove()
    {
        if(!canMove)
            return;
        Debug.Log("Ent");
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");  

        setPlayerOrientation();
        
        if(h == 0 && v == 0)
            animator.SetBool("GoIdle", true);
        else
            animator.SetBool("GoIdle", false);
    }
    
    void setPlayerOrientation()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y,v2.x) * 180/Mathf.PI;
        if(angle < 0) angle += 360;
        if(angle <= 22.5f || angle > 337.5f) //오른쪽을 바라봅니다.
        {
            if(playerOrient == 0)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 0;
            animator.SetInteger("hAxisRaw", 1);
            animator.SetInteger("vAxisRaw", 0);
            animator.SetBool("isChange", true);
            
        }
        else if(67.5f <= angle && angle < 112.5f) //위를 바라봅니다.
        {
            if(playerOrient == 1)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 1;
            animator.SetInteger("hAxisRaw", 0);
            animator.SetInteger("vAxisRaw", 1);
            animator.SetBool("isChange", true);
        }
        else if (157.5f <= angle && angle < 202.5f) //왼쪽을 바라봅니다.
        {
            if(playerOrient == 2)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 2;
            animator.SetInteger("hAxisRaw", -1);
            animator.SetInteger("vAxisRaw", 0);
            animator.SetBool("isChange", true);
        }
        else if (247.5f <= angle && angle < 292.5f) //아래를 바라봅니다.
        {
            if(playerOrient == 3)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 3;
            animator.SetInteger("hAxisRaw", 0);
            animator.SetInteger("vAxisRaw", -1);
            animator.SetBool("isChange", true);
        }
    }

    void dash(){
        curDashCoolTime -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && curDashCoolTime <= 0)
        {
            if(h == 0 && v == 0)
                return;
            
            curDashCoolTime = dashCoolTime;
            animator.SetTrigger("DashOn");
            StartCoroutine("IDash");

        }
    }

    void attack()
    {
        if(Input.GetMouseButtonDown(0)){
            if(!canAttack)
                return;
            if(onAttack)
                return;
            onAttack = true;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 orientation = new Vector2(mousePos.x - transform.position.x, mousePos.y -  transform.position.y).normalized;
            StartCoroutine("IAttack", orientation);
        }
    }

    //#.피해 입기
    public void damaged(int damage)
    {
        player_hp -= damage;
        dead();
    }
    //#.죽기
    void dead()
    {
        if(player_hp <= 0)
            GetComponent<SpriteRenderer>().color = Color.red;
    }

    IEnumerator IDash()
    {
        canMove = false;
        if(Mathf.Abs(h) == 1  && Mathf.Abs(v) == 1)
            rigid.AddForce(new Vector2(h*(dashPower/1.5f), v*(dashPower/1.5f)));        
        else
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
        setSwordAngle();
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(atkPos.position, boxSize, 0);
        foreach(Collider2D collider in collider2Ds)
        {
            if(collider.CompareTag("CanBroke"))
            {
                BreakableObj obj = collider.GetComponent<BreakableObj>();
                obj.breakObj();
            }
        }

        yield return new WaitForSeconds(atkCoolTime);

        onAttack = false;
        sword.SetActive(false);
        canMove = true;
    }


    void setSwordAngle()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y,v2.x) * 180/Mathf.PI;
        if(angle < 0) angle += 360;
        Debug.Log(angle);
        if(angle <= 22.5f || angle > 337.5f) //오른쪽을 바라봅니다.
        {
            GameObject atkEffect = Instantiate(attackEffect, transform.position + new Vector3(0.4f,0,0), Quaternion.AngleAxis(180, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if(22.5f <= angle && angle < 67.5f) //오른쪽위를 바라봅니다.
        {
            GameObject atkEffect = Instantiate(attackEffect, transform.position + new Vector3(0.2f,0.2f,0), Quaternion.AngleAxis(225, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if(67.5f <= angle && angle < 112.5f) //위를 바라봅니다.
        {
            GameObject atkEffect = Instantiate(attackEffect, transform.position + new Vector3(0,0.4f,0), Quaternion.AngleAxis(270, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if(112.5f <= angle && angle < 157.5f) //왼쪽위를 바라봅니다.
        {
            GameObject atkEffect = Instantiate(attackEffect, transform.position + new Vector3(-0.2f,0.2f,0), Quaternion.AngleAxis(315, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(135, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if (157.5f <= angle && angle < 202.5f) //왼쪽을 바라봅니다.
        {
            GameObject atkEffect = Instantiate(attackEffect, transform.position + new Vector3(-0.4f,0,0), Quaternion.AngleAxis(0, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (202.5f <= angle && angle < 247.5f) //왼쪽아래를 바라봅니다.
        {
            GameObject atkEffect = Instantiate(attackEffect, transform.position + new Vector3(-0.2f,-0.2f,0), Quaternion.AngleAxis(45, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(225, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (247.5f <= angle && angle < 292.5f) //아래를 바라봅니다.
        {
            GameObject atkEffect = Instantiate(attackEffect, transform.position + new Vector3(0,-0.4f,0), Quaternion.AngleAxis(90, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (292.5f <= angle && angle < 337.5f) //오른쪽아래를 바라봅니다.
        {
            GameObject atkEffect = Instantiate(attackEffect, transform.position + new Vector3(0.2f,-0.2f,0), Quaternion.AngleAxis(135, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(315, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
    }
    //#.공격 범위 유니티에서 그리기
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkPos.position, boxSize);
    }
    //#.문 열 때 플레이어 제어
    public void playerConfine()
    {
        canMove = false;
        canAttack = false;
     
        rigid.velocity = Vector2.zero;
        Debug.Log(rigid.velocity);

        animator.SetBool("isChange", true);
        animator.SetBool("GoIdle", true);
        animator.SetInteger("hAxisRaw", 0);
        animator.SetInteger("vAxisRaw", 1);
    }

    public void playerFree()
    {
        canAttack = true;
        canMove = true;
        animator.SetBool("isChange", false);
    }
}