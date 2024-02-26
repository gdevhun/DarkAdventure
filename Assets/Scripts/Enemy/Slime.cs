using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
	protected override void FixedUpdate()
	{

		if (isDetectedPlayer && currentHP >= 0)
		{
			playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
			if (playerTrans.position.x > transform.position.x)
			{
				spriter.flipX = true;
			}
			else
			{
				spriter.flipX = false;
			}
			if (!isImortal || timer >= attackDelay)
			{
				Vector2 dir = (playerTrans.position - transform.position);
				dir.y = 0; dir.Normalize();
				rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
			}

			//Ray�� �����Ͽ� �÷��̾ ���� ���
			ray = new Ray2D(transform.position, (playerTrans.position - transform.position).normalized);

			// Ray�� �ð�ȭ
			Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.red);

			// Ray�� �浹�� ��ü�� Ȯ��
			hit = Physics2D.Raycast(ray.origin, ray.direction, 0.5f, 1 << 6 | 1 << 0);

			if (hit.collider == null) return;

			// �÷��̾�� �浹�� ���
			if (hit.collider.CompareTag("Player"))
			{
				if (timer >= attackCooltime)
				{
					anim.SetTrigger("isAttack");

					PlayerHp.Instance.TakeDamage(enemyAttackDmg, transform.position);
					timer = 0;
				}
			}

		}
		// �÷��̾ ���� ������ ����� ���� ���¸� �����ϰ� �ٽ� ���� ���·� ��ȯ
		else if (!isDetectedPlayer)
		{

			// ���� �߿��� ���� ����
			if (endPos.x > startPos.x)
			{
				spriter.flipX = true;
			}
			else
			{
				spriter.flipX = false;
			}
			Vector2 dir = endPos;
			dir.y = 0; dir.Normalize();
			transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.fixedDeltaTime);

			// �������� �����ϸ� ���� ��ġ�� �� ��ġ�� ��ü
			if (Vector2.Distance(transform.position, endPos) <= 0)
			{
				Vector2 temp = startPos;
				startPos = endPos;
				endPos = temp;
			}
		}
	}
}
