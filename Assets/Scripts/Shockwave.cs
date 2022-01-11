using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public Animator anim;
    public int Damage = 10;
    public bool isCharged = true;
    public bool isShocked = false;
    Player player;
    Vector3 ZeroScale = new Vector3(0,0,0);
    Vector3 Scaled = new Vector3(1, 1, 1);

    void Start()
    {
        if (isCharged)
        {
            StartCoroutine(Charge());
        }
    }
    void Update()
    {
        if (transform.localScale == ZeroScale)
        {
            anim.SetTrigger("Charged");
            StartCoroutine(Shock());
            isShocked = true;
        }
        if (transform.localScale == Scaled)
        {
            StopCoroutine(Shock());
        }
    }
    
    IEnumerator Charge()
    {
        for(float per = 1f; per >= 0f; per -= 0.01f)
        {
            transform.localScale = new Vector3(per, per, per);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator Shock()
    {
        for (float per = 0f; per <= 1f; per += 0.01f)
        {
            transform.localScale = new Vector3(per, per, per);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ㅋㅋ");
        if(collision.gameObject.CompareTag("Player") && isShocked)
        {
            Debug.Log("zz");
            Player player = gameObject.GetComponent<Player>();
            Boss boss = gameObject.GetComponent<Boss>();
            Rigidbody2D playerRig = gameObject.GetComponent<Rigidbody2D>();
            player.damaged(Damage);
            Vector2 playerdir = boss.transform.position - player.transform.position;

            playerRig.AddForce(new Vector2(playerdir.normalized.x,playerdir.normalized.y)*100,ForceMode2D.Impulse); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ㅋㅋ");
        if (collision.gameObject.CompareTag("Player") && isShocked)
        {
            Debug.Log("zz");
            player = collision.gameObject.GetComponent<Player>();
            Rigidbody2D playerRig = collision.gameObject.GetComponent<Rigidbody2D>();
            player.damaged(Damage);
            Vector2 playerdir = player.transform.position - transform.position;

            player.playerConfine();
            playerRig.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * 30, ForceMode2D.Impulse);

            Invoke("InbokePlayerFree", 0.15f);
        }
    }

    private void InbokePlayerFree()
    {
        player.playerFree();
    }
}
