using UnityEngine;
using System.Collections;

public class WorldTilt : MonoBehaviour
{

    public float speed;
    public GameObject player;
    private float prevHorizontal;
    private float prevVertical;

    private float maxRot = 20;
    private Vector3 prevRotation;
    private Vector3 initPosition;
    private Vector3 prevPosition;

    // Use this for initialization
    void Start()
    {
        prevHorizontal = 0;
        prevVertical = 0;
        prevPosition = Vector3.zero;
        prevRotation = Vector3.zero;
        initPosition = transform.position;
    }

    private void Update()
    {
        /*Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);

        Vector3 targetPosition = Vector3.MoveTowards(transform.position, initPosition, Time.deltaTime * 2);
        transform.position = targetPosition;

        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, maxRot);
        targetRotation.ToAngleAxis(out angle, out axis);
        this.transform.RotateAround(playerPos, axis, -angle);
        */

	
        Vector3 playerPos = new Vector3 (player.transform.position.x, 0, player.transform.position.z);
		float moveHorizontal;
		float moveVertical;
        if (!player.GetComponent<PlayerController>().gameOver)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && (prevHorizontal != 0 || prevVertical != 0))
            {
                print(prevHorizontal + ", " + prevVertical);
                moveHorizontal = prevHorizontal;
                moveVertical = prevVertical;
                this.transform.RotateAround(player.transform.position, Vector3.left, speed * moveHorizontal * Time.deltaTime);
                this.transform.RotateAround(player.transform.position, Vector3.back, speed * moveVertical * Time.deltaTime);
                print(moveHorizontal + ", " + moveVertical);
            }
            else
            {
                // Get horizontal and vertical forces from Input
                moveHorizontal = Input.GetAxis("Horizontal");
                moveVertical = Input.GetAxis("Vertical");

                //this.transform.RotateAround(player.transform.position, player.transform.right, speed * moveHorizontal * Time.deltaTime);
                //this.transform.RotateAround(player.transform.position, player.transform.forward, speed * moveVertical * Time.deltaTime);

                this.transform.RotateAround(player.transform.position, Vector3.right, speed * moveHorizontal * Time.deltaTime);
                this.transform.RotateAround(player.transform.position, Vector3.forward, speed * moveVertical * Time.deltaTime);


            }



            // The final movement vector before applying it to the player
            //Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical) * speed * Time.deltaTime;
            //this.transform.Rotate(speed * moveHorizontal * Time.deltaTime, 0, speed * moveVertical *  Time.deltaTime, Space.Self);
            //this.transform.localRotation = new Quaternion (speed * moveHorizontal * Time.deltaTime, 0, speed * moveVertical *  Time.deltaTime , 1);


            prevHorizontal = Mathf.Abs(moveHorizontal);
            prevVertical = Mathf.Abs(moveVertical);
            // Allow movement as long as the game isn't over
            //this.transform.Rotate (movement);
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);

        float moveHorizontal;
        float moveVertical;
        if (!player.GetComponent<PlayerController>().gameOver)
        {
            // Get horizontal and vertical forces from Input
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            this.transform.RotateAround(player.transform.position, Vector3.right, speed * moveHorizontal * Time.deltaTime);
            this.transform.RotateAround(player.transform.position, Vector3.forward, speed * moveVertical * Time.deltaTime);

            // Record previous info
            prevHorizontal = moveHorizontal;
            prevVertical = moveVertical;
            prevRotation = transform.rotation.eulerAngles;
            prevPosition = playerPos;
        }
    }
}
