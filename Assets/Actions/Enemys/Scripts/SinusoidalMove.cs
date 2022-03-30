using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMove : MonoBehaviour
{

	[Header("移動速度")]
	public float moveSpeed = 5f;

	[Header("頻度")]
	public float frequency = 20f;

	[Header("波の大きさ")]
	public float magnitude = 0.5f;

	[Header("時間")] public float time = 0;

	bool facingRight = true;

	Vector3 pos, localScale;

	// Use this for initialization
	void Start()
	{

		pos = transform.position;

		localScale = transform.localScale;

	}

	// Update is called once per frame
	void Update()
	{
        if (Mathf.Sin(time) < 0)
        {
			MoveRight();
        }
        else
        {
			MoveLeft();
        }
		time += Time.deltaTime;

	}

	void CheckWhereToFace()
	{
		if (pos.x < -7f)
			facingRight = true;

		else if (pos.x > 7f)
			facingRight = false;

		if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
			localScale.x *= -1;

		transform.localScale = localScale;

	}

	void MoveRight()
	{
		//pos += transform.right * Time.deltaTime * moveSpeed;
		//transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, Mathf.Sin(time * frequency) * magnitude);
	}

	void MoveLeft()
	{
		//pos -= transform.right * Time.deltaTime * moveSpeed;
		//transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
		GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, Mathf.Sin(time * frequency) * magnitude);
	}

}