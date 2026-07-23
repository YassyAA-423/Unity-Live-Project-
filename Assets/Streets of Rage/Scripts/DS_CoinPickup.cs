using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS_CoinPickup : MonoBehaviour
{
	[SerializeField] AudioClip coinPickupSFX;
	[SerializeField] int coinValue = 50;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
		FindObjectOfType<DS_GameSession>().AddToScore(coinValue);
		Destroy(gameObject);
	}
}
