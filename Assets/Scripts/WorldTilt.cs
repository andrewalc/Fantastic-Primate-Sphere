using UnityEngine;
using System.Collections;

public class WorldTilt : MonoBehaviour {

	public float speed;
	public float decay;
	public float smooth_thresh;
	public GameObject player;
	public GameObject ground;
	private float prevHorizontal;
	private float prevVertical;

	// Use this for initialization
	void Start () {
		prevHorizontal = 0;
		prevVertical = 0;
	}



    // Update is called once per frame
    void FixedUpdate () {
		Vector3 playerPos = new Vector3 (player.transform.position.x, 0, player.transform.position.z);
		float moveHorizontal;
		float moveVertical;
        if (!player.GetComponent<PlayerController>().gameOver)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && (prevHorizontal != 0 || prevVertical != 0))
            {
                print(prevHorizontal + ", " + prevVertical);
                //if (Mathf.Abs (prevHorizontal) < smooth_thresh) {
                //	prevHorizontal = 0;
                //}
                //if (Mathf.Abs (prevVertical) < smooth_thresh) {
                //	prevVertical = 0;
                //}
                moveHorizontal = prevHorizontal * decay;
                moveVertical = prevVertical * decay;
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
}
