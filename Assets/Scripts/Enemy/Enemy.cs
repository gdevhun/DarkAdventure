using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	protected Animator anim;
	protected Transform playerTrans = null;
	protected Rigidbody2D rb;
	protected SpriteRenderer spriter;
	protected Collider2D col;

	public Vector2 startPos;
	public Vector2 endPos;
	public float speed;
	protected bool isImortal;
	private readonly float imortalTime = 0.5f;
	protected RaycastHit2D hit;
	protected Ray2D ray;

	public float enemyAttackDmg;	 
	protected readonly float attackCooltime = 1.5f;
	protected float attackDelay = 0.3f;
	protected float timer;

	public bool isDetectedPlayer = false;

	protected float currentHP;
	public float maxHP;

	protected virtual void Awake()
	{
		spriter = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		transform.position = startPos;
		currentHP = maxHP;
	}
	protected void Update()
	{
		timer += Time.deltaTime;
	}
	protected virtual void FixedUpdate()
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
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{  
		if (collision.CompareTag("Player"))
		{
			isDetectedPlayer = true;
		}
	}
	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isDetectedPlayer = false;
		}
	}

	public void DamagedbyPlayer(float playerDmg)
	{
		if (isImortal)
		{
			return;
		}
		isImortal = true;
		ImortalRoutine().Forget();
		Vector2 forceDirection = (transform.position - playerTrans.position).normalized;
		forceDirection.y = 0.6f;
		rb.AddForce(forceDirection * 3, ForceMode2D.Impulse);

		//�÷��̾� �ǰ� (hp����), �÷��̾�� ������ �ǰ�
		currentHP -= playerDmg;
		Debug.Log(currentHP);
		if (currentHP <= 0)
		{
			anim.SetTrigger("isDead");
			isImortal = true;
			rb.simulated = false;
			col.enabled = false;
			return;
		}
		anim.SetTrigger("isHit");
	}

	public void DisableEnemy()
	{   // �ִϸ��̼� �̺�Ʈ���� ȣ��� �Լ�
		gameObject.SetActive(false);
	}
	private async UniTaskVoid ImortalRoutine()
	{
		await UniTask.Delay(TimeSpan.FromSeconds(imortalTime));
		isImortal = false;
	}
	
}

