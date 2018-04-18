using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

	public Object nextScene;
	public Button button;

	// Use this for initialization
	void Start () {
		Button b = button.GetComponent<Button>();
		b.onClick.AddListener(NextScene);
	}
	
	void NextScene ()
	{
		SceneManager.LoadScene(nextScene.name);
	}
}
