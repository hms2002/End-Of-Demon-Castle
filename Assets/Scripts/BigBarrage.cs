using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBarrage : MonoBehaviour
{
    public int Damage = 20;
    public int breakableLayer = 8;
    int smallBarrageSpeed = 10;
    public ObjectManager objectManager;
    void OnEnable()
    {
        breakableLayer = 8;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        int Layer = collision.gameObject.layer;
        if(Layer == 10)
        {
            collision.GetComponent<Player>().damaged(Damage);
            gameObject.SetActive(false);
        }
        if(Layer == breakableLayer)
        {
            int RoundNum = 10;
            for(int i = 0; i < RoundNum; i++)
            {
                GameObject Barrage = objectManager.MakeObj("Barrage");
                Vector2 Rounddir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / RoundNum), Mathf.Sin(Mathf.PI * 2 * i / RoundNum));
                Barrage.transform.position = transform.position;
                Rigidbody2D rigid = Barrage.GetComponent<Rigidbody2D>();
                rigid.AddForce(new Vector2(Rounddir.x, Rounddir.y) * smallBarrageSpeed, ForceMode2D.Impulse);
            }
            SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {   
        if (collision.gameObject.layer == 15)
        {
            breakableLayer = 8;
        }
    }

    public void TimeDiffernce()
    {
        
    }
}
