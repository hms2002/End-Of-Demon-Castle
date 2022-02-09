using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cristal : BreakableObj
{
    Slider HpSlider;
    public ObjectManager objectManager;
    public Player player;
    public Vector3 playerdir;
    public static readonly WaitForSeconds waitForSecond = new WaitForSeconds(0.1f);
    int BarrageNum = 5;

    private void Awake()
    {
        HpSlider = transform.parent.GetChild(1).GetChild(0).GetComponent<Slider>();
        anim = GetComponent<Animator>();

        Hp = 350;
        HpSlider.maxValue = Hp;
    }

    public override void breakObj(float damage)
    {
        Hp -= damage;
        if (Hp > 87) { }
        else if (Hp > 0)
        {
            anim.SetTrigger("Break");
        }
        else
        {
            Hp = 0;
            Boss.GetInstance().CristalNum -= 1;
            Destroy(transform.parent.gameObject);
        }

        HpSlider.value = Hp;
    }

    public IEnumerator Pattern_13()
    {
        for (int i = 0; i < 25; i++)
        {
            float BarrageSpeed = Random.Range(8f, 15f);
            GameObject Barrage = objectManager.MakeObj("Barrage");
            Barrage.transform.position = transform.position;
            Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
            Vector2 playerdir = player.transform.position - transform.position;
            Vector2 angledir = new Vector2(Random.Range(-2.7f, 2.7f), Random.Range(-2f, 2f));
            playerdir += angledir;
            rigid.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.02f);
        }
    }

    public IEnumerator Pattern_14()
    {
        Barrage[] barrageLogic = new Barrage[BarrageNum];
        int BarrageSpeed = 14;
        playerdir = player.transform.position;
        Vector2 forcedir = playerdir - transform.position;
        for (int i = 0; i < BarrageNum; i++)
        {
            GameObject Barrage = objectManager.MakeObj("Barrage");
            barrageLogic[i] = Barrage.GetComponent<Barrage>();
            Barrage.transform.position = transform.position;
            Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(forcedir.normalized.x, forcedir.normalized.y) * BarrageSpeed, ForceMode2D.Impulse);
            barrageLogic[i].StartCoroutine("TimeDifferencePattern_14");
            yield return new WaitForSeconds(0.1f);
        }
    }
}

