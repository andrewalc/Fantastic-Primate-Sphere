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
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);

        Vector3 targetPosition = Vector3.MoveTowards(transform.position, initPosition, Time.deltaTime * 2);
        transform.position = targetPosition;

        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, maxRot);
        targetRotation.ToAngleAxis(out angle, out axis);
        this.transform.RotateAround(playerPos, axis, -angle);

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
            prevPosition = playerPos;
        }
    }
}
