using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float vertical_offset;

    public Text StageText;

    public string StageName;
    //public GameObject ground;

    private Vector3 p_offset;

    private float fadeOutTime = 1.0f;
    private float fadeInTime = 0.3f;

    //private Vector3 g_offset;
    // Use this for initialization
    void Start()
    {
        p_offset = transform.position - player.transform.position;
        //g_offset = transform.rotation - ground.transform.rotation;
        StageText.text = StageName;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + p_offset;
        transform.position = transform.position + new Vector3(0, vertical_offset, 0);
        //transform.rotation = ground.transform.rotation + g_offset;
    }

    private IEnumerator FadeOutTextRoutine()
    {
        Color originalColor = StageText.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            StageText.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }

    private IEnumerator FadeInTextRoutine()
    {
        Color originalColor = StageText.color;
        for (float t = 0.01f; t < fadeInTime; t += Time.deltaTime)
        {
            StageText.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t / fadeInTime));
            yield return null;
        }

    }
}