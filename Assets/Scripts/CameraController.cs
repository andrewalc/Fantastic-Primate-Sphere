using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float vertical_offset;
	//public GameObject ground;

	private Vector3 p_offset;
	//private Vector3 g_offset;
	// Use this for initialization
	void Start () {
		p_offset = transform.position - player.transform.position;
		//g_offset = transform.rotation - ground.transform.rotation;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + p_offset;
		transform.position = transform.position + new Vector3 (0, vertical_offset, 0);
		//transform.rotation = ground.transform.rotation + g_offset;
	}
}
