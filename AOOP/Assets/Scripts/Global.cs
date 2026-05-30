using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour


{

    public float location;
    public bool gotsword;

    public bool colorStrike;
    public bool onestroke;
    public Camera camera1;  // Reference to Camera 1
    public Camera camera2;  // Reference to Camera 2

    public GameObject player;


    public GameObject shadowplayer;

    public GameObject position1;


    public GameObject position2;

    public GameObject position3;


    public GameObject skybg;

    public GameObject darkbg;



    // Start is called before the first frame update
    void awake()
    {

        camera1.enabled = true;
        camera2.enabled = false;

        player.gameObject.SetActive(true);
        shadowplayer.gameObject.SetActive(false);
        skybg.gameObject.SetActive(true);
        darkbg.gameObject.SetActive(false);
        gotsword = false;
        location = 2;
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) // Change KeyCode to your preferred key
        {
            SwitchPlayer();
        }

    }
    private void SwitchCamera()
    {
        camera1.enabled = !camera1.enabled;  // Toggle Camera 1
        camera2.enabled = !camera2.enabled;  // Toggle Camera 2
    }
    private void SwitchPlayer()
    {   
        player.gameObject.SetActive(false);
        shadowplayer.gameObject.SetActive(true);
        skybg.gameObject.SetActive(false);
        darkbg.gameObject.SetActive(true); // Toggle Camera 2
    }
}

