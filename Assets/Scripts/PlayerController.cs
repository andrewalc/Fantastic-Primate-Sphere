using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int timeForLevel;

    float minutes = 0;
    float seconds = 30;
    float miliseconds = 0;

    public string nextLevel;
    public bool gameOver;
    public bool gameStart;
    public bool TimeOver;

    private Rigidbody rb;

    private float count;
    private int lifeCounter;
    private Text countText;
    private Text winText;
    private Text TimerText;
    private Text loseText;
    private Text lifeText;

    private AudioSource[] sounds;
    private AudioSource beepSound;
    private AudioSource doubleBeepSound;
    private Animation animation;
    private float POINT_SECOND_MULTIPLIER = 100;
    private float POINT_MILLISECOND_MULTIPLIER = 10;
    private bool flyUp;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sounds = GetComponents<AudioSource>();
        animation = GetComponent<Animation>();

        doubleBeepSound = sounds[0];
        beepSound = sounds[1];

        gameStart = false; // Has the game started?
        gameOver = false; // Is the game over?
        TimeOver = false; // Has time run out?

        count = 0; // The number of pickups the player has
        lifeCounter = 2;

        // Set up the UI text
        countText = GameObject.Find("/Canvas/CountText").GetComponent<Text>();
        winText = GameObject.Find("/Canvas/WinText").GetComponent<Text>();
        TimerText = GameObject.Find("/Canvas/TimerText").GetComponent<Text>();
        loseText = GameObject.Find("/Canvas/LoseText").GetComponent<Text>();

        // makes the ball lighter
        rb.mass = .5f;
        rb.drag = .5f;
        rb.angularDrag = .3f;

        SetCountText();
        SetTimerText();


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

    private void Update()
    {
        if (flyUp)
        {
            rb.velocity += Vector3.up * 4;
        }

        if (gameStart && !gameOver)
        {
            if (miliseconds <= 0)
            {
                if (seconds <= 0)
                {
                    minutes--;
                    seconds = 59;
                }
                else if (seconds >= 0)
                {
                    seconds--;
                    beepSound.Play();
                    if (minutes == 0.0f && seconds <= 10.0f)
                    {
                        doubleBeepSound.Play();
                        TimerText.color = Color.red;
                    }
                }

                miliseconds = 100;
            }

            if (minutes <= 0 && seconds <= 0 && miliseconds <= 0)
            {
                TimeOver = true;
                gameOver = true;
            }

            miliseconds -= Time.deltaTime * 100;
        }else if(TimeOver)
            {
                if (lifeCounter >= 0)
                {
                    SceneManager.LoadScene("Scenes/" + SceneManager.GetActiveScene().name);
                }
                else
                {
                    loseText.text = "You lose!";
                }
            }
        
        //Debug.Log(string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds));
        SetTimerText();
    }

    public void StartGame()
    {
        gameStart = true;
    }

    public void StartFlyingUp()
    {
        flyUp = true;
    }

    // On collision with an Enemy, the player loses, on collision with the goal, the player moves on to the next level
    void OnCollisionEnter(Collision col)
    {
        if (!gameOver && !TimeOver && col.gameObject.tag == "Goal")
        {
            winText.text = "You Win!";
            gameOver = true;
            count += seconds * POINT_SECOND_MULTIPLIER;
            count += miliseconds * POINT_MILLISECOND_MULTIPLIER;
            GameObject.FindGameObjectWithTag("Goal").GetComponent<ParticleSystem>().Play();
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("EndGoalCam");
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Scenes/" + nextLevel);
    }

    // On trigger with a pick up, increase the score and deactivate the pick up
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (count % 100 == 0)
        {
            lifeCounter++;
        }
    }

    // Set the count
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (!gameOver && count >= 9)
        {
            winText.text = "You Win!";
            gameOver = true;
        }
    }

    // Set the timer
    void SetTimerText()
    {
        TimerText.text = string.Format("~TIME~" + "\n" + "{1:00}:{2:00}", minutes, seconds, (int) miliseconds);
    }
}