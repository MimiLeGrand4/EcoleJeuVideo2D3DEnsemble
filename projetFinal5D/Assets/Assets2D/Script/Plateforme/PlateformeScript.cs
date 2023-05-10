using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeScript : MonoBehaviour
{
    public float vitesse = 2.0f; // vitesse of the platform
    public float distance = 5.0f; // distance to move
    public float pause = 2.0f; // time to pause at top/bottom of mouvement
    private float compteurPause = 0.0f; // time since last pause
    public bool monte = true; // whether to start at the top or bottom of mouvement
    private bool monteEnHaut = true; // whether platform is moving up or down
    private Vector3 position; // starting position of platform
    private float distanceActuel = 0.0f; // current distance moved
    
    

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        if (!monte) {
            monteEnHaut = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie le temps du déplacement
        if (compteurPause < pause) {
            compteurPause += Time.deltaTime;
            return;
        }

        // Temps du mouvement
        float mouvement = vitesse * Time.deltaTime;

        // Vérifie si la pateforme monte
        if (monteEnHaut) {
            transform.position += Vector3.up * mouvement;
        } else {
            transform.position += Vector3.down * mouvement;
        }

        distanceActuel += mouvement;

        //Vérifie si la distance est égale ou suppérieur à la distance initial dans ce cas tout est réinitialisé
        if (distanceActuel >= distance) {
            distanceActuel = 0.0f;
            monteEnHaut = !monteEnHaut;
            compteurPause = 0.0f;
        }
    }
}
