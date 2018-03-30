using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    public int timeForLevel;
    public string nextLevel;
	public bool gameOver;

	private Rigidbody rb;

	private int count;
	private int currTime;
	private int lifeCounter;
    private Text countText;
    private Text winText;
    private Text TimerText;
    private Text loseText;
	private Text lifeText;


    void Start(){
		rb = GetComponent<Rigidbody> ();
		gameOver = false; // Is the game over?
		count = 0; // The number of pickups the player has
        currTime = timeForLevel; // How much time per level there is

		// Tick the timer every one second
		InvokeRepeating ("TimerTick", 0.0f, 1.0f);

        // Set up the UI text
        countText = GameObject.Find("/Canvas/CountText").GetComponent<Text>();
        winText = GameObject.Find("/Canvas/WinText").GetComponent<Text>();
        TimerText = GameObject.Find("/Canvas/TimerText").GetComponent<Text>();
        loseText = GameObject.Find("/Canvas/LoseText").GetComponent<Text>();

		// makes the ball lighter
		rb.mass = .5f;
		rb.drag = .5f;
		rb.angularDrag = .3f;

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
		if (lifeCounter > 0) {
			lifeCounter--;
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
		if (count % 100 == 0) {
			lifeCounter++;
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