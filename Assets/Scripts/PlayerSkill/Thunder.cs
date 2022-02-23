using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    Boss boss;
    BreakableObj breakableObj;

    BoxCollider2D boxCollider2D;
    GameObject buffICON;

    float damage;

    float cur;
    private GameObject tempICON;

    private void Start()
    {
        
        damage = 3;
        cur = 0.5f;

        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/Thunder", 0.3f, Define.Sound.Effect, 0.7f);

        boxCollider2D = GetComponent<BoxCollider2D>();

        StartCoroutine(Damage());

        Destroy(gameObject, 10);

        //#.스킬 아이콘 및 강제 초기화
        buffICON = Resources.Load<GameObject>("Prefabs/Buff/Buff_Thunder");
        SkillSelectManager.GetInstance().Init += BeforeDEL;
        
        tempICON = Instantiate(buffICON, BuffLayoutSetting.GetInstance().transform);
        BuffLayoutSetting.GetInstance().AddBuff();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == 12)
        {
            if (boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(damage + DamageControler.GetInstance().GetDotDamage());
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj(damage + DamageControler.GetInstance().GetDotDamage());
        }
    }

    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    IEnumerator Damage()
    {
        while(true)
        {
            boxCollider2D.enabled = true;
            yield return new WaitForSeconds(0.1f);
            boxCollider2D.enabled = false;
            yield return new WaitForSeconds(0.45f);
        }
    }

    public void BeforeDEL()
    {
        if(gameObject != null)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SkillSelectManager.GetInstance().Init -= BeforeDEL;
        Destroy(tempICON);
        BuffLayoutSetting.GetInstance().SubBuff();

    }

}
