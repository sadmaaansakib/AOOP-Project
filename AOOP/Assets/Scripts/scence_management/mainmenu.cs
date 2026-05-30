using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject story;
    private void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            // Exit story menu if it is active
            if (story.activeSelf)
            {
                story.SetActive(false);
            }

            // Exit credits menu if it is active
            if (credits.activeSelf)
            {
                credits.SetActive(false);
            }
        }
    }

    public void Play()
    {
        //SceneManager.LoadScene(1);
        //LevelManager.Instance.LoadScene("SampleScence","CrossFade");

        Loader.Load(Loader.Scence.Starting);
    }

    public void Quit()
    {
        // Quit the application
        Application.Quit();

//        // If running in the Editor, stop play mode
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
    }

    public void Credits()
    {

        credits.SetActive(true);

    }
    public void Story()
    {
        story.SetActive(true);


    }
}