using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 5f;
    public int damage = 2;

    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        //Bouge à droite
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x > startPos.x + distance)
            {
                movingRight = false;
            }
        }
        // Bouge à gauche
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (transform.position.x < startPos.x - distance)
            {
                movingRight = true;
            }
        }
    }

    //Si le joueur touche l'objet il perd des vies
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
    }
}
