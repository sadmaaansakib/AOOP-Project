using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Healt : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    [SerializeField] private AudioClip hurtsound;
    [SerializeField] private AudioClip diesound;

    public float currentHealth {get; private set; }

    private Animator anim;

    private bool death;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim=GetComponent<Animator>();
    }

    public void Taka_damage(float _damage)
    {

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
          anim.SetTrigger("hurt");
          //SoundManager.instance.PlaySound(hurtsound);
        }
        else
        {  
            if(!death){
            anim.SetTrigger("die");
            //SoundManager.instance.PlaySound(diesound);
            GetComponent<PlayerMovement>().enabled=false;
            death=true;
            }
        }
    }

    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth +_value, 0, startingHealth);

    }

    public void Respawn(){

        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        GetComponent<PlayerMovement>().enabled=true;
            death=false;

    }



    }

