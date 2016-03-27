using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CardboardAudioSource))]
public class CuratorAudio : MonoBehaviour {
	public AudioClip audioClip1;
	public AudioClip audioClip2;
	private CardboardAudioSource audioSource;
	private bool haveBothClipsBeenPlayed = false;
	private bool hasFirstClipBeenPlayed = false;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<CardboardAudioSource> ();
		audioSource.volume = 1.0f;
		audioSource.rolloffMode = AudioRolloffMode.Linear;
		audioSource.minDistance = 1.0f;
		audioSource.maxDistance = 500.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!BedroomScene.isPaintingComplete) {
			if (!haveBothClipsBeenPlayed) {
				if (!hasFirstClipBeenPlayed) {
					audioSource.clip = audioClip1;
					audioSource.Play ();
					hasFirstClipBeenPlayed = true;
					Debug.Log ("Playing now");
				}
				if (!audioSource.isPlaying) {
					audioSource.clip = audioClip2;
					audioSource.Play ();
					haveBothClipsBeenPlayed = true;
				}
			}
		} else {
			
		}
	}
}
