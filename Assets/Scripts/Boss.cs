using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float BossHp = 700;
    public float PatternTime = 4f;
    public int PatternNum = 0;
    public int PrePatternNum = 0;
    public float BarrageSpeed;
    public Player player;
    public ObjectManager objectManager;
    
    void Start()
    {
        PatternManager();
    }

    private void PatternManager()
    {
        do
        {
            PatternNum = Random.Range(1, 5);
        } while (PatternNum == PrePatternNum);

        PrePatternNum = PatternNum;

        switch (PatternNum)
        {
            case 1:
                StartCoroutine(Pattern_9());
                break;
            case 2:
                StartCoroutine(Pattern_9());
                break;
            case 3:
                StartCoroutine(Pattern_9());
                break;
            case 4:
                StartCoroutine(Pattern_9());
                break;
            default:
                break;
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
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine("Boss_Scw");
    }

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
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("Boss_Scw");
    }

    private IEnumerator Pattern_3()
    {
        for (int j = 0; j <= 1; j++)
        {
            for (int i = 1; i <= 3; i++)
            {
                GameObject Laser = objectManager.MakeObj("Laser");
                Laser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                Laser.transform.localScale = new Vector3(5.25f, 16.2f, 1);
                Laser.transform.position = new Vector2(0, 12f + (8 * i));
            }
            yield return new WaitForSeconds(1f);
            for (int i = 1; i <= 2; i++)
            {
                GameObject Laser = objectManager.MakeObj("Laser");
                Laser.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                Laser.transform.localScale = new Vector3(5.25f, 16.2f, 1);
                Laser.transform.position = new Vector2(0, 16f + (8 * i));
            }
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("Boss_Scw");
    }
    
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
            yield return new WaitForSeconds(0.3f);
        }
        StartCoroutine("Boss_Scw");
    }

    public IEnumerator Pattern_6()
    {
        int RoundNum = 8;
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < RoundNum; i++)
            {
                BarrageSpeed = 10;
                GameObject Barrage = objectManager.MakeObj("Barrage");
                Barrage barrageLogic = Barrage.GetComponent<Barrage>();
                barrageLogic.breakableLayer = 14;
                Vector2 Rounddir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / RoundNum) * 10, Mathf.Sin(Mathf.PI * 2 * i / RoundNum) * 10);
                Barrage.transform.position = new Vector2(player.transform.position.x + Rounddir.x, player.transform.position.y + Rounddir.y);
                Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
                Vector2 playerdir = player.transform.position - Barrage.transform.position;
                Vector2 Forcedir = new Vector2(playerdir.normalized.x, playerdir.normalized.y);
                rigid.AddForce(new Vector2(Forcedir.x, Forcedir.y) * BarrageSpeed, ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(3);
        }
        StartCoroutine("Boss_Scw");
    }

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
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("Boss_Scw");
    }

    private IEnumerator Pattern_9()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject Barrage = objectManager.MakeObj("Barrage");
            Barrage.transform.position = transform.position;
            BarrageSpeed = 15f;
            Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
            Vector2 playerdir = player.transform.position - transform.position; 
            Vector2 angledir = new Vector2(Mathf.Sin(Mathf.PI * 48 * i / 50) * 2f, 0);
            playerdir += angledir;
            rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine("Boss_Scw");
    }

    private IEnumerator Pattern_10()
    {
        yield return new WaitForSeconds(0.1f);
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
    //#.죽기
    void dead()
    {
        if(BossHp <= 0)
            GetComponent<SpriteRenderer>().color = Color.red;
    }
}
