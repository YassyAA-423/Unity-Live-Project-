using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class DS_Enemy : MonoBehaviour
{
	[SerializeField] float enemyMoveSpeed = 1f;
	[SerializeField] float distanceBetween = 10; // Just off screen with 10 so they start charging at you off screen
	[SerializeField] float enemyHealth = 1f;
	[SerializeField] bool isBoss = false;
	[SerializeField] Vector2 knockBack = new Vector2(10f, 10f);

	public GameObject player;
	private float distance;
	Rigidbody2D enemyRigidBody;
	Animator enemyAnimator;
	
	void Start()
	{
		enemyRigidBody = GetComponent<Rigidbody2D>();
		enemyAnimator = GetComponent<Animator>();

		try
		{
			player = GameObject.Find("Player");
		}
		catch {  Debug.Log("Info Panel not found");  };
	}

	void Update()
	{
		EnemyMovement();
	}

	public float GetEnemyHealth()
	{
		return enemyHealth;
	}

	public void EnemyHit(float playerDamage)
	{
		enemyRigidBody.velocity = knockBack * new Vector2(-transform.localScale.x, 1f);
		enemyHealth = EnemyTakeDamage(playerDamage);

		if (enemyHealth < 1)
		{
			Dying();
		}
	}

	private float EnemyTakeDamage(float damage)
	{
		return enemyHealth -= damage;
	}

	public bool CheckEnemyType()
	{
		return isBoss;
	}

	private void EnemyMovement()
	{
		// Setting distance of the enemy from the player
		distance = Vector2.Distance(transform.position, player.transform.position);
		Vector2 direction = player.transform.position - transform.position;

		// If the distance of the enemy to the player is less that the preset distance between them then the enemy will pursuit the player
		if (distance < distanceBetween)
		{
			transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position,
			enemyMoveSpeed * Time.deltaTime);

			FlipSprite();
		}
	}

	public void Dying()
	{
		// TODO create death animation before rehooking this back up
		//enemyAnimator.SetTrigger("isDead");
		//GetComponent<BoxCollider2D>().enabled = false;
		//enemyRigidBody.bodyType = RigidbodyType2D.Static;
		Destroy(gameObject);
	}

	private void FlipSprite()
	{
		if (isBoss)
		{
			if (transform.position.x < player.transform.position.x)
			{
				transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
			}
			else
			{
				transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
			}
		}
		else
		{
			if (transform.position.x < player.transform.position.x)
			{
				transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
			}
			else
			{
				transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
			}
		}
	}
}
