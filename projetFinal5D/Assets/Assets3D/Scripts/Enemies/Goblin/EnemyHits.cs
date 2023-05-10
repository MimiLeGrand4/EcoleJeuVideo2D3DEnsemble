using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHits : MonoBehaviour
{
    public GoblinMovement goblinMovement;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && goblinMovement.isAttacking){
            Debug.Log(other.name);

            if(other.name == "Player" || other.name.StartsWith("Player (") && other.name.EndsWith(")")){
                other.GetComponent<PlayerMovement>().TakeDamage(goblinMovement.damage);
            }
        }
    }
}
