using UnityEngine;
using System.Collections;

public class l5_spinplatform : MonoBehaviour {

	//changes
	void Start(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
	}
}
