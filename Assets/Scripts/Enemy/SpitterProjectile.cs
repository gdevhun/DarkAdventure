using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterProjectile : MonoBehaviour
{
	private float enemyAttackDmg = 8f;
	private WaitForSeconds coolTime=new WaitForSeconds(4.5f);
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>(); 
	}

	private void Start()
	{
		StartCoroutine(ActiveTime());
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
	private IEnumerator ActiveTime()
	{
		yield return coolTime;
		DisableObj();

	}
}

