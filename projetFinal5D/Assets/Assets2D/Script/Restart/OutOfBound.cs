using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBound : MonoBehaviour
{
    public GameObject restartPanel;
    public Camera camera1;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if (camera1.transform.position.y > player.transform.position.y + 8)
        {
            restartPanel.SetActive(true);
        }
    }
}
