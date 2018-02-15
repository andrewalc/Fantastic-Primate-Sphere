using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
	public Text countText;
	public Text winText;
	public Text TimerText;
	public Text loseText;

	private bool airborne;
	private bool timerStarted;
	private bool gameOver;

	private Rigidbody rb;

	private int count;
	private int currTime;
	private int numOfPickUps;

	private GameObject[] pickUps;
	private Transform[] pickUpTransforms;


	void Start(){
		rb = GetComponent<Rigidbody> ();
		airborne = false; // Is the player in the air?
		timerStarted = false; // Has the timer started yet?
		gameOver = false; // Is the game over?
		numOfPickUps = 9; // The number of pick ups left
		count = 0; // The number of pickups the player has
		currTime = 0; // The current time on the timer

		// Tick the timer every one second
		InvokeRepeating ("TimerTick", 0.0f, 1.0f);

		// Set up the UI text
		SetCountText ();
		SetTimerText ();
		winText.text = "";
		loseText.text = "";

		// Create an array to hold all pick up transforms
		pickUps = GameObject.FindGameObjectsWithTag ("Pick Up");
		pickUpTransforms = new Transform[numOfPickUps];
		for (int i = 0; i < numOfPickUps; i++) {
			pickUpTransforms [i] = pickUps[i].transform;
		}
	}

	// Returns the transform of the closest pick up
	Transform getNearestPickUpTransform(){
		Transform result = null;
		float closestDistanceMag = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		foreach (Transform pickUp in pickUpTransforms) {
			Vector3 gap = pickUp.position - currentPosition;
			float gapMag = gap.sqrMagnitude;
			if (gapMag < closestDistanceMag) {
				closestDistanceMag = gapMag;
				result = pickUp;
			}
		}

		return result;
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		// Begin the timer when the ball starts rolling
		if (!timerStarted && (rb.velocity.x > 0 || rb.velocity.y > 0)) {
			timerStarted = true;
		}

		// Jump force resets to 0
		float jump = 0;

		// If the player is not moving up or down, it is not airborne
		if (rb.velocity.y == 0) {
			airborne = false;
		}


		// Allow jumping only if we are on the ground (not airborne)
		if (!airborne && Input.GetKey (KeyCode.Space)) {
			jump = jumpSpeed;
			airborne = true;
		}

		// Update the pick ups' isClosest flag to keep track of what is the closest pickup
		foreach (GameObject pickUp in pickUps) {
			Rotater script = GameObject.Find(pickUp.name).GetComponent<Rotater>();
			Transform nearest = getNearestPickUpTransform();
			if (pickUp.transform == nearest) {
				if (!script.isClosest) {
					script.isClosest = true;
				}

			} else {
				script.isClosest = false;
			}
		}

		// Get horizontal and vertical forces from Input
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// The final movement vector before applying it to the player
		Vector3 movement = new Vector3 (moveHorizontal, jump, moveVertical) * Time.deltaTime;

		// Allow movement as long as the game isn't over
		if (!gameOver) {
			rb.AddForce (movement * speed);
		}

	}

	// Tick the timer one second and update the timer text
	void TimerTick(){
		if (!gameOver && timerStarted) {
			currTime++;
			SetTimerText ();
		}
	}

	// On collision with an Enemy, the player loses
	void OnCollisionEnter(Collision col) {
		if (!gameOver && col.gameObject.tag == "Enemy") {
			loseText.text = "You lose!";
			gameOver = true;
		}
	}

	// On trigger with a pick up, increase the score and deactivate the pick up
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();

			// Update transforms array
			numOfPickUps--;
			if (numOfPickUps > 0) {
				pickUps = GameObject.FindGameObjectsWithTag ("Pick Up");
				pickUpTransforms = new Transform[numOfPickUps];
				for (int i = 0; i < numOfPickUps; i++) {
					pickUpTransforms [i] = pickUps[i].transform;
				}
			}
		}
	}

	// Set the 
	void SetCountText(){
		countText.text = "Count: " + count.ToString ();
		if (!gameOver && count >= 9) {
			winText.text = "You Win!";
			gameOver = true;
		}
	}

	void SetTimerText(){
		TimerText.text = "Time: " + currTime.ToString ();
	}
}