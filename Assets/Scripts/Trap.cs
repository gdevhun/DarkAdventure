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
			//�÷��̾� �ǰ���, ������ ����Ʈ ���ϸ��̼� �߰��ʿ�
			PlayerHp.Instance.TakeDamage(hitDamage);
			Player.Instance.NotKnockBack();
			await UniTask.Delay(TimeSpan.FromSeconds(hitTime));
		}
	}
}
