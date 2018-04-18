using UnityEngine;
 
// By Denvery https://answers.unity.com/questions/1260393/make-music-continue-playing-through-scenes.html
public class MusicClass : MonoBehaviour
{
	private AudioSource _audioSource;
	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
		_audioSource = GetComponent<AudioSource>();
	}
 
	public void PlayMusic()
	{
		if (_audioSource.isPlaying) return;
		_audioSource.Play();
	}
 
	public void StopMusic()
	{
		_audioSource.Stop();
	}
}