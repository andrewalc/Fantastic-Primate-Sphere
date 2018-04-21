using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class l6_platform_r : MonoBehaviour
{
    public float speed;
    public float time;
    private bool moveright;
    private bool keepMoving = true;

    private void Start()
    {
        StartCoroutine(moveswitch());
    }

    IEnumerator moveswitch()
    {
        while (keepMoving)
        {
            if (moveright)
            {
                moveright = false;
            }
            else
            {
                moveright = true;
            }

            yield return new WaitForSeconds(time);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveright)
        {
            transform.Translate(0, 0, speed);
        }
        else
        {
            transform.Translate(0, 0, -speed);
        }

    }
}