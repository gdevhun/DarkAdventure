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

			//Ray�� �����Ͽ� �÷��̾ ���� ���
			ray = new Ray2D(transform.position, (playerTrans.position - transform.position).normalized);

			// Ray�� �ð�ȭ
			Debug.DrawRay(ray.origin, ray.direction *2f, Color.red);

			// Ray�� �浹�� ��ü�� Ȯ��
			hit = Physics2D.Raycast(ray.origin, ray.direction, 2f, 1 << 6 | 1 << 0);

			if (hit.collider == null) return;

			// �÷��̾�� �浹�� ���
			if (hit.collider.CompareTag("Player"))
			{

				if (timer >= attackCooltime)
				{
					anim.SetTrigger("isAttack");
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
				spriter.flipX = false;
			}
			else
			{
				spriter.flipX = true;
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

		// �÷��̾� ��ġ�� ���� ���� ���� ���
		dirVec = playerTrans.position - transform.position;

		// ���� ���͸� ���� ȸ�� ���� ���
		angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;

		// projectile �߻�
		GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
		projectile.transform.parent = this.transform;
		// ���� ���͸� ���� add force
		Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
		projectileRb.AddForce(dirVec.normalized * projectileSpeed, ForceMode2D.Impulse);
	}
	
}
