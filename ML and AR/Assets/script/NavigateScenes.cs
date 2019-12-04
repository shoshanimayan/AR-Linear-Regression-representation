using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateScenes : MonoBehaviour
{

    public void LoadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "menu")
        {
            SceneManager.LoadScene("menu");
        }
        else
        {
            SceneManager.LoadScene("graph");
        }

    }
}