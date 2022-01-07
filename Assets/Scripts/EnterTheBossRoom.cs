using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheBossRoom : MonoBehaviour
{
    public GameObject TextBox;
    public Transform destination;
    Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player reached the door");
        player = other.GetComponent<Player>();

        TextBox.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player move away from the door");

        TextBox.SetActive(false);
    }

    public void GoToPos()
    {
        player.gameObject.transform.position = destination.position;
    }
}
