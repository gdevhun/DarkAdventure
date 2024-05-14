using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : SingletonBehaviour<SoundManager>
{
	[System.Serializable]
	public class Sound
	{
		public SoundType soundType;
		public AudioClip clip;
	}

	[SerializeField]
	private List<Sound> bgms = null;
	[SerializeField]
	private List<Sound> sfxs = null;
	[SerializeField]
	private AudioSource bgmPlayer = null;
	public List<AudioSource> sfxPlayers = new List<AudioSource>();

	private Dictionary<SoundType, AudioClip> _sfxDictionary;

	private Queue<AudioSource> _sfxQueue = new Queue<AudioSource>();

	public float bgmVolume, sfxVolume; // ����� ���� �� ȿ���� ����
	protected override void Awake()
	{
		base.Awake();

		// SFX ��ųʸ� �ʱ�ȭ
		_sfxDictionary = sfxs.ToDictionary(s => s.soundType, s => s.clip);
	}

	private void Start()
	{
		bgmPlayer = gameObject.AddComponent<AudioSource>();
		// ���� �ʱ�ȭ
		bgmVolume = 0.6f; 
		sfxVolume = 1f;
		
		// �޴�bgm���
		SoundManager.Instance.PlayBGM(SoundType.MenuBGM);

		// SFX �÷��̾� �� ���� �ʱ⿡ �����ϰ� ����Ʈ�� �߰�
		for (int i = 0; i < 20; i++)
		{
			AudioSource sfxPlayer = gameObject.AddComponent<AudioSource>();
			sfxPlayers.Add(sfxPlayer);
			_sfxQueue.Enqueue(sfxPlayer);
		}
	}

	public void PlayBGM(SoundType soundType)
	{
		var bgm = bgms.First(b => b.soundType == soundType);
		bgmPlayer.clip = bgm.clip;
		bgmPlayer.volume = bgmVolume; // ���� ���� bgmVolume
		bgmPlayer.loop = true;
		bgmPlayer.Play();
	}

	public void StopBGM()
	{
		bgmPlayer.Stop();
	}
	
	
	// ���� �÷��̰� ������ ȣ���Ͽ� ť�� �ٽ� �ֱ�
	public void ReturnSFXPlayerToQueue(AudioSource sfxPlayer)
	{
		_sfxQueue.Enqueue(sfxPlayer);
	}
	
	// ���� ����� ������ Ǯ�� ��ȯ
	private IEnumerator ReturnSFXPlayerWhenFinished(AudioSource sfxPlayer, float delay)
	{
		// ���� ��� �ð���ŭ ���
		yield return new WaitForSeconds(delay);

		// ���� ����� �������� ť�� ��ȯ
		ReturnSFXPlayerToQueue(sfxPlayer);
	}
	public void PlaySFX(SoundType soundType)
	{
		if (_sfxDictionary.TryGetValue(soundType, out AudioClip clip))
		{
			AudioSource sfxPlayer = GetAvailableSFXPlayer();
			sfxPlayer.clip = clip;
			sfxPlayer.volume = sfxVolume; // ���� ���� sfxVolume
			sfxPlayer.Play();

			// ���� ����� ������ Ǯ�� ��ȯ
			StartCoroutine(ReturnSFXPlayerWhenFinished(sfxPlayer, clip.length));
		}
	}

	private AudioSource GetAvailableSFXPlayer()
	{
		if (_sfxQueue.Count > 0)
		{
			return _sfxQueue.Dequeue();
		}
		else
		{
			// �� �÷��̾ �����ϰ� ����Ʈ�� �߰�
			AudioSource newSFXPlayer = gameObject.AddComponent<AudioSource>();
			sfxPlayers.Add(newSFXPlayer);
			return newSFXPlayer;
		}
	}
	
	// ����� ���� ����
	public void SetBgmVolume(float volume)
	{
		// �����̴� �������� ���� ����
		bgmPlayer.volume = volume;

		// �����̴� ���� ������ �����ؼ� ��������� �����Ҷ����� ������ ����
		bgmVolume = volume;
	}

	// ȿ���� ���� ����
	public void SetSfxVolume(float volume)
	{
		// �����̴� ���� ������ �����ؼ� ȿ������ �����Ҷ����� ������ ����
		sfxVolume = volume;
	}
}
public enum SoundType
{
	MenuBGM = 0,
	FirstBGM = 1,
	SecondBGM = 2,

	PlayerWalkSFX=3,
	PlayerSwingSFX=4,
	PlayerSwingSFX2=5,
	PlayerCounterSFX3 = 6,
	PlayerJumpSFX = 7,
	PlayerCutSFX = 8,
	PlayerHitSFX=9,
	PlayerHitSFX2=10,
	PlayerElecSFX=11,
	PlayerElecSFX2=12,
	PlayerUsePotion=13,
	PlayerUseItem=14,
	PlayerGetItem=15,
	PlayerBubbleSFX=16,
	PlayerInventoryOpen=17,
	PlayerUserItem2=18,
	EnemyHit1=19,
	EnemyHit2=20,
	EnmeySfx=21,
	EnemySfx2=22,
	EnemySfx3=23
}