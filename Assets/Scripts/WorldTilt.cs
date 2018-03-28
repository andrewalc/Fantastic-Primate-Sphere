using UnityEngine;
using System.Collections;

public class WorldTilt : MonoBehaviour
{

    public float speed;
    public float decay_step;
    public GameObject player;
    private float prevHorizontal;
    private float prevVertical;

    private Vector3 prevRotation;
    private Vector3 prevPostition;

    // Use this for initialization
    void Start()
    {
        prevHorizontal = 0;
        prevVertical = 0;
        prevPostition = Vector3.zero;
        prevRotation = Vector3.zero;
    }

    private void Update()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, decay_step);

        /*
        // Resetting the previous rotation
        this.transform.RotateAround(prevPostition, Vector3.right * -prevHorizontal, speed * Time.deltaTime);
        this.transform.RotateAround(prevPostition, Vector3.forward * -prevVertical, speed * Time.deltaTime);
        this.transform.RotateAround(playerPos, Vector3.right * prevHorizontal, speed * Time.deltaTime);
        this.transform.RotateAround(playerPos, Vector3.forward * prevVertical, speed * Time.deltaTime);
        */
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
            prevPostition = playerPos;
        }
    }
}
