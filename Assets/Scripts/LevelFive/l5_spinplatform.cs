using UnityEngine;
using System.Collections;

public class l5_spinplatform : MonoBehaviour
{

	public int speed;

	//changes
	void Start(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (0, speed, 0) * Time.deltaTime);
	}
}