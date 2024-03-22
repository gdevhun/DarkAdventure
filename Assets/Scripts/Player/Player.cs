using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
	public static Player Instance = null;

	[SerializeField] private Vector2 inputVec;
	private Rigidbody2D rigid;
	private SpriteRenderer spriter;
	private Animator animator;
	private CapsuleCollider2D capsuleCollider;
	public GameObject playerLighter;
	public GameObject SwordPos;
	public GameObject SkillEffect;
	private AudioSource playerFootSound;

	public bool isGrounded;
	private bool isImortal;
	private float jumpVelocity = 12f;
	private float speed;	
	private readonly float walkSpeed = 3.5f;
	private readonly float runSpeed = 5f;

	private WaitForSeconds imortalTime = new WaitForSeconds(0.1f);
	public float playerAttackDmg = 3f;
	private readonly float attackCoolTime = 0.7f;
	private bool isAttacking = false;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance= this;
		}
		else if (Instance != this)
		{
			Destroy(this);
		}

	}
	void Start()
    {
		rigid = GetComponent<Rigidbody2D>();
		spriter = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		capsuleCollider=GetComponent<CapsuleCollider2D>();
		playerFootSound = GetComponent<AudioSource>();
	}
	private void Update()
	{

		inputVec.x = Input.GetAxisRaw("Horizontal");
		speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

		if (isImortal)
		{
			return;
		}


		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			Jump();
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			Attack_Q();
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			Attack_W();
		}
		else if (Input.GetKeyDown(KeyCode.E))
		{
			Counter();
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			Attack_R();
		}

	}
	private void FixedUpdate()
	{
		if(isImortal)
		{
			return;
		}

		rigid.velocity=new Vector2(inputVec.x*speed*Time.fixedDeltaTime*50f,rigid.velocity.y);

		if (inputVec.x != 0 && isGrounded)
		{
			PlayFootstepSound();
		}
		else
		{
			playerFootSound.Stop();
		}
	}
	private void LateUpdate()
	{
		if(inputVec.x != 0)
		{
			if (inputVec.x < 0)
			{
				spriter.flipX = true;
				SwordPos.transform.localPosition = new Vector2(-1, 0);
				SkillEffect.transform.localPosition=new Vector2(-1, 0);
				playerLighter.transform.rotation = Quaternion.Euler(0, 0, 90);
			}
			//spriter.flipX = inputVec.x < 0;
			else 
			{
				spriter.flipX = false;
				SwordPos.transform.localPosition = new Vector2(1, 0);
				SkillEffect.transform.localPosition = new Vector2(1, 0);
				playerLighter.transform.rotation = Quaternion.Euler(0, 0, -90);
			}

		}
		
		animator.SetFloat("Speed", Mathf.Abs(inputVec.x)*speed);
	
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Ground"))
		{
			ContactPoint2D contact = other.GetContact(0);
			// 아래로 떨어지는 방향을 확인
			Vector2 fallingDirection = Vector2.up; // 아래로 떨어지는 방향을 나타내는 벡터

			// 두 벡터의 방향을 비교
			isGrounded = (Vector2.Dot(contact.normal, fallingDirection) > 0.6);
			Debug.Log(Vector2.Dot(contact.normal, fallingDirection));
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
	private void PlayFootstepSound()
	{
		// 발소리
		if (!playerFootSound.isPlaying)
		{
			playerFootSound.Play();
		}
	}
	private void Jump()
	{
		SoundManager.Instance.PlaySFX(SoundType.PlayerJumpSFX, 1f);
		isGrounded = false;
		rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity);
		animator.SetBool("isJump", !isGrounded);
	}

	private void Attack_Q()
	{
		if(!isAttacking && isGrounded)
		{
			animator.SetTrigger("Attack1");
			SoundManager.Instance.PlaySFX(SoundType.PlayerSwingSFX, 1f);
			StartCoroutine(AttackCoolTime());
		}
	}
	private void Attack_W()
	{
		if (!isAttacking && isGrounded)
		{
			animator.SetTrigger("Attack2");
			SoundManager.Instance.PlaySFX(SoundType.PlayerSwingSFX2, 1f);
			StartCoroutine(AttackCoolTime());
		}
	}
	private void Counter()
	{
		if (!isAttacking && isGrounded && (PlayerMp.Instance.CurrentMp >= 5))
		{
			animator.SetTrigger("Counter");
			SoundManager.Instance.PlaySFX(SoundType.PlayerElecSFX, 1f);
			PlayerMp.Instance.UseSkill(5f);
		}
	}
	private void Attack_R()
	{
		if (!isAttacking && isGrounded && (PlayerMp.Instance.CurrentMp >= 10)) 
		{
			animator.SetTrigger("Attack4");
			SoundManager.Instance.PlaySFX(SoundType.PlayerElecSFX2, 1f);
			PlayerMp.Instance.UseSkill(10f);
			StartCoroutine(AttackCoolTime());
		}

	}
	public void PlayerHitSound()
	{
		int randomHitSound = Random.Range(0, 2); //0이상 1미만.
		if (randomHitSound == 0)
		{
			SoundManager.Instance.PlaySFX(SoundType.PlayerHitSFX, 1f);
		}
		else
		{
			SoundManager.Instance.PlaySFX(SoundType.PlayerHitSFX2, 1f);
		}
	}
	private IEnumerator AttackCoolTime()
	{
		isAttacking = true;
		yield return new WaitForSeconds(attackCoolTime);
		isAttacking = false;
	}

	public void DamageUp(float _dmg)
	{
		playerAttackDmg += _dmg;
	}
	public void PlayerLightUP() //아이템획득시 주변시야 밝게하는 함수
	{
		Light2D playerLight2d=GetComponentInChildren<Light2D>();
		if(playerLight2d.falloffIntensity>=0.5)
			playerLight2d.falloffIntensity -= 0.1f;
		return;
	}
	public void KnockBack(Vector2 enemyPos) //넉벡 O
	{
		// 무적 타임 발동
		isImortal = true;
		// 애니메이션 실행 
		animator.SetBool("isHit", true);
		PlayerHitSound();
		StartCoroutine(ImortalRoutine());
		
		// 물리적 넉백 
		Vector2 playerPos = transform.position;
		Vector2 forceDirection = (playerPos- enemyPos).normalized;
		forceDirection.y = 1f;
		rigid.AddForce(forceDirection * 9, ForceMode2D.Impulse);

	}
	public void NotKnockBack()  //넉벡X
	{
		// 무적 타임 발동
		isImortal = true;
		// 애니메이션 실행 
		animator.SetBool("isHit", true);
		PlayerHitSound();
		StartCoroutine(ImortalRoutine());

	}
	private IEnumerator ImortalRoutine()
	{
		yield return imortalTime;
		isImortal = false;
		animator.SetBool("isHit", false);
	}
	public void PlayerDeadEvent()
	{   //버그있는 부분
		animator.SetTrigger("isDead");
		rigid.gravityScale = 0f;
		rigid.simulated = false;
		capsuleCollider.enabled = false;
		enabled = false;
	}
	public void OnUseplayer(Item _itemData)
	{
		switch(_itemData.type)
		{
			case Item.Type.HpPotion:
				PlayerHp.Instance.RestoreHp(_itemData.value);
				SoundManager.Instance.PlaySFX(SoundType.PlayerUsePotion, 1f);
				break;
			case Item.Type.MpPotion:
				PlayerMp.Instance.RestoreMp(_itemData.value);
				SoundManager.Instance.PlaySFX(SoundType.PlayerUsePotion, 1f);
				break;
			case Item.Type.Lighter:
				PlayerLightUP();
				SoundManager.Instance.PlaySFX(SoundType.PlayerUserItem2, 1f);
				break;
			case Item.Type.PowerItem:
				DamageUp(_itemData.value);
				SoundManager.Instance.PlaySFX(SoundType.PlayerUseItem, 1f);
				break;
		}
	}
}
