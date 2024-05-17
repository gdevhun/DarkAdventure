using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SpitterProjectile : MonoBehaviour
{
	private float enemyAttackDmg = 8f;
	private float coolTime=4.5f;
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>(); 
	}

	private void Start()
	{
		ActiveTime().Forget();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayerHp.Instance.TakeDamage(enemyAttackDmg);
			anim.SetTrigger("Burst");

		}
	}
	public void DisableObj()
	{
		gameObject.SetActive(false);
	}
	private async UniTaskVoid ActiveTime()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(coolTime));
		DisableObj();

	}
}

