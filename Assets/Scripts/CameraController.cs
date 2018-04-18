using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float vertical_offset;

    public Text StageText;
    public Text ReadyText;
    public Text GoText;
    private Color GoText_Color;
    private Color ReadyText_Color;
    private Color StageText_Color;

    public string StageName;
    //public GameObject ground;

    private Vector3 p_offset;

    private float ST_FOT = 1.0f;
    private float ST_FIT = 0.3f;

    private float RT_FOT = 0.2f;
    private float RT_FIT = 0.1f;

    private float GT_FOT = 0.1f;
    private float GT_FIT = 0.1f;

    //private Vector3 g_offset;
    // Use this for initialization
    void Start()
    {
        p_offset = transform.position - player.transform.position;
        //g_offset = transform.rotation - ground.transform.rotation;
        StageText_Color = StageText.color;
        ReadyText_Color = ReadyText.color;
        GoText_Color = GoText.color;

        StageText.color = Color.clear;
        ReadyText.color = Color.clear;
        GoText.color = Color.clear;

        StageText.text = StageName;
        ReadyText.text = "Ready?";
        GoText.text = "GO!";
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + p_offset;
        transform.position = transform.position + new Vector3(0, vertical_offset, 0);
        //transform.rotation = ground.transform.rotation + g_offset;
    }

    void StartPlayerGame()
    {
        player.GetComponent<PlayerController>().StartGame();
    }
    
    

    private IEnumerator StageTextFadeOut()
    {
        Color originalColor = StageText_Color;
        for (float t = 0.01f; t < ST_FOT; t += Time.deltaTime)
        {
            StageText.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / ST_FOT));
            yield return null;
        }
    }

    private IEnumerator StageTextFadeIn()
    {
        Color originalColor = StageText_Color;
        for (float t = 0.01f; t < ST_FIT; t += Time.deltaTime)
        {
            StageText.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t / ST_FIT));
            yield return null;
        }
    }

    private IEnumerator ReadyTextFadeOut()
    {
        Color originalColor = ReadyText_Color;
        for (float t = 0.01f; t < RT_FOT; t += Time.deltaTime)
        {
            ReadyText.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / RT_FOT));
            yield return null;
        }
    }

    private IEnumerator ReadyTextFadeIn()
    {
        Color originalColor = ReadyText_Color;
        for (float t = 0.01f; t < RT_FIT; t += Time.deltaTime)
        {
            ReadyText.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t / RT_FIT));
            yield return null;
        }
    }

    private IEnumerator GoTextFadeOut()
    {
        Color originalColor = GoText_Color;
        for (float t = 0.01f; t < GT_FOT; t += Time.deltaTime)
        {
            GoText.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / GT_FOT));
            yield return null;
        }
    }

    private IEnumerator GoTextFadeIn()
    {
        Color originalColor = GoText_Color;
        for (float t = 0.01f; t < GT_FIT; t += Time.deltaTime)
        {
            GoText.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t / GT_FIT));
            yield return null;
        }
    }
}