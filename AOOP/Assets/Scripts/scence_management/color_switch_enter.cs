using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class ColorSwitchEnter : MonoBehaviour
{
    // This method is called when another collider enters the trigger

    [SerializeField] private GameObject transitionmenu;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("scenceChanged");
            
            transitionmenu.SetActive(true);
        }
    }

    public void shift(){


        Loader.Load(Loader.Scence.ColorSwitch);
    }
}
