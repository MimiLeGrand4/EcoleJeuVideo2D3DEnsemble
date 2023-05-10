using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(Disappear());
            audioSource.PlayOneShot(audioClip);
        }
    }

    IEnumerator Disappear() {
    // Supprimer le sprite de la pièce
    GetComponent<SpriteRenderer>().enabled = false;
    // Supprimer le collider de la pièce
    GetComponent<BoxCollider2D>().enabled = false;
    // Attendre 0.5 seconde pour laisser le temps au joueur de récupperer la pièce
    yield return new WaitForSeconds(0.5f);
    // Détruire le gameobject de la pièce
    Destroy(gameObject);
    }


}
