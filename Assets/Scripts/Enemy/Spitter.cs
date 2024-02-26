using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
	public GameObject projectilePrefab;
	public Transform projectileSpawnPoint;
	public float projectileSpeed;
	private Vector2 dirVec;
	private float angle;

	protected override void FixedUpdate()
	{			
		if (isDetectedPlayer && currentHP >= 0)
		{
			playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
			if (playerTrans.position.x > transform.position.x)
			{
				spriter.flipX = false;
			}
			else
			{
				spriter.flipX = true;
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
			Debug.DrawRay(ray.origin, ray.direction *2f, Color.red);

			// Ray에 충돌한 객체를 확인
			hit = Physics2D.Raycast(ray.origin, ray.direction, 2f, 1 << 6 | 1 << 0);

			if (hit.collider == null) return;

			// 플레이어와 충돌한 경우
			if (hit.collider.CompareTag("Player"))
			{

				if (timer >= attackCooltime)
				{
					anim.SetTrigger("isAttack");
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
				spriter.flipX = false;
			}
			else
			{
				spriter.flipX = true;
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

		if (spriter.flipX)
		{
			projectileSpawnPoint.transform.localPosition = new Vector2(-0.2f, 0.4f);
		}
		else
		{
			projectileSpawnPoint.transform.localPosition = new Vector2(0.2f, 0.4f);
		}
	}
	public void ThrowProjectile()
	{
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

		// 플레이어 위치를 향한 방향 벡터 계산
		dirVec = playerTrans.position - transform.position;

		// 방향 벡터를 통해 회전 각도 계산
		angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;

		// projectile 발사
		GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
		projectile.transform.parent = this.transform;
		// 방향 벡터를 통해 add force
		Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
		projectileRb.AddForce(dirVec.normalized * projectileSpeed, ForceMode2D.Impulse);
	}
	
}
