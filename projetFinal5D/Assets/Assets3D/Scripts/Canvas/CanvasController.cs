using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
   
    
        public GameObject canvasObject;
        public KeyCode toggleKey = KeyCode.F1;

        private bool isCanvasActive = true;

        void Start()
        {
            canvasObject.SetActive(true);
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                ToggleCanvas();
            }
        }

        void ToggleCanvas()
        {
            isCanvasActive = !isCanvasActive;
            canvasObject.SetActive(isCanvasActive);

            if (isCanvasActive)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }
        }
}
