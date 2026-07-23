using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_MeatPickup : MonoBehaviour
{
	[SerializeField] AudioClip meatPickupSFX;
	[SerializeField] int meatValue = 3;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AudioSource.PlayClipAtPoint(meatPickupSFX, Camera.main.transform.position);
		FindObjectOfType<DS_GameSession>().AddToHealth(meatValue);
		Destroy(gameObject);
	}
}
