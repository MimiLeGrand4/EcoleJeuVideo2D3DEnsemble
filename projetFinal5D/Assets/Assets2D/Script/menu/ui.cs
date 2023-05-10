using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui : MonoBehaviour
{

    public GameObject uiMenu;
    public GameObject pauseMenu;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseFrames(true);
            pauseMenu.SetActive(true);
            uiMenu.SetActive(false);
        }
    }
}
