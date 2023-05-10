using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSword : MonoBehaviour
{
    // Variable Mouvement des Personnages
    public PlayerMovement playerMovement;
    public AudioClip swordSound1;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy" && playerMovement.isAttacking){
            Debug.Log(other.name);

            if(swordSound1)
			    AudioSource.PlayClipAtPoint(swordSound1, transform.position);

            if(other.name == "Goblin" || other.name.StartsWith("Goblin (") && other.name.EndsWith(")")){
                other.GetComponent<GoblinMovement>().TakeDamage(playerMovement.damage);
            }

            if(other.name == "Wizard" || other.name.StartsWith("Wizard (") && other.name.EndsWith(")")){
                other.GetComponent<WizardMovement>().TakeDamage(playerMovement.damage);
            }

            if(other.name == "Golem" || other.name.StartsWith("Golem (") && other.name.EndsWith(")")){
                other.GetComponent<GoblinMovement>().TakeDamage(playerMovement.damage);
            }
        }
    }
}
