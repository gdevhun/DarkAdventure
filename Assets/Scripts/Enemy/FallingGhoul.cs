using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGhoul : Enemy
{
	public bool isFalled = false;

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
		anim.SetTrigger("isWake");
	}
	protected override void FixedUpdate()
	{
		if (isFalled)
		{
			base.FixedUpdate();
		}
	}
	protected override void OnTriggerEnter2D(Collider2D collision)
	{  

		if (collision.CompareTag("Player"))
		{
			isDetectedPlayer = true;
			if (!isFalled) 
			{
				isFalled = true;
				rb.gravityScale = 1f;
				anim.SetTrigger("isWalk");
			}
		}
	}
	protected override void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isDetectedPlayer = false;			
		}
	}
	public void WakeAnimLoop()
	{
		anim.SetTrigger("isWake");
	}
}
