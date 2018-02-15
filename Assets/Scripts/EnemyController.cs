using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed;
	private float xDir;
	private float zDir;
	Rigidbody rb;
	Vector3 movement;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		// Give the enemy a random direction to go in
		xDir = Random.Range(-3, 3);
		zDir = Random.Range(-3, 3);
		movement = new Vector3 (xDir, 0, zDir) * Time.deltaTime;

	}

	void OnCollisionEnter(Collision col) {
		// Flip the appropriate force axis when colliding with a wall
		if (col.gameObject.tag == "xWall") {
			movement.x *= -1;
		}
		if (col.gameObject.tag == "yWall") {
			movement.z *= -1;
		}
		if (col.gameObject.tag == "Enemy") {
			movement.z *= -1;
			movement.x *= -1;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Always be moving
		rb.AddForce (movement * speed);
	}
}
