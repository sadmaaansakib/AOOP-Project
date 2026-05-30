using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthcollectable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float heathvalue;
     [SerializeField] private AudioClip lifegot;

   private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag=="Player"){


        //SoundManager.instance.PlaySound(lifegot);
        other.GetComponent<Healt>().AddHealth(heathvalue);

        Destroy(gameObject);
    }
   }
}
