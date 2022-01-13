using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int Damage = 20;
    public bool isFired = false;
    Player player;
    Animator anim;
    BoxCollider2D boxCollider2D;
    Vector3 HorizonScale = new Vector3(5.25f, 16.2f, 1);

    void OnEnable()
    {
        anim = GetComponent<Animator>();
        
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        StartCoroutine("Warning");
    }

  public IEnumerator Warning()
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("LaserOn");
        StartCoroutine("Fire");
    }

    public IEnumerator Fire()
    {
        isFired = true;
        boxCollider2D.enabled = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int Layer = collision.gameObject.layer;
        if (Layer == 10 && isFired)
        {
            player = collision.GetComponent<Player>();
            player.damaged(Damage);
            Rigidbody2D playerRig = collision.gameObject.GetComponent<Rigidbody2D>();
            player.playerConfine();
            if (transform.localScale != HorizonScale)
            {
                playerRig.AddForce(new Vector2(0, -1f) * 30, ForceMode2D.Impulse);
            }
            else
            {
                playerRig.AddForce(new Vector2(-1f, 0) * 30, ForceMode2D.Impulse);
            }
            Invoke("InbokePlayerFree", 0.15f);
        }
    }

    private void InbokePlayerFree()
    {
        player.playerFree();
    }
}
