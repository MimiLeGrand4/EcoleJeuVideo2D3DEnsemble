using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    private int coin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] listCoins = GameObject.FindGameObjectsWithTag("Coin");

        if(listCoins != null){
            coin = listCoins.Length;
        }
        
        if(coin <= 0){
            //Test sur la console
            Debug.Log("La porte est ouverte");

            // Supprimer le sprite
            GameObject tilemapObject = GameObject.Find("Brick");
            TilemapRenderer tilemapRenderer = tilemapObject.GetComponent<TilemapRenderer>();
            tilemapRenderer.enabled = true;
            // Supprimer le collider de la pièce
            GetComponent<BoxCollider2D>().enabled = false;
            // Détruire la porte de la pièce
            Destroy(gameObject);
        }
    }
}
