using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackEffect : MonoBehaviour
{
	public Type type;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			PlayerHp.Instance.TakeDamage(type.damageVal, transform.position);
		}
	}

}
[Serializable]
public class Type
{
	public enum AttackType
	{
		BloodHit1, BloodHit2, BloodHit3, BloodHit4, BloodHit5
	}
	public AttackType attacktype;
	public float damageVal;
}
