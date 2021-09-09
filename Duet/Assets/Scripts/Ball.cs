using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    ParticleSystem explosionVfx;
    private void Start()
    {
        explosionVfx = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Box"))
        {
            GameManager.Instance.isGameOver = true;
            explosionVfx.Play();
            PlayerMovement.Instance.Restart();
        }
    }
   
}
