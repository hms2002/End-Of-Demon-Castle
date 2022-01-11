using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float BossHp = 700;
    public float PatternTime = 5f;
    public int PatternNum = 0;
    public int PrePatternNum = 0;
    public float BarrageSpeed = 30;
    public Player player;
    public GameObject BarrageObj;
    public ObjectManager objectManager;

    void Start()
    {
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
                StartCoroutine(Pattern_1());
                break;
            case 2:
                Pattern_2();
                break;
            default:
                break;
        }


        Invoke("PatternManager", PatternTime);
    }

    private IEnumerator Pattern_1()
    {
        for (int i = 0; i < 6; i++)
        {
            for (float angle = 0f; angle < 5.4; angle = angle + 1.8f)
            {
                GameObject Barrage = objectManager.MakeObj("Barrage");
                Barrage.transform.position = transform.position;
                Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
                Vector2 playerdir = player.transform.position - transform.position;
                Vector2 angledir = new Vector2(angle, angle / 2);
                playerdir += angledir;
                rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            }
            for (float angle = 0f; angle < 5.4; angle = angle + 1.8f)
            {
                GameObject Barrage = objectManager.MakeObj("Barrage");
                Barrage.transform.position = transform.position;
                Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
                Vector2 playerdir = player.transform.position - transform.position;
                Vector2 angledir = new Vector2(-angle, angle / 2);
                playerdir += angledir;
                rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void Pattern_2()
    {
        Debug.Log("패턴2");
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
