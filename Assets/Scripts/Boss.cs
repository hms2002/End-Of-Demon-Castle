using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss boss;
    public static Boss GetInstance()
    {
        if (boss == null)
        {
            boss = FindObjectOfType<Boss>();
        }
        return boss;
    }

    public float BossHp = 700;
    public bool wasHit;
    public float PatternTime = 2f;
    public int PatternNum = 0;
    public int PrePatternNum = 0;
    public float BarrageSpeed;
    public float BossSpeed;
    public Player player;
    public ObjectManager objectManager;
    public GameObject[] cristal = new GameObject[4];
    public int CristalNum = 4;
    public BoxCollider2D boxCollider;
    public bool Phase1;
    public bool Phase2 = false;
    public bool Phase3 = false;
    public bool BossStop = true;
    public bool BossVanished = true;
    public Rigidbody2D rigid;
    public Animator anim;

    //피격 이펙트 프리펩
    GameObject damagedEffect;

    void Start()
    {
        Phase1 = true;
        wasHit = false;
        BossSpeed = 3.5f;
        player = Player.GetInstance();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        damagedEffect = Resources.Load<GameObject>("Prefabs/Damaged_Effect(BOSS_CRIST)");
    }

    void Update()
    {
        if (!boxCollider.enabled)
        {
            if (CristalNum == 0 && Phase1)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
                StopAllCoroutines();
                boxCollider.enabled = true;
                Phase1 = false;
                Phase2 = true;
                BossStop = false;
                TextManager.GetInstance().BossPhase2On(0);
            }
        }
        if (BossHp <= 0 && Phase2)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            StopAllCoroutines();
            boxCollider.enabled = false;
            Phase2 = false;
            Phase3 = true;
            BossStop = false;
            rigid.velocity = Vector2.zero;
            StartCoroutine("HpRaise");
            TextManager.GetInstance().BossPhase3On(0);
        }
        if (BossHp <= 0 && Phase3)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            StopAllCoroutines();
            Phase3 = false;
            BossStop = false;
            rigid.velocity = Vector2.zero;
            TextManager.GetInstance().BossDead(0);
        }
    }

    public void PatternManager()
    {
        SoundManager.GetInstance().Play("Sound/BossSound/PatternSound", 0.8f);
        if (Phase1 && BossStop)
        {
            do
            {
                PatternNum = Random.Range(1, 8);
            } while (PatternNum == PrePatternNum);

            PrePatternNum = PatternNum;

            switch (PatternNum)
            {
                case 1:
                    StartCoroutine("Pattern_1");
                    break;
                case 2:
                    StartCoroutine("Pattern_5");
                    break;
                case 3:
                    StartCoroutine("Pattern_6");
                    break;
                case 4:
                    StartCoroutine("Pattern_9");
                   break;
                case 5:
                    StartCoroutine("Pattern_11");
                    break;
                case 6:
                    StartCoroutine("Pattern_13");
                    break;
                case 7:
                    StartCoroutine("Pattern_14");
                    break;
            }
        }
        if(Phase2 && BossStop)
        {
            do
            {
                PatternNum = Random.Range(1, 11);
            } while (PatternNum == PrePatternNum);

            PrePatternNum = PatternNum;

            switch (PatternNum)
            {
                case 1:
                    StartCoroutine("Pattern_1");
                    break;
                case 2:
                    StartCoroutine("Pattern_2");
                    break;
                case 3:
                    StartCoroutine("Pattern_3");
                    break;
                case 4:
                    StartCoroutine("Pattern_5");
                    break;
                case 5:
                    StartCoroutine("Pattern_6");
                    break;
                case 6:
                    StartCoroutine("Pattern_7");
                    break;
                case 7:
                    StartCoroutine("Pattern_8");
                    break;
                case 8:
                    StartCoroutine("Pattern_9");
                    break;
                case 9:
                    StartCoroutine("Pattern_10");
                    break;
                case 10:
                    StartCoroutine("Pattern_11");
                    break;
                
            }
        }

        if (Phase3 && BossStop)
        {
            do
            {
                PatternNum = Random.Range(1, 8);
            } while (PatternNum == PrePatternNum);

            PrePatternNum = PatternNum;

            switch (PatternNum)
            {
                case 1:
                    StartCoroutine("Pattern_2");
                    break;
                case 2:
                    StartCoroutine("Pattern_3");
                    break;
                case 3:
                    StartCoroutine("Pattern_7");
                    break;
                case 4:
                    StartCoroutine("Pattern_8");
                    break;
                case 5:
                    StartCoroutine("Pattern_10");
                    break;
                case 6:
                    StartCoroutine("Pattern_11");
                    break;
                case 7:
                    StartCoroutine("Pattern_12");
                    break;
            }
        }
    }

    private IEnumerator Boss_Scw()
    {
        yield return new WaitForSeconds(PatternTime);
        GameObject Shockwave = objectManager.MakeObj("Shockwave");
        Shockwave.transform.position = transform.position;
        while (Shockwave.activeSelf)
            yield return new WaitForSeconds(0.1f);
        Invoke("PatternManager", 0f);
    }

    private IEnumerator Boss_CristalSet()
    {
        Cristal[] cristalLogic = new Cristal[4];
        for (int i = 0; i < 4; i++)
        {
            if (cristal[i] == null)
            {
                continue;
            }
            cristalLogic[i] = cristal[i].GetComponentInChildren<Cristal>();
        }
        for (int i = 0; i < 4; i++)
        {
            if(cristal[i] == null)
            {
                continue;
            }
            cristalLogic[i].StartCoroutine("CristalSet");
        }
        yield break;
    }

    private IEnumerator Boss_Set()
    {
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.magenta;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    //패턴1 : 부채꼴 모양 발사
    private IEnumerator Pattern_1()
    {
        StartCoroutine("Boss_Set");
        if(Phase2)
        {
            StartCoroutine("playerTracking");
        }
        for (int i = 0; i < 6; i++)
        {
            for (float angle = 0.9f; angle < 5.4f; angle = angle + 1.8f)
            {
                GameObject Barrage = objectManager.MakeObj("Barrage");
                Barrage.transform.position = transform.position;
                BarrageSpeed = 15f; 
                Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
                Vector2 playerdir = player.transform.position - transform.position;
                Vector2 angledir = new Vector2(angle, angle / 2);
                playerdir += angledir;
                rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            }
            for (float angle = 0.9f; angle < 5.4f; angle = angle + 1.8f)
            {
                GameObject Barrage = objectManager.MakeObj("Barrage");
                Barrage.transform.position = transform.position;
                BarrageSpeed = 15f;
                Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
                Vector2 playerdir = player.transform.position - transform.position;
                Vector2 angledir = new Vector2(-angle, angle / 2);
                playerdir += angledir;
                rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            }
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
            yield return new WaitForSeconds(0.5f);
        }
        if (Phase2)
        {
            StopCoroutine("playerTracking");
            rigid.velocity = Vector2.zero;
            StartCoroutine("Boss_Scw");
            yield break;
        }
        Invoke("PatternManager", 2f);
    }

    //패턴2 : 레이저 세로발사
    private IEnumerator Pattern_2()
    {
        StartCoroutine("Boss_Set");
        yield return new WaitForSeconds(0.3f);
        transform.position = new Vector2(0, 26);
        for (int j = 0; j <= 1; j++)
        {
            for (int i = 1; i <= 4; i++)
            {
                float yPlus = 0f;
                switch (i)
                {
                    case 1:
                        yPlus = 0;
                        break;
                    case 2:
                        yPlus = 5;
                        break;
                    case 3:
                        yPlus = 5;
                        break;
                    case 4:
                        yPlus = 4;
                        break;
                }
                GameObject Laser = objectManager.MakeObj("Laser");
                Laser.transform.position = new Vector2(-22 + (8 * i), 18.5f + yPlus);
                Laser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
            SoundManager.GetInstance().Play("Sound/BossSound/LaserSound", 0.1f);
            yield return new WaitForSeconds(1f);
            for (int i = 1; i <= 4; i++)
            {
                float yPlus = 0f;
                switch (i)
                {
                    case 1:
                        yPlus = 4;
                        break;
                    case 2:
                        yPlus = 5;
                        break;
                    case 3:
                        yPlus = 5;
                        break;
                    case 4:
                        yPlus = 4;
                        break;
                }
                GameObject Laser = objectManager.MakeObj("Laser");
                Laser.transform.position = new Vector2(22 - (8 * i), 18.5f + yPlus);
                Laser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
            SoundManager.GetInstance().Play("Sound/BossSound/LaserSound", 0.1f);
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("Boss_Scw");
    }

    //패턴3 : 레이저 가로발사
    private IEnumerator Pattern_3()
    {
        StartCoroutine("Boss_Set");
        yield return new WaitForSeconds(0.3f);
        transform.position = new Vector2(0, 26);
        for (int j = 0; j <= 1; j++)
        {
            for (int i = 1; i <= 3; i++)
            {
                GameObject Laser = objectManager.MakeObj("Laser");
                Laser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                Laser.transform.localScale = new Vector3(5.6f, 30f, 1);
                Laser.transform.position = new Vector2(0, 12f + (8 * i));
            }
            SoundManager.GetInstance().Play("Sound/BossSound/LaserSound", 0.1f);
            yield return new WaitForSeconds(1f);
            for (int i = 1; i <= 2; i++)
            {
                GameObject Laser = objectManager.MakeObj("Laser");
                SpriteRenderer LaserRenderer = Laser.GetComponent<SpriteRenderer>();
                LaserRenderer.flipY = true;
                Laser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                Laser.transform.localScale = new Vector3(5.6f, 30f, 1);
                Laser.transform.position = new Vector2(0, 16f + (8 * i));
            }
            SoundManager.GetInstance().Play("Sound/BossSound/LaserSound", 0.1f);
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("Boss_Scw");
    }
    
    //패턴5 : 탄막 빠르게 15개 플레이어방향으로
    private IEnumerator Pattern_5()
    {
        StartCoroutine("Boss_Set");
        yield return new WaitForSeconds(0.3f);
        if (Phase2)
        {
            StartCoroutine("playerTracking");
        }
        for (int i = 1; i <= 15; i++)
        {
            GameObject Barrage = objectManager.MakeObj("Barrage");
            Barrage.transform.position = transform.position;
            BarrageSpeed = 35;
            Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
            Vector2 playerdir = player.transform.position - transform.position;
            rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
            yield return new WaitForSeconds(0.05f);
            if (i % 3 == 0)
            {
                yield return new WaitForSeconds(1f);
            }
        }
        if (Phase2)
        {
            StopCoroutine("playerTracking");
            rigid.velocity = Vector2.zero;
            StartCoroutine("Boss_Scw");
            yield break;
        }
        Invoke("PatternManager", 2f);
    }

    //패턴6 : 맵밖에서 원형으로 감싸는 탄막
    public IEnumerator Pattern_6()
    {
        StartCoroutine("Boss_Set");
        int RoundNum = 8;
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < RoundNum; i++)
            {
                BarrageSpeed = 15;
                GameObject Barrage = objectManager.MakeObj("Barrage");
                Barrage barrageLogic = Barrage.GetComponent<Barrage>();
                barrageLogic.breakableLayer = 14;
                Vector2 Rounddir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / RoundNum) * 20, Mathf.Sin(Mathf.PI * 2 * i / RoundNum) * 20);
                Barrage.transform.position = new Vector2(player.transform.position.x + Rounddir.x, player.transform.position.y + Rounddir.y);
                Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
                Vector2 playerdir = player.transform.position - Barrage.transform.position;
                Vector2 Forcedir = new Vector2(playerdir.normalized.x, playerdir.normalized.y);
                rigid.AddForce(new Vector2(Forcedir.x, Forcedir.y) * BarrageSpeed, ForceMode2D.Impulse);
            }
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
            yield return new WaitForSeconds(3);
        }
        if(Phase2)
        {
            StartCoroutine("Boss_Scw");
            yield break;
        }
        Invoke("PatternManager", 2f);
    }

    //패턴7 : 플레이어 방향으로 레이저
    private IEnumerator Pattern_7()
    {
        StartCoroutine("Boss_Set");
        yield return new WaitForSeconds(0.3f);
        int RandomNum = Random.Range(0, 1);
        Vector2 target;
            for (int i = 0; i < 4; i++)
            {
                if (RandomNum == 0)
                {
                    switch (i)
                    {
                        case 0:
                            target = new Vector2(-13, 30);
                        transform.position = target;
                            break;
                        case 1:
                            target = new Vector2(13, 30);
                        transform.position = target;
                        break;
                        case 2:
                            target = new Vector2(-13, 21);
                        transform.position = target;
                        break;
                        case 3:
                            target = new Vector2(13, 21);
                        transform.position = target;
                        break;
                    }
                }   
                if (RandomNum == 1)
                {
                    switch (i)
                    {
                        case 0:
                        target = new Vector2(13, 30);
                        transform.position = target;
                        break;
                        case 1:
                        target = new Vector2(13, 21);
                        transform.position = target;
                        break;
                        case 2:
                        target = new Vector2(-13, 30);
                        transform.position = target;
                        break;
                        case 3:
                        target = new Vector2(-13, 21);
                        transform.position = target;
                        break;
                    }
                }
                GameObject Laser = objectManager.MakeObj("LaserPivot");
                Laser.transform.position = transform.position;
                Vector3 playerdir = player.transform.position - transform.position;
                float angle = Mathf.Atan2(playerdir.y, playerdir.x) * 180 / Mathf.PI;
                if (angle < 0) angle += 360;
                Laser.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                SoundManager.GetInstance().Play("Sound/BossSound/LaserSound", 0.1f);
                yield return new WaitForSeconds(1);
            }
            StartCoroutine("Boss_Scw");
    }

    //패턴8 : 랜덤방향으로 탄막 여러개 발사
    private IEnumerator Pattern_8()
    {
        StartCoroutine("Boss_Set");
        if (!Phase1)
        {
            StartCoroutine("playerTracking");
        }
        for (int i = 0; i < 30; i++)
        {
            GameObject Barrage = objectManager.MakeObj("Barrage");
            Barrage.transform.position = transform.position;
            BarrageSpeed = 15f;
            Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
            Vector2 playerdir = player.transform.position - transform.position;
            Vector2 angledir = new Vector2(Random.Range(-4f, 4f), 0);
            playerdir += angledir;
            rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        if (!Phase1)
        {
            StopCoroutine("playerTracking");
            rigid.velocity = Vector2.zero;
            StartCoroutine("Boss_Scw");
            yield break;
        }
        Invoke("PatternManager", 2f);
    }

    //패턴9 : 지그재그방향으로 탄막 여러개 발사
    private IEnumerator Pattern_9()
    {
        StartCoroutine("Boss_Set");
        if (Phase2)
        {
            StartCoroutine("playerTracking");
        }
        for (int i = 0; i < 50; i++)
        {
            GameObject Barrage = objectManager.MakeObj("Barrage");
            Barrage.transform.position = transform.position;
            BarrageSpeed = 15f;
            Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
            Vector2 playerdir = player.transform.position - transform.position;
            Vector2 angledir = new Vector2(Mathf.Sin( Mathf.PI * 25 *  i / 50) * 2f, 0);
            playerdir += angledir;
            rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        if (Phase2)
        {
            StopCoroutine("playerTracking");
            rigid.velocity = Vector2.zero;
            StartCoroutine("Boss_Scw");
            yield break;
        }
        Invoke("PatternManager", 2f);
    }

    //패턴10 : 큰 탄막 터지면 작은탄막
    private IEnumerator Pattern_10()
    {
        StartCoroutine("Boss_Set");
        if (Phase3)
        {
            StartCoroutine("playerTracking");
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject BigBarrage = objectManager.MakeObj("BigBarrage");
            BigBarrage bigBarrageLogic = BigBarrage.GetComponent<BigBarrage>();
            bigBarrageLogic.objectManager = objectManager;
            BigBarrage.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            BarrageSpeed = 10f;
            Rigidbody2D rigid = BigBarrage.GetComponent<Rigidbody2D>();
            Vector2 playerdir = player.transform.position - transform.position;
            rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f,Define.Sound.Effect, 0.5f);
            yield return new WaitForSeconds(2f);
        }
        if (Phase3)
        {
            StopCoroutine("playerTracking");
            rigid.velocity = Vector2.zero;
            StartCoroutine("Boss_Scw");
            yield break;
        }
        StartCoroutine("Boss_Scw");
    }

    //패턴11 : 탄막 원형으로 생기고 시간차공격
    public IEnumerator Pattern_11()
    {
        StartCoroutine("Boss_Set");
        if (Phase2 || Phase3)
        {
            transform.position = new Vector2(0, 26);
        }   
        int RoundNum = 12;
        Barrage[] barrageLogic = new Barrage[RoundNum];
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < RoundNum; i++)
            {
                GameObject Barrage = objectManager.MakeObj("Barrage");
                barrageLogic[i] = Barrage.GetComponent<Barrage>();
                barrageLogic[i].breakableLayer = 14;
                barrageLogic[i].player = player;
                Vector2 Rounddir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / RoundNum) * 6, Mathf.Sin(Mathf.PI * 2 * i / RoundNum) * 6);
                Barrage.transform.position = new Vector2(player.transform.position.x + Rounddir.x, player.transform.position.y + Rounddir.y);
            }
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.05f, Define.Sound.Effect, 0.5f);
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < RoundNum; i++)
            {
                barrageLogic[i].time *= i;
                barrageLogic[i].StartCoroutine("TimeDifference");
            }
            yield return new WaitForSeconds(3.5f);
        }
        if (Phase2 || Phase3)
        {
            StartCoroutine("Boss_Scw");
            yield break;
        }
        Invoke("PatternManager", 2f);
    }
        
    //패턴12 : 장판생기고 탄막난사
    public IEnumerator Pattern_12()
    {
        StartCoroutine("Boss_Set");
        yield return new WaitForSeconds(0.3f);
        transform.position = new Vector2(0, 30);
        for (int j = 0; j < 2; j++)
        {
            GameObject AOE = objectManager.MakeObj("AOE");
            AOE.transform.localScale = new Vector3(5.5f, 5.5f, 5.5f);
            switch(j)
            {
                case 0:
                    AOE.transform.position = new Vector2(12.7f, 30f);
                    break;
                case 1:
                    AOE.transform.position = new Vector2(-4.1f, 30f);
                    break;
            }
        }
        for (int k = 0; k < 6; k++)
        {
            GameObject AOE = objectManager.MakeObj("AOE");
            AOE.transform.localScale = new Vector3(4.6f, 4.6f, 4.6f);
            switch (k)
            {
                case 0:
                    AOE.transform.position = new Vector2(-12.4f, 30.3f);
                    break;
                case 1:
                    AOE.transform.position = new Vector2(10.1f, 23.8f);
                    break;
                case 2:
                    AOE.transform.position = new Vector2(5.1f, 20.5f);
                    break;
                case 3:
                    AOE.transform.position = new Vector2(-9.4f, 25f);
                    break;
                case 4:
                    AOE.transform.position = new Vector2(6f, 28.6f);
                    break;
                case 5:
                    AOE.transform.position = new Vector2(-0.8f, 24.3f);
                    break;
            }
        }
        for (int l = 0; l < 3; l++)
        {
            GameObject AOE = objectManager.MakeObj("AOE");
            AOE.transform.localScale = new Vector3(3.8f, 3.8f, 3.8f);
            switch (l)
            {
                case 0:
                    AOE.transform.position = new Vector2(-5.1f, 20.6f);
                    break;
                case 1:
                    AOE.transform.position = new Vector2(13.3f, 20.1f);
                    break;
                case 2:
                    AOE.transform.position = new Vector2(-13.9f, 20.6f);
                    break;
            }
        }
        for (int i = 0; i < 15; i++)
        {
            GameObject Barrage = objectManager.MakeObj("Barrage");
            Barrage.transform.position = transform.position;
            BarrageSpeed = 60;
            Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
            Vector2 playerdir = player.transform.position - transform.position;
            rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
            yield return new WaitForSeconds(0.3f);
        }
        AOE[] aoe = FindObjectsOfType<AOE>();
        for(int o = 0; o < 11; o++)
        {
            aoe[o].delete();
        }
        yield return new WaitForSeconds(0);
        StartCoroutine("Boss_Scw");
    }

    private IEnumerator Pattern_13()
    {
        StartCoroutine("Boss_CristalSet");
        yield return new WaitForSeconds(0.3f);
        Cristal[] cristalLogic = new Cristal[4];
        for (int i = 0; i < 2; i++)
        {
            if (cristal[i] == null && cristal[i + 2] == null)
            {
                Debug.Log("둘다없음");
                yield return new WaitForSeconds(1f);
                continue;
            }
            if (cristal[i] == null)
            {
                Debug.Log("첫번째거 없음");
                cristalLogic[i + 2] = cristal[i + 2].GetComponentInChildren<Cristal>();
                cristalLogic[i + 2].StartCoroutine("Pattern_13");
                SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.2f);
                yield return new WaitForSeconds(1f);
                continue;
            }
            if (cristal[i + 2] == null)
            {
                Debug.Log("두번째거 없음");
                cristalLogic[i] = cristal[i].GetComponentInChildren<Cristal>();
                cristalLogic[i].StartCoroutine("Pattern_13");
                SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.2f);
                yield return new WaitForSeconds(1f);
                continue;
            }

            cristalLogic[i] = cristal[i].GetComponentInChildren<Cristal>();
            cristalLogic[i].StartCoroutine("Pattern_13");
            cristalLogic[i + 2] = cristal[i + 2].GetComponentInChildren<Cristal>();
            cristalLogic[i + 2].StartCoroutine("Pattern_13");
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.2f);
            yield return new WaitForSeconds(1f);
        }
        Invoke("PatternManager", 2f);
    }

    private IEnumerator Pattern_14()
    {
        StartCoroutine("Boss_CristalSet");
        yield return new WaitForSeconds(0.3f);
        Cristal[] cristalLogic = new Cristal[4];
        for (int i = 0; i < 4; i++)
        {
            if (cristal[i] == null)
            {
                continue;
            }
            cristalLogic[i] = cristal[i].GetComponentInChildren<Cristal>();
            cristalLogic[i].StartCoroutine("Pattern_14");
            yield return new WaitForSeconds(1f);
        }
        Invoke("PatternManager", 2f);
    }

    private IEnumerator playerTracking()
    {
        while(true)
        {
            Vector2 playerdir = player.transform.position - transform.position;
            rigid.velocity = new Vector2(playerdir.normalized.x * BossSpeed, playerdir.normalized.y * BossSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator HpRaise()
    {
        for(int i = 1; i < 1000; i++)
        {
            BossHp = i;
            yield return new WaitForSeconds(0.0007f);
        }
    }

    public void OnVanish()
    {
        BossVanished = false;
    }
    //#.피해 입기


    public void damaged(int damage)
    {
        BossHp -= damage;
        Instantiate(damagedEffect, transform.position + new Vector3(Random.Range(0, 1f) - 0.5f, Random.Range(0, 1f) - 0.5f), transform.rotation);

    }
    public void damaged(float damage)
    {
        BossHp -= damage;
        Instantiate(damagedEffect, transform.position + new Vector3(Random.Range(0, 1f) - 0.5f, Random.Range(0, 1f) - 0.5f), transform.rotation);

    }
    public void damaged(float damage, string attackType)
    {
        BossHp -= damage;
        Instantiate(damagedEffect, transform.position + new Vector3(Random.Range(0, 1f) - 0.5f, Random.Range(0, 1f) - 0.5f), transform.rotation);


        if (attackType == "sword")
        {
            wasHit = true;
        }
    }
    //#.죽기
}
