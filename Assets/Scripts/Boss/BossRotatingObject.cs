using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotatingObject : MonoBehaviour
{
	[SerializeField] private GameObject Bloodobj1;
	[SerializeField] private GameObject Bloodobj2;
	[SerializeField] private GameObject Bloodobj3;

	public float rotatingSpeed = 75f;

	public Type objectType; 

	private GameObject[] rotatingObjects;

	void Start()
	{
		if (this.objectType == Type.bloodHit1)
		{
			rotatingObjects = new GameObject[5];
			SpawnRotatingObject(4);
		}
		else if (this.objectType == Type.bloodHit2)
		{
			rotatingObjects = new GameObject[4];
			SpawnRotatingObject(4);
		}
		else if (this.objectType == Type.bloodHit3)
		{
			rotatingObjects = new GameObject[3];
			SpawnRotatingObject(3);
		}

	}
	void Update()
	{
		transform.Rotate(Vector3.forward, rotatingSpeed * Time.deltaTime);
	}

	private void SpawnRotatingObject(int num)
	{	
		for (int i = 0; i < num; i++)
		{
			rotatingObjects[i] = Instantiate(GetBloodObject());
			rotatingObjects[i].transform.SetParent(this.transform);
			float angle = i * 360f / num;
			rotatingObjects[i].transform.localPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 2f, Mathf.Sin(angle * Mathf.Deg2Rad) * 2f, 0f);
			rotatingObjects[i].transform.Rotate(Vector3.forward, angle);
		}
	}
	private GameObject GetBloodObject()
	{
		return objectType switch
		{
			Type.bloodHit1 => Bloodobj1,
			Type.bloodHit2 => Bloodobj2,
			Type.bloodHit3 => Bloodobj3,
			_ => null,
		};
	}
	public enum Type
	{
		bloodHit1,
		bloodHit2,
		bloodHit3
	}
}