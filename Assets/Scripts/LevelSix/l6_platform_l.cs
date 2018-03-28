using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class l6_platform_l : MonoBehaviour
{
	public float speed;
	public float time;
	private bool moveleft;
	private bool keepMoving = true;

	private void Start()
	{
		StartCoroutine(moveswitch());
	}

	IEnumerator moveswitch()
	{
		while (keepMoving)
		{
			if (moveleft)
			{
				moveleft = false;
			}
			else
			{
				moveleft = true;
			}

			yield return new WaitForSeconds(time);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (moveleft)
		{
			transform.Translate(0, 0, -speed);
		}
		else
		{
			transform.Translate(0, 0, speed);
		}

		print(transform.position);
	}
}