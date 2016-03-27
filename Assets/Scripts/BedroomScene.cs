using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BedroomScene : MonoBehaviour {
	public LayerMask layerMask;
	private bool hasIntroAudioBeenPlayed = false;
	private AudioSource audioSource;
	private bool hasFurnitureAudioBeenPlayed = false;
	private bool shouldStartDreamSequence = false;
	private bool hasWallAudioBeenPlayed = false;
	private bool hasPerspectiveAudioBeenPlayed = false;
	public static bool isDreamComplete = false;
	public static bool isPaintingComplete = false;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;
		if (!isDreamComplete) {
			if (!hasIntroAudioBeenPlayed) {
				Debug.Log ("Ahh! Home! This is the only place where I can find peace and comfort. " +
				"When I painted this room on canvas, I made sure that color was doing everything."); //TODO: Play Audio
				hasIntroAudioBeenPlayed = true;
			}
		} else {
			if (!hasIntroAudioBeenPlayed) {
				Debug.Log ("Huh! I should put what I saw onto the canvas.");
				hasIntroAudioBeenPlayed = true;
			}
		}

		if (Input.GetButtonDown ("Fire1")) {
			if (!isDreamComplete) {
				if (!hasIntroAudioBeenPlayed) {
					Debug.Log ("Ahh! Home! This is the only place where I can find peace and comfort. " +
						"When I painted this room, I made sure that color was doing everything."); //TODO: Play Audio
					hasIntroAudioBeenPlayed = true;
				} else {
					if (Physics.Raycast (Cardboard.SDK.GetComponentInChildren<CardboardHead> ().Gaze, out hitInfo, Mathf.Infinity, layerMask)) {
						GameObject hitObject = hitInfo.transform.gameObject;
						Debug.Log ("hitObject :: " + hitObject.name);
						if (hitObject.name.Contains ("bad")) {
							if (!hasFurnitureAudioBeenPlayed) {
								Debug.Log ("I have painted the wood of the furnitures in fresh and warm yellow to express a sense of rest." +
									"Come to think of it, I should sleep for sometime."); //TODO: Play Audio.
								hasFurnitureAudioBeenPlayed = true;
								shouldStartDreamSequence = true;
							} else {
								Debug.Log ("I should sleep for sometime."); //TODO: Play Audio.
								shouldStartDreamSequence = true;
							}
						} else if (hitObject.name.Contains ("chair")) {
							if (!hasFurnitureAudioBeenPlayed) {
								Debug.Log ("I have painted the wood of the furnitures in fresh and warm yellow to express a sense of rest."); //TODO: Play Audio.
								hasFurnitureAudioBeenPlayed = true;
							} else {
								Debug.Log ("I should sleep for sometime."); //TODO: Play Audio.
							}
						} else if (hitObject.name.Contains ("walls")) {
							if (!hasWallAudioBeenPlayed) {
								Debug.Log ("The walls are pale blue to evoke a sense of inviolable calmness."); //TODO: Play Audio.
								hasWallAudioBeenPlayed = true;
							} else {
								Debug.Log ("I should sleep for sometime."); //TODO: Play Audio.
							}
						} else {
							if (!hasPerspectiveAudioBeenPlayed) {
								Debug.Log ("I have painted the bedroom in a skewed perspective so as to put the viewer in the room."); //TODO: Play Audio.
								hasPerspectiveAudioBeenPlayed = true;
							} else {
								Debug.Log ("I should sleep for sometime."); //TODO: Play Audio.
							}
						}
					}
				}
			} else {
				if (!hasIntroAudioBeenPlayed) {
					Debug.Log ("Huh! I should put what I saw onto the canvas."); //TODO: Play Audio here.
					hasIntroAudioBeenPlayed = true;
				}
			}

		}

		if(shouldStartDreamSequence) {
			RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
			SceneManager.LoadSceneAsync (2);
		}

		if(isPaintingComplete) {
			SceneManager.LoadSceneAsync (0);
		}
	}
}
