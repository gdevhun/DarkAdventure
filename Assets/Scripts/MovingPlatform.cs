using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    public float speed;
    
    void Start()
    {
        transform.position = startPos; 
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.transform.CompareTag("Player")){
            collision.transform.SetParent(transform);
        }
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.transform.CompareTag("Player"))
		{
			collision.transform.SetParent(null);
		}
	}
	void FixedUpdate()
    {
        //lerp(a,b,t) �������� : a����, b�� t����������(0~1)
        transform.position=Vector2.MoveTowards(transform.position, endPos, speed * Time.fixedDeltaTime);

        //Distance(a,b) a�� b���� �Ÿ� ���
         if(Vector2.Distance(transform.position, endPos) < 0.05f)
         {
             Vector2 temp = startPos;
             startPos = endPos;
             endPos = temp;
         }
    }
}
