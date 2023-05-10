using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public Canvas victoryCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Pause the game
            Time.timeScale = 0f;

            // Show the exit canvas
            victoryCanvas.gameObject.SetActive(true);
        }
    }
}
