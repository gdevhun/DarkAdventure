using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Trap : MonoBehaviour
{
	public float hitDamage;
	public float hitTime;
	private bool isPlayerInside = false;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player")) 
		{
			isPlayerInside = true;
			DamagePlayer().Forget();
			
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			isPlayerInside = false;
		}
	}
	private async UniTaskVoid DamagePlayer()
	{
		while(isPlayerInside)
		{
			//플레이어 피감소, 데미지 이펙트 에니메이션 추가필요
			PlayerHp.Instance.TakeDamage(hitDamage);
			Player.Instance.NotKnockBack();
			await UniTask.Delay(TimeSpan.FromSeconds(hitTime));
		}
	}
}
