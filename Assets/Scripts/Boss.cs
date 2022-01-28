using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float BossHp = 700;
    public bool wasHit;
    public float PatternTime = 4f;
    public int PatternNum = 0;
    public int PrePatternNum = 0;
    public float BarrageSpeed;
    public Player player;
    public ObjectManager objectManager;
    public GameObject[] cristal = new GameObject[4];

    void Start()
    {
        wasHit = false;
        PatternManager();
    }

    private void PatternManager()
    {
        do
        {
            PatternNum = Random.Range(1, 3);
        } while (PatternNum == PrePatternNum);

        PrePatternNum = PatternNum;

        switch (PatternNum)
        {
            case 1:
                StartCoroutine("Pattern_13");
                break;
            case 2:
                StartCoroutine("Pattern_13");
                break;
            /*case 3:
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
            case 11:
                StartCoroutine("Pattern_12");
                break;*/
        }

    }

    private IEnumerator Boss_Scw()
    {
        yield return new WaitForSeconds(4);
        GameObject Shockwave = objectManager.MakeObj("Shockwave");
        Shockwave.transform.position = transform.position;
        while (Shockwave.activeSelf)
            yield return new WaitForSeconds(0.1f);
        Invoke("PatternManager", 1.5f);
    }

    //패턴1 : 부채꼴 모양 발사
    private IEnumerator Pattern_1()
    {
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
        StartCoroutine("Boss_Scw");
    }

    //패턴2 : 레이저 세로발사
    private IEnumerator Pattern_2()
    {
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
        for (int j = 0; j <= 1; j++)
        {
            for (int i = 1; i <= 3; i++)
            {
                GameObject Laser = objectManager.MakeObj("Laser");
                Laser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                Laser.transform.localScale = new Vector3(4f, 16.1f, 1);
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
                Laser.transform.localScale = new Vector3(4f, 16.1f, 1);
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
        StartCoroutine("Boss_Scw");
    }

    //패턴6 : 맵밖에서 원형으로 감싸는 탄막
    public IEnumerator Pattern_6()
    {
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
        StartCoroutine("Boss_Scw");
    }

    //패턴7 : 플레이어 방향으로 레이저
    private IEnumerator Pattern_7()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject Laser = objectManager.MakeObj("LaserPivot");
            Laser.transform.position = transform.position;
            Vector3 playerdir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(playerdir.y, playerdir.x) * 180 / Mathf.PI;
            if (angle < 0) angle += 360;
            Laser.transform.rotation = Quaternion.AngleAxis(angle + 90,Vector3.forward);
            SoundManager.GetInstance().Play("Sound/BossSound/LaserSound", 0.1f);
            yield return new WaitForSeconds(1);
        }
        StartCoroutine("Boss_Scw");
    }

    //패턴8 : 랜덤방향으로 탄막 여러개 발사
    private IEnumerator Pattern_8()
    {
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
        StartCoroutine("Boss_Scw");
    }

    //패턴9 : 지그재그방향으로 탄막 여러개 발사
    private IEnumerator Pattern_9()
    {
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
        StartCoroutine("Boss_Scw");
    }

    //패턴10 : 큰 탄막 터지면 작은탄막
    private IEnumerator Pattern_10()
    {
        for(int i = 0; i < 2; i++)
        {
            GameObject BigBarrage = objectManager.MakeObj("BigBarrage");
            BigBarrage bigBarrageLogic = BigBarrage.GetComponent<BigBarrage>();
            bigBarrageLogic.objectManager = objectManager;
            BigBarrage.transform.position = transform.position;
            BarrageSpeed = 10f;
            Rigidbody2D rigid = BigBarrage.GetComponent<Rigidbody2D>();
            Vector2 playerdir = player.transform.position - transform.position;
            rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f,Define.Sound.Effect, 0.5f);
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine("Boss_Scw");
    }

    //패턴11 : 탄막 원형으로 생기고 시간차공격
    public IEnumerator Pattern_11()
    {
        int RoundNum = 12;
        Barrage[] barrageLogic = new Barrage[RoundNum];
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < RoundNum; i++)
            {
                BarrageSpeed = 18;
                GameObject Barrage = objectManager.MakeObj("Barrage");
                barrageLogic[i] = Barrage.GetComponent<Barrage>();
                barrageLogic[i].player = player;
                Vector2 Rounddir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / RoundNum) * 8, Mathf.Sin(Mathf.PI * 2 * i / RoundNum) * 8);
                Barrage.transform.position = new Vector2(player.transform.position.x + Rounddir.x, player.transform.position.y + Rounddir.y);
            }
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.05f, Define.Sound.Effect, 0.5f);
            yield return new WaitForSeconds(1);
            for (int i = 0; i < RoundNum; i++)
            {
                barrageLogic[i].time *= i;
                barrageLogic[i].StartCoroutine("TimeDifference");
            }
                yield return new WaitForSeconds(4);
        }
        StartCoroutine("Boss_Scw");
    }

    //패턴12 : 장판생기고 탄막난사
    public IEnumerator Pattern_12()
    {
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
        Cristal[] cristalLogic = new Cristal[4];
        for (int i = 0; i < 4; i++)
        {
            cristalLogic[i] = cristal[i].GetComponentInChildren<Cristal>();
            cristalLogic[i].StartCoroutine("Pattern_13");
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("Boss_Scw");
    }
    //#.피해 입기
    public void damaged(int damage)
    {
        BossHp -= damage;
        dead();
    }
    public void damaged(float damage)
    {
        BossHp -= damage;
        dead();
    }
    public void damaged(float damage, string attackType)
    {
        BossHp -= damage;
        dead();

        if (attackType == "sword")
        {
            wasHit = true;
        }
    }
    //#.죽기
    void dead()
    {
        if(BossHp <= 0)
            GetComponent<SpriteRenderer>().color = Color.red;
    }

    IEnumerator zz()
    {
        SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
        yield return new WaitForSeconds(0.5f);
        SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 1f);
    }
}
