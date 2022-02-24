using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    public int Damage = 10;
    public int breakableLayer = 8;
    public Player player;
    public float time = 0.2f;
    public static readonly WaitForSeconds waitForSecond = new WaitForSeconds(1f);

    void OnEnable()
    {
        time = 0.2f;
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


    public IEnumerator TimeDifference()
    {
        yield return new WaitForSeconds(time);
        float Speed = 20f;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        Vector2 playerdir = player.transform.position - transform.position;
        Vector2 Forcedir = new Vector2(playerdir.normalized.x, playerdir.normalized.y);
        rigid.AddForce(new Vector2(Forcedir.x, Forcedir.y) * Speed, ForceMode2D.Impulse);
        SoundManager.GetInstance().Play("Sound/BossSound/BarrageSound", 0.1f);
    }

    public IEnumerator TimeDifferencePattern_14()
    {
        yield return waitForSecond;
        int Speed = 14;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        Vector2 Forcedir = player.transform.position - transform.position;
        rigid.velocity = Vector3.zero;
        rigid.AddForce(new Vector2(Forcedir.normalized.x, Forcedir.normalized.y) * Speed, ForceMode2D.Impulse);
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }
}
