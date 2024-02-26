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

			//Ray를 생성하여 플레이어를 향해 쏜다
			ray = new Ray2D(transform.position, (playerTrans.position - transform.position).normalized);

			// Ray를 시각화
			Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.red);

			// Ray에 충돌한 객체를 확인
			hit = Physics2D.Raycast(ray.origin, ray.direction, 0.5f, 1 << 6 | 1 << 0);

			if (hit.collider == null) return;

			// 플레이어와 충돌한 경우
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
		// 플레이어가 감지 범위를 벗어나면 감지 상태를 해제하고 다시 순찰 상태로 전환
		else if (!isDetectedPlayer)
		{

			// 순찰 중에도 방향 설정
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

			// 목적지에 도달하면 시작 위치와 끝 위치를 교체
			if (Vector2.Distance(transform.position, endPos) <= 0)
			{
				Vector2 temp = startPos;
				startPos = endPos;
				endPos = temp;
			}
		}
	}
}
