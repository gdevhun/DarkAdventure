using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
			StartCoroutine(DamagePlayer());
			
		}
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			isPlayerInside = false;
		}
	}
	IEnumerator DamagePlayer()
	{
		while(isPlayerInside)
		{
			//�÷��̾� �ǰ���, ������ ����Ʈ ���ϸ��̼� �߰��ʿ�
			PlayerHp.Instance.TakeDamage(hitDamage);
			Player.Instance.NotKnockBack();
			yield return new WaitForSeconds(hitTime);
		}
	}
}
