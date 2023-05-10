using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public WizardMovement wizardMovement;
    // Start is called before the first frame update
    void Start()
    {
        wizardMovement = FindObjectOfType<WizardMovement>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log(other.name);
            if(other.name == "Player" || other.name.StartsWith("Player (") && other.name.EndsWith(")")){
                other.GetComponent<PlayerMovement>().TakeDamage(wizardMovement.damage);
            }
            Destroy(gameObject);
        }
    }
}
