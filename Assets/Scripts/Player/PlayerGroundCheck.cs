using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
	private Animator animator;
	private bool isGrounded;
	private void Awake()
	{
		//GetComponentInParent<Player>();
		isGrounded = GetComponentInParent<Player>().isGrounded;
		animator=GetComponentInParent<Animator>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			ContactPoint2D contact = other.GetContact(0);
			// 아래로 떨어지는 방향을 확인
			Vector2 fallingDirection = Vector2.up; // 아래로 떨어지는 방향을 나타내는 벡터

			// 두 벡터의 방향을 비교
			isGrounded = (Vector2.Dot(contact.normal, fallingDirection) > 0.6);

			// 충돌 지점의 법선 벡터가 아래로 떨어지는 방향과 대략적으로 일치하는 경우		
			animator.SetBool("isJump", !isGrounded);
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		isGrounded = false;
		animator.SetBool("isJump", !isGrounded);
	}
	private void OnCollisionStay2D(Collision2D collision)
	{
		OnCollisionEnter2D(collision);
	}
}
