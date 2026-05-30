using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Atk_sword"))
        {
            Debug.Log("enemy detected");
            anim.SetTrigger("death");
        }
    }

    // This method will be called by the animation event
    public void die()
    {

        Debug.Log("enemy death");
        Destroy(gameObject);
    }
}
