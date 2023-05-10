using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {NoType, Coin, Life, Key}; // Le type de gameObject affecté au objet exemple Coin sera le comportement pour un GameObject Coin

	public CollectibleTypes CollectibleType; // GameObject Type

	public bool rotate;
	private PlayerMovement playerMovement;
	public float rotationSpeed;
	public AudioClip collectSound;

	public GameObject collectEffect;

	// Use this for initialization
	void Start () {
		playerMovement = FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
	}

	public void Collect()
	{
		if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		//Below is space to add in your code for what happens based on the collectible type

		if (CollectibleType == CollectibleTypes.NoType) {

			Debug.Log ("Aucun Type");
		}

		//Comportement Coin
		if (CollectibleType == CollectibleTypes.Coin) {
			Debug.Log ("Le joueur a rammser un Coin");
		}

		//Comportement Vie
		if (CollectibleType == CollectibleTypes.Life) {
			playerMovement.health += 2;
			Debug.Log ("Le joueur a rammser un Coeur");
		}

		//Comportement Key
		if (CollectibleType == CollectibleTypes.Key) {
			foreach(GameObject door in GameObject.FindGameObjectsWithTag("DoorWicket"))
			{
				door.SetActive(false);
			}
			Debug.Log ("Le joueur a rammser une clé");
		}

		Destroy (gameObject);
	}
}
