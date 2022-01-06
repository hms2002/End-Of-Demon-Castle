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
    //#.플레이어 공격
    public Transform atkPos;
    public Vector2 boxSize;
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
        //#4.플레이어 사망체크
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
                Animator anim = collider.GetComponent<Animator>();
                anim.SetTrigger("Break");
            }
        }

        yield return new WaitForSeconds(0.3f);
        
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
            sword.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if(22.5f <= angle && angle < 67.5f) //오른쪽위를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if(67.5f <= angle && angle < 112.5f) //위를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if(112.5f <= angle && angle < 157.5f) //왼쪽위를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(135, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if (157.5f <= angle && angle < 202.5f) //왼쪽을 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (202.5f <= angle && angle < 247.5f) //왼쪽아래를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(225, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (247.5f <= angle && angle < 292.5f) //아래를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (292.5f <= angle && angle < 337.5f) //오른쪽아래를 바라봅니다.
        {
            sword.transform.rotation = Quaternion.AngleAxis(315, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
    }
    //#.공격 범위 유니티에서 그리기
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkPos.position, boxSize);
    }
}