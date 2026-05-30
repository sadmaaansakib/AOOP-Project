using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip chkpntsnd;
    private Transform currentchkpnt;

    private Healt playerhealth;

    private void Awake(){
        playerhealth=GetComponent<Healt>();
    }

    public void Respawn(){
        transform.position=currentchkpnt.position;
        playerhealth.Respawn();
    }


    
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.transform.tag=="CheckPoint"){

            currentchkpnt=other.transform;
            other.GetComponent<Collider2D>().enabled=false;
            other.GetComponent<Animator>().SetTrigger("check");
        }
    }
 



}
