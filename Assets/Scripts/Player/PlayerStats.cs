using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private readonly float maxHp = 100f;
    private readonly float maxMp = 100f;
    private readonly float increaseInterval = 3f;

    protected float currentHp;
    protected float currentMp;
    private float elapsedTime;
   
    protected virtual void Awake()
    {		
		currentHp = maxHp;
        currentMp = maxMp;
        elapsedTime = 0f;
    }
    protected void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > increaseInterval) 
        { 
            elapsedTime = 0f;
            IncreaseStats(1f);
        }
    }
	protected virtual void IncreaseStats(float amount)
	{
        currentHp = Mathf.Min(maxHp, currentHp + amount);
        currentMp = Mathf.Min(maxMp, currentMp + amount);
	}

	protected void UpdateStats(Image[] imageArray,float currentVal)
    {
        int heartIndex=Mathf.FloorToInt(currentVal/12.5f);
        for(int i=0; i<imageArray.Length; i++)
        {
            if (i <= heartIndex)
            {
                imageArray[i].enabled = true;
            }
            else
            {
                imageArray[i].enabled = false;
            }
        }
    }
}
