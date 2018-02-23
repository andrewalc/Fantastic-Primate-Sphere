using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
	public Text countText;
	public Text winText;
	public Text TimerText;
	public Text loseText;
    public int timeForLevel;
    public string nextLevel;

	public bool gameOver;

	private Rigidbody rb;

	private int count;
	private int currTime;


	void Start(){
		rb = GetComponent<Rigidbody> ();
		gameOver = false; // Is the game over?
		count = 0; // The number of pickups the player has
        currTime = timeForLevel; // How much time per level there is

		// Tick the timer every one second
		InvokeRepeating ("TimerTick", 0.0f, 1.0f);

		// Set up the UI text
		SetCountText ();
		SetTimerText ();
		winText.text = "";
		loseText.text = "";
	}
    // shows "forward"
//    void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Vector3 direction = transform.TransformDirection(rb.velocity) * 5;
 //       Gizmos.DrawRay(transform.position, direction);
 //   }
    // Update is called once per frame
    void FixedUpdate() { }

	// Tick the timer one second and update the timer text
	void TimerTick(){
		if (!gameOver) {
            if (currTime >= 1)
            {
                currTime--;
                SetTimerText();
            }
		}
	}

	// On collision with an Enemy or if the player runs out of time, the player loses
	void OnCollisionEnter(Collision col) {
		if (!gameOver && col.gameObject.tag == "Enemy" || currTime <= 0) {
			loseText.text = "You lose!";
			gameOver = true;
            SceneManager.LoadScene("Scenes/" + "LevelOne");
        }
        if (!gameOver && col.gameObject.tag == "Goal" && currTime > 0)
        {
            winText.text = "You Win!";
            gameOver = true;
            SceneManager.LoadScene("Scenes/" + nextLevel);
        }
    }

	// On trigger with a pick up, increase the score and deactivate the pick up
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	// Set the count
	void SetCountText(){
		countText.text = "Count: " + count.ToString ();
		if (!gameOver && count >= 9) {
			winText.text = "You Win!";
			gameOver = true;
		}
	}

    // Set the timer
	void SetTimerText(){
		TimerText.text = "Time: " + currTime.ToString ();
	}
}