using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    public int Damage = 10;
    public int breakableLayer = 8;
    public Player player;
    public float time = 0.3f;

    void OnEnable()
    {
        time = 0.3f;
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


    public IEnumerator TimeDiffernce()
    {
        yield return new WaitForSeconds(time);
        float Speed = 15f;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        Vector2 playerdir = player.transform.position - transform.position;
        Vector2 Forcedir = new Vector2(playerdir.normalized.x, playerdir.normalized.y);
        rigid.AddForce(new Vector2(Forcedir.x, Forcedir.y) * Speed, ForceMode2D.Impulse);
    }
}
