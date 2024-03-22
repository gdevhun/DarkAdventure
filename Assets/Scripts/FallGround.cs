using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FallGround : MonoBehaviour
{
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GameManager.Instance.isGameOver = true;
			Player.Instance.PlayerHitSound();
			GameManager.Instance.LoseGame();
		}
		else if (collision.gameObject.CompareTag("Enemy"))
		{
			if(gameObject.TryGetComponent(out Enemy enemy)){
				enemy.DisableEnemy();
			}
		}
	}

}
