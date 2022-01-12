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
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int Layer = collision.gameObject.layer;
        if (Layer == 10 && isFired)
        {
            collision.GetComponent<Player>().damaged(Damage);
        }
    }
}
