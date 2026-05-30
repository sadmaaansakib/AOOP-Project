using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene4entry : MonoBehaviour
{

    public string leveltoload;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //[SerializeField] private GameObject transitionmenu;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("scenceChanged");
            SceneManager.LoadScene(leveltoload);
            //transitionmenu.SetActive(true);
        }
    }
    
}
