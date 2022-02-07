using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //#.능력치
    public float player_hp;
    public float maxHP;

    PlayerSkill playerSkill;

    //#.플레이어 이동
    AudioSource audioSrc;
    public float speed;
    float h;
    float v;
    bool canMove;
    int playerOrient;
    //#.플레이어 대쉬
    public GameObject dashEffect;
    public float dashCoolTime;
    public int dashPower;
    float curDashCoolTime;
    public bool canDash = true;
    Rigidbody2D rigid;
    Animator animator;
        //대쉬 쿨타임 슬라이더
        public Slider dashCoolTimeImage;

    //#.플레이어 공격
    public int attackDamage;
        //공격 딜레이
    public float atkCoolTime = 0.25f;
        //연속 공격 쿨타임 + 연속공격 bool
    float continuousAtkTime = 0;
    bool goContinuous = false;

    bool canAttack;
    bool onAttack;
        //프리펩
    public GameObject attackEffect;
    public Transform atkPos;
    public Vector2 boxSize;
    GameObject sword;
        //인스턴스 저장할 오브젝트
    GameObject atkEffect;
    SpriteRenderer swordSprite;
    //#.플레이어 화살
    ObjectManager objectManager;
    public float arrowSpeed = 10f;
    public float arrowCoolTime = 1f;
    float arrowCurTime = 0;

    //#.플레이어 죽음
    bool isDead = false;
    public delegate void AboutDead();
    public AboutDead onDead;

    //#.플레이어 데미지
    SpriteRenderer playerSprite;
    public Material whiteMaterial;
    public Material playerMaterial;

    //
    public static Player player;

    public static Player GetInstance()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        return player;
    }

    void Start()
    {
        dashCoolTimeImage.maxValue = dashCoolTime;

        playerSkill = GetComponent<PlayerSkill>();

        audioSrc = GetComponent<AudioSource>();

        objectManager = FindObjectOfType<ObjectManager>();
        playerSprite = GetComponent<SpriteRenderer>();

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

    void Update()
    {
        //#1.플레이어 이동
        playerMove();
        //#2.플레이어 대쉬
        dash();
        //#3.플레이어 공격
        attack();
        //shotArrow();
        //#4.플레이어 체력체크
        PlayerHP();

    }


    private void FixedUpdate()
    {
        //#1.플레이어 이동
        if (canMove)
        {
            if (Mathf.Abs(h) == 1 && Mathf.Abs(v) == 1)
            {
                rigid.velocity = new Vector2(h, v) * (speed / 1.5f);
            }
            else
            {
                rigid.velocity = new Vector2(h, v) * speed;
            }
        }
        else
        {
            Debug.Log("잡았다");
        }
    }

    void playerMove()
    {
        if (rigid.velocity == Vector2.zero)
            audioSrc.Stop();
        if (!canMove)
        {
            audioSrc.Stop();
            return;
        }
        else
        {
            if (!audioSrc.isPlaying)
                audioSrc.Play();
        }
        if (isDead == true)
            return;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        setPlayerOrientation();

        if (h == 0 && v == 0)
            animator.SetBool("GoIdle", true);
        else
            animator.SetBool("GoIdle", false);
    }

    void setPlayerOrientation()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
        if (angle < 0) angle += 360;
        if (angle <= 22.5f || angle > 337.5f) //오른쪽을 바라봅니다.
        {
            if (playerOrient == 0)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 0;
            animator.SetInteger("hAxisRaw", 1);
            animator.SetInteger("vAxisRaw", 0);
            animator.SetBool("isChange", true);

        }
        else if (22.5f <= angle && angle < 67.5f)
        {
            if (playerOrient == 1)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 1;
            animator.SetInteger("hAxisRaw", 1);
            animator.SetInteger("vAxisRaw", 1);
            animator.SetBool("isChange", true);

        }
        else if (67.5f <= angle && angle < 112.5f) //위를 바라봅니다.
        {
            if (playerOrient == 2)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 2;
            animator.SetInteger("hAxisRaw", 0);
            animator.SetInteger("vAxisRaw", 1);
            animator.SetBool("isChange", true);
        }
        else if (112.5f <= angle && angle < 157.5f)
        {
            if (playerOrient == 3)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 3;
            animator.SetInteger("hAxisRaw", -1);
            animator.SetInteger("vAxisRaw", 1);
            animator.SetBool("isChange", true);

        }
        else if (157.5f <= angle && angle < 202.5f) //왼쪽을 바라봅니다.
        {
            if (playerOrient == 4)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 4;
            animator.SetInteger("hAxisRaw", -1);
            animator.SetInteger("vAxisRaw", 0);
            animator.SetBool("isChange", true);
        }
        else if (202.5f <= angle && angle < 247.5f)
        {
            if (playerOrient == 5)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 5;
            animator.SetInteger("hAxisRaw", -1);
            animator.SetInteger("vAxisRaw", -1);
            animator.SetBool("isChange", true);

        }
        else if (247.5f <= angle && angle < 292.5f) //아래를 바라봅니다.
        {
            if (playerOrient == 6)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 6;
            animator.SetInteger("hAxisRaw", 0);
            animator.SetInteger("vAxisRaw", -1);
            animator.SetBool("isChange", true);
        }
        else if (292.5f <= angle && angle < 337.5f)
        {
            if (playerOrient == 7)
            {
                animator.SetBool("isChange", false);
                return;
            }
            playerOrient = 7;
            animator.SetInteger("hAxisRaw", 1);
            animator.SetInteger("vAxisRaw", -1);
            animator.SetBool("isChange", true);

        }
    }

    void dash()
    {
        curDashCoolTime -= Time.deltaTime;
        dashCoolTimeImage.value = curDashCoolTime;
        if (Input.GetKeyDown(KeyCode.Space) && curDashCoolTime <= 0)
        {
            if (!canDash)
                return;
            if (h == 0 && v == 0)
                return;
            SoundManager.GetInstance().Play("Sound/PlayerSound/DashSound", 2);
            curDashCoolTime = dashCoolTime;
            animator.SetTrigger("DashOn");
            StartCoroutine("IDash");

        }
    }

    void attack()
    {
        continuousAtkTime -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (!canAttack)
                return;
            if (onAttack)
                return;
            if (continuousAtkTime < 0)
            {
                goContinuous = false;
            }
            else
            {
                goContinuous = true;
            }
            onAttack = true;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 orientation = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;
            StartCoroutine("IAttack", orientation);
        }
    }

    void shotArrow()
    {
        arrowCurTime -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && arrowCurTime < 0)
        {
            arrowCurTime = arrowCoolTime;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 v2 = mousePos - (Vector2)transform.position;
            float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;

            GameObject arrow = objectManager.MakeObj("Arrow");
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
            arrow.transform.position = transform.position;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector2 dir = v2.normalized;
            arrowRb.AddForce(new Vector2(dir.x, dir.y) * arrowSpeed, ForceMode2D.Impulse);
        }
        else
            return;
    }

    //#.피해 입기
    public void damaged(int damage)
    {
        StartCoroutine("ShowDamaged");
        player_hp -= damage;
        dead();
    }
    //#.플레이어 체력
    void PlayerHP()
    {
        HPLimit();
        dead();
    }
    //#.플레이어 체력 제한
    void HPLimit()
    {
        if (player_hp > maxHP)
        {
            player_hp = maxHP;
        }
    }

    //#.죽기
    void dead()
    {
        if (isDead)
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        if (player_hp <= 0)
        {
            isDead = true;
            onDead();
        }
    }

    IEnumerator ShowDamaged()
    {
        playerSprite.material = whiteMaterial;
        yield return new WaitForSeconds(0.1f);
        playerSprite.material = playerMaterial;
    }

    IEnumerator IDash()
    {
        canMove = false;
        if (Mathf.Abs(h) == 1 && Mathf.Abs(v) == 1)
            rigid.AddForce(new Vector2(h * (dashPower / 1.5f), v * (dashPower / 1.5f)));
        else
            rigid.AddForce(new Vector2(h * dashPower, v * dashPower));
        gameObject.layer = 11;

        animator.SetInteger("hAxisRaw", (int)h);
        animator.SetInteger("vAxisRaw", (int)v);

        MakeDashEffect();
        yield return new WaitForSeconds(0.3f);
        if (canDash)
        {
            canMove = true;
        }
        gameObject.layer = 10;
        
        //플레이어가 커서 방향 바라보도록 설정
        playerOrient = -1;
        setPlayerOrientation();
    }

    IEnumerator IAttack(Vector2 orientation)
    {
        sword.SetActive(true);
        SoundManager.GetInstance().Play("Sound/PlayerSound/SwordSlash", 0.35f);
        canMove = false;

        rigid.velocity = new Vector2(0, 0);
        setSwordAngle();
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(atkPos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("CanBroke"))
            {
                BreakableObj obj = collider.GetComponent<BreakableObj>();
                obj.breakObj(attackDamage);
            }
            if (collider.CompareTag("Boss"))
            {
                Boss boss = collider.GetComponent<Boss>();
                boss.damaged(attackDamage, "sword");
            }
        }

        yield return new WaitForSeconds(atkCoolTime);

        onAttack = false;
        sword.SetActive(false);
        canMove = true;
    }

    

    void setSwordAngle()
    {
        animator.SetTrigger("Attack");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
        if (angle < 0) angle += 360;

        if (angle <= 22.5f || angle > 337.5f) //오른쪽을 바라봅니다.
        {
            atkEffect = Instantiate(attackEffect, transform.position /*+ new Vector3(0.4f, 0, 0)*/, Quaternion.AngleAxis(-90, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (22.5f <= angle && angle < 67.5f) //오른쪽위를 바라봅니다.
        {
            atkEffect = Instantiate(attackEffect, transform.position /*+ new Vector3(0.2f, 0.2f, 0)*/, Quaternion.AngleAxis(-45, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if (67.5f <= angle && angle < 112.5f) //위를 바라봅니다.
        {
            atkEffect = Instantiate(attackEffect, transform.position /*+ new Vector3(0, 0.4f, 0)*/, Quaternion.AngleAxis(0, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if (112.5f <= angle && angle < 157.5f) //왼쪽위를 바라봅니다.
        {
            atkEffect = Instantiate(attackEffect, transform.position /*+ new Vector3(-0.2f, 0.2f, 0)*/, Quaternion.AngleAxis(45, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(135, Vector3.forward);
            swordSprite.sortingOrder = 9;
        }
        else if (157.5f <= angle && angle < 202.5f) //왼쪽을 바라봅니다.
        {
            atkEffect = Instantiate(attackEffect, transform.position /*+ new Vector3(-0.4f, 0, 0)*/, Quaternion.AngleAxis(90, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (202.5f <= angle && angle < 247.5f) //왼쪽아래를 바라봅니다.
        {
            atkEffect = Instantiate(attackEffect, transform.position /*+ new Vector3(-0.2f, -0.2f, 0)*/, Quaternion.AngleAxis(135, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(225, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (247.5f <= angle && angle < 292.5f) //아래를 바라봅니다.
        {
            atkEffect = Instantiate(attackEffect, transform.position /*+ new Vector3(0, -0.4f, 0)*/, Quaternion.AngleAxis(180, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        else if (292.5f <= angle && angle < 337.5f) //오른쪽아래를 바라봅니다.
        {
            atkEffect = Instantiate(attackEffect, transform.position /*+ new Vector3(0.2f, -0.2f, 0)*/, Quaternion.AngleAxis(225, Vector3.forward));
            Destroy(atkEffect, atkCoolTime);
            sword.transform.rotation = Quaternion.AngleAxis(315, Vector3.forward);
            swordSprite.sortingOrder = 10;
        }
        if (goContinuous)
        {
            atkEffect.transform.Rotate(new Vector3(0, 180, 0));
            //atkEffect.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            continuousAtkTime = 0;
        }
        else
        {
            //atkEffect.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            continuousAtkTime = 1;
        }
    }
    private void MakeDashEffect()
    {
        int angle = 0;
        if(h > 0 && v == 0)//오른쪽
            angle = 0;
        else if(h > 0 && v > 0)//오른쪽위
            angle = 45;
        else if(h == 0 && v > 0)//위
            angle = 90;
        else if(h < 0 && v > 0)//왼쪽위
            angle = 135;
        else if(h < 0 && v == 0)//왼쪽
            angle = 180;
        else if(h < 0 && v < 0)//왼쪽아래   
            angle = 225;
        else if(h == 0 && v < 0)//아래
            angle = 270;
        else if(h > 0 && v < 0)//오른쪽아래
            angle = 315;
        GameObject dashEffectInst = Instantiate(dashEffect, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        dashEffectInst.transform.SetParent(transform);
        Destroy(dashEffectInst, 0.3f);
    }
    //#.공격 범위 유니티에서 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkPos.position, boxSize);
    }
    //#.문 열 때 플레이어 제어
    public void playerConfine()
    {
        canMove = false;
        canAttack = false;
        canDash = false;
        rigid.velocity = Vector2.zero;

        //플레이어가 커서 방향 바라보도록 설정
        playerOrient = -1;
        setPlayerOrientation();
    }

    public void playerFree()
    {
        if (isDead)
        {
            rigid.velocity = Vector2.zero;
            return;
        }
        canAttack = true;
        canMove = true;
        canDash = true;
        playerSkill.canSkill = true;
        animator.SetBool("isChange", false);

//        Debug.Log("넌 자유야");

        //플레이어가 커서 방향 바라보도록 설정
        playerOrient = -1;
        setPlayerOrientation();
    }
    public void playerConfine(string idx)
    {
        switch(idx)
        {
            case "Attack":
                canAttack = false;
                break;
            case "Move":
                canMove = false;
                rigid.velocity = Vector2.zero;

                //플레이어가 커서 방향 바라보도록 설정
                playerOrient = -1;
                setPlayerOrientation();
                animator.SetBool("isChange", false);
                break;
            case "Dash":
                canDash = false;
                break;
            case "Skill":
                Debug.Log("스키ㅣㅣㅣ일 멈춰");
                playerSkill.canSkill = false;
                break;

        }
    }
    public void playerFree(string idx)
    {
        Debug.Log("넌 자유야22");
        switch (idx)
        {
            case "Attack":
                canAttack = true;
                break;
            case "Move":
                canMove = true;
                rigid.velocity = Vector2.zero;

                //플레이어가 커서 방향 바라보도록 설정
                playerOrient = -1;
                setPlayerOrientation();
            break;
            case "Dash":
                canDash = true;
            break;
            case "Skill":
                Debug.Log("스키ㅣㅣㅣ일 멈추지마");
                playerSkill.canSkill = true;
                break;

        }
    }

    public void lookUp()
    {
        animator.SetBool("isChange", true);
        animator.SetInteger("hAxisRaw", 0);
        animator.SetInteger("vAxisRaw", 1);
        animator.SetBool("GoIdle", true);
    }
}