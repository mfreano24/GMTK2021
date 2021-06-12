using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts;
    int healthRemaining = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boulder"))
        {
            Debug.Log("Smacked");
            hearts[--healthRemaining].SetActive(false);
            if (healthRemaining == 0)
            {
                Debug.Log("you died lol");
                GameManager.Instance.PlayerDeath();
            }
        }
    }


}
