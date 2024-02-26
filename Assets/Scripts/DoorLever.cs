using UnityEngine;
using DG.Tweening;
public class DoorLever : MonoBehaviour
{
	public GameObject Door;
	private AudioSource DoorSound;

	void Start()
	{
		DoorSound = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Sword"))
		{
			DoorSound.Play();
			Door.transform.DOLocalMove(new Vector2(162.5f,-5f), 4f).SetEase(Ease.InOutCubic);
		}
	}
}

