using DG.Tweening;
using UnityEngine;

public class BossRoomCameraEvent : MonoBehaviour
{
	public Transform targetPosition;
	public float duration = 6f;

	public void CameraMoveEvent()
	{
		transform.DOMove(targetPosition.position, duration).SetEase(Ease.Linear);
	}

}
