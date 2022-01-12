using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int Damage = 20;
    public bool isFired = false;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (!isFired)
        {
            StartCoroutine("Warning");
        }
    }

    public IEnumerator Fire()
    {
        gameObject.gameObject.layer = 11;
        isFired = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        isFired = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int Layer = collision.gameObject.layer;
        if (Layer == 10 && isFired)
        {
            collision.GetComponent<Player>().damaged(Damage);
        }
    }

    public IEnumerator Warning()
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("LaserOn");
        StartCoroutine("Fire");
    }
}
