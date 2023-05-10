using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject ui;
    public GameObject pauseMenu;

    void Start()
    {
        
    }

    public void stopGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }

    void Update()
    {
        
    }

    public void pauseFrames(bool active)
    {
        if (active)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
