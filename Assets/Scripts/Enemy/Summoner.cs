using UnityEngine;

public class Summoner : Enemy
{
	public Transform GhoulSummonPoint;
	public GameObject SummonedGhoulPrefab;

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (spriter.flipX)
		{
			GhoulSummonPoint.transform.localPosition = new Vector2(-2f, 0f);
		}
		else
		{
			GhoulSummonPoint.transform.localPosition = new Vector2(2f, 0f);
		}
	}
	public void SummonGhoul()
	{
		GameObject SummonedGhoul = Instantiate(SummonedGhoulPrefab, GhoulSummonPoint);
		SummonedGhoul.transform.parent = this.transform;
	}
}
