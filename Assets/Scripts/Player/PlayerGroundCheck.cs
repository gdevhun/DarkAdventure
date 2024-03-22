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
			// �Ʒ��� �������� ������ Ȯ��
			Vector2 fallingDirection = Vector2.up; // �Ʒ��� �������� ������ ��Ÿ���� ����

			// �� ������ ������ ��
			isGrounded = (Vector2.Dot(contact.normal, fallingDirection) > 0.6);

			// �浹 ������ ���� ���Ͱ� �Ʒ��� �������� ����� �뷫������ ��ġ�ϴ� ���		
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
