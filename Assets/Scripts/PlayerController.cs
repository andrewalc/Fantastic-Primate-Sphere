using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int timeForLevel;

    float timer = 30;
    float gameRestartTimer = 3;

    public string nextLevel;
    public bool gameOver;
    public bool gameWin;
    public bool gameStart;

    private Rigidbody rb;

    private float count; // score
    private Text countText;
    private Text winText;
    private Text TimerText;
    private Text loseText;

    private AudioSource[] sounds;
    private AudioSource beepSound;
    private AudioSource doubleBeepSound;
    private Animation animation;
    private float POINT_PICKUP = 100;
    private float POINT_SECOND_MULTIPLIER = 100;
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
        gameWin = false; // Is the game won?

        count = 0; // The number of pickups the player has

        // Set up the UI text
        countText = GameObject.Find("/Canvas/CountText").GetComponent<Text>();
        winText = GameObject.Find("/Canvas/WinText").GetComponent<Text>();
        TimerText = GameObject.Find("/Canvas/TimerText").GetComponent<Text>();
        loseText = GameObject.Find("/Canvas/LoseText").GetComponent<Text>();

        // makes the ball lighter
        rb.mass = .5f;
        rb.drag = .5f;
        rb.angularDrag = .3f;

        SetStartingText();
    }

    // Update is called once per frame

    private void Update()
    {
        // The player/game wants to restart the level
        if (Input.GetKeyDown("joystick 1 button " + 7) || Input.GetKeyDown("r") || gameRestartTimer <= 0)
        {
            SceneManager.LoadScene("Scenes/" + SceneManager.GetActiveScene().name);
        }

        // The game has started and not yet ended
        if (gameStart && !gameOver)
        {
            UpdateTime();
        }
        // The game has ended
        else if (gameStart)
        {
            // The player won
            if (gameWin)
            {
                winText.text = "You Win!";
            }
            // The player lost
            else
            {
                loseText.text = "You lose!" + "\n" + "Press start or R to restart or wait 3 seconds!";
                gameRestartTimer -= Time.deltaTime;
            }
        }

        // Flying up animation when the goal is hit
        if (flyUp)
        {
            rb.velocity += Vector3.up * 4;
        }
    }

    public void StartGame()
    {
        gameStart = true;
    }

    // This is for the flying up after hitting a goal
    public void StartFlyingUp()
    {
        flyUp = true;
        GetComponent<ParticleSystem>().Play();
    }

    // On collision with an Enemy, the player loses, on collision with the goal, the player moves on to the next level
    void OnCollisionEnter(Collision col)
    {
        // Hit a killplane
        if (!gameOver && col.gameObject.tag == "Enemy")
        {
            gameOver = true;
        }

        // Hit the goal
        if (!gameOver && col.gameObject.tag == "Goal")
        {
            // Lock the time
            SetTimerText();
            // Add and lock the score
            count += (int) (timer * POINT_SECOND_MULTIPLIER);
            SetCountText();
            // End the game with a win
            gameOver = true;
            gameWin = true;
            GameObject.FindGameObjectWithTag("Goal").GetComponent<ParticleSystem>().Play();
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("EndGoalCam");
        }
    }

    public void LoadNextLevel()
    {
        //SceneManager.LoadScene("Scenes/" + nextLevel); Can use later
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene("Scenes/" + "Menu");
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // On trigger with a pick up, increase the score and deactivate the pick up
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += POINT_PICKUP;
            SetCountText();
        }
    }

    void SetStartingText()
    {
        SetCountText();
        SetTimerText();
        winText.text = "";
        loseText.text = "";
    }

    // Set the count
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }

    // Set the timer
    void SetTimerText()
    {
        float seconds = Mathf.Floor(timer);
        float milliseconds = (timer * 100) % 100;
        TimerText.text = string.Format("~TIME~" + "\n" + "{0}:{1:D2}", (int) seconds, (int) milliseconds);
    }

    void UpdateTime()
    {
        var nearestSecond = Mathf.Floor(timer);

        timer -= Time.deltaTime;
        var seconds = Mathf.Floor(timer);

        // if the second changed
        if (seconds < nearestSecond)
        {
            beepSound.Play();

            // if we're also low on time
            if (seconds <= 10)
            {
                doubleBeepSound.Play();
                TimerText.color = Color.red;
            }
        }

        // if lost
        if (timer <= 0.0f)
        {
            timer = 0.0f;
            gameOver = true;
        }

        SetTimerText();
    }
}