using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	public bool isClosest;

	void Start(){
		isClosest = false;
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);


		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Vector3 gap = transform.position - player.transform.position;
		gap.Set (gap.x, gap.y, gap.z);
		gap.Normalize();

		// Move if its the closest pick up
		if (isClosest) {
			transform.Translate (gap * Time.deltaTime, Space.World);
		}
	}
}
