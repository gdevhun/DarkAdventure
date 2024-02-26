using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonedGhoul : Enemy
{
	protected override void Awake()
	{
		spriter = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		currentHP = maxHP;
	}
	private void Start()
	{
		anim.SetTrigger("isSummon");
		Invoke("SetPos", 2f);
	}
	private void SetPos()
	{
		startPos = this.transform.position;
		endPos = new Vector2(this.transform.position.x, this.transform.position.y) + Vector2.right * 2;
	}
}
