using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scence{
        Main, SampleScene, SampleScene2, SampleScene3, SampleScene4, Starting, ColorSwitch,OneStroke
    }
    public static void Load(Scence scence){

        SceneManager.LoadScene(scence.ToString());

}
}
