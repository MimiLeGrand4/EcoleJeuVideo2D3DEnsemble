using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaFPS : MonoBehaviour
{
    public Canvas canvas; // Référence au GameObject Canvas

    // Start est appelée avant le premier frame
    void Start()
    {
        // Active le Canvas au démarrage
        canvas.enabled = true;
    }

    // Update est appelée une fois par frame
    void Update()
    {
        // Vérifie si la touche "C" est enfoncée
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Active ou désactive le Canvas en fonction de son état actuel
            canvas.enabled = !canvas.enabled;
        }
    }
}
