using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public AudioSource victorySound;
    public AudioSource levelMusic;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Menu);
        victorySound.Play();
        levelMusic.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Menu();
        }
    }

    void Menu()
    {
        // Add code here;
    }
}
