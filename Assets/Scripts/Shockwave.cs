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
    Vector3 ZeroScale = new Vector3(0f,0f,1);
    Vector3 Scaled = new Vector3(8f, 8f, 1);

    void OnEnable()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        if (isCharged)
        {
            anim = GetComponent<Animator>();
            isShocked = false;
            StartCoroutine(Charge());
        }
    }
    void FixedUpdate()
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
        for(float per = 8f; per >= 0f; per -= 8 * Time.deltaTime)
        {
            transform.localScale = new Vector3(per, per, 1);
            Debug.Log(transform.localScale);
            yield return new WaitForSeconds(0.001f);
        }
        transform.localScale = ZeroScale;
    }

    IEnumerator Shock()
    {
        for (float per = 0f; per <= 8f; per += 24f * Time.deltaTime)
        {
            transform.localScale = new Vector3(per, per, 1);

            yield return new WaitForSeconds(0.001f);
        }
        transform.localScale = Scaled;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isShocked)
        {
            player = collision.gameObject.GetComponent<Player>();
            Rigidbody2D playerRig = collision.gameObject.GetComponent<Rigidbody2D>();
            player.damaged(Damage);
            Vector2 playerdir = player.transform.position - transform.position;

            player.playerConfine();
            playerRig.AddForce(new Vector2(playerdir.normalized.x, playerdir.normalized.y) * 15, ForceMode2D.Impulse);
            GetComponent<CircleCollider2D>().enabled = false;
            Invoke("InbokePlayerFree", 0.15f);
        }
    }

    private void InbokePlayerFree()
    {
        player.playerFree();
    }
}
