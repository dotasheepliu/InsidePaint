using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DreamSequence : MonoBehaviour {
	private float waitBeforeSequenceStarts = 1.2f;
	private bool isDreamStartAudioPlayed = false;
	public LayerMask layerMask;
	private bool isMoonTransformed = false;
	private bool isStarTransformed;
	private GameObject[] gameObjectsForTransform;
	public Dictionary<string, GameObject> starsInVincentVisionReference;
	private bool shouldStartGraveSequence = false;
	private GameObject[] graveSequenceGameObjects;
	private GameObject[] graveSequenceGameObjectsReference;
	private bool shouldWakeUp = false;
	public Material secondSkyboxMaterial;
	private float blend = 0.0f;
	public GameObject vincentFriend;
	public GameObject gravestone;
	public GameObject thePlayer;
	private bool hasSkyMonologueBeenPlayed = false;
	private bool hasDepressionClipBeenPlayed = false;

	// Use this for initialization
	void Start () {
		gameObjectsForTransform = GameObject.FindGameObjectsWithTag ("ForVincentVision");
		GameObject[] starsInVincentVision = GameObject.FindGameObjectsWithTag ("VincentVisionStars");
		starsInVincentVisionReference = new Dictionary<string, GameObject> ();
		foreach (GameObject gameObject in starsInVincentVision) {
			starsInVincentVisionReference.Add (gameObject.name, gameObject);
			gameObject.SetActive (false);
		}

		graveSequenceGameObjects = GameObject.FindGameObjectsWithTag ("GraveDream");
		graveSequenceGameObjectsReference = graveSequenceGameObjects;
		foreach (GameObject gameObject in graveSequenceGameObjects) {
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;
		if (waitBeforeSequenceStarts > 0) {
			waitBeforeSequenceStarts -= Time.deltaTime;
		} else {
			if (!isDreamStartAudioPlayed) {
				Debug.Log ("Whoa! Where am I? Is this a dream? Dr. Gachet, is that you?"); //TODO: Play audio here.
				isDreamStartAudioPlayed = true;
			}
		}

		if (isDreamStartAudioPlayed && !shouldStartGraveSequence) {
			Debug.Log ("Yes Vincent, my friend! Look at the beautiful night sky above you!"); //TODO: Play audio here.
			Vector3 lookDirection = Cardboard.SDK.GetComponentInChildren<CardboardHead> ().Gaze.direction;
			foreach (GameObject gameObject in gameObjectsForTransform) {
				if (gameObject.tag == "ForVincentVision") {

					//Transform moon
					if (gameObject.name == "crescent moon" && !isMoonTransformed) {
						Vector3 moonDirection = gameObject.transform.position - transform.position;
						float moonAngle = Vector3.Angle (moonDirection, lookDirection);
						if (moonAngle <= 5.0f) {
							Debug.Log ("The moon!"); //TODO: Play Audio here.
							isMoonTransformed = gameObject.GetComponentInChildren<TransformMoon> ().transformMoon ();
							if (isMoonTransformed) {
								gameObject.tag = "InVincentVision";
							}
						}
					}

					//Transform Stars
					if (gameObject.name.Contains ("Star")) {
						Vector3 starDirection = gameObject.transform.position - transform.position;
						float starAngle = Vector3.Angle (starDirection, lookDirection);
						if (starAngle <= 10.0f) {
							Debug.Log ("The Star!"); //TODO: Play Audio here.
							isStarTransformed = gameObject.GetComponentInChildren<TransformStars>().transformStar();
							if (isStarTransformed) {
								gameObject.tag = "InVincentVision";
							}
							if(!hasSkyMonologueBeenPlayed) {
								Debug.Log ("I see. I see things differently. The sky appears calm and blue. But the stars! " +
									"Can you see how they roll their light and energy through the sky?"); //TODO: Play Audio here.
								hasSkyMonologueBeenPlayed = true;
								shouldStartGraveSequence = true;
							}
						}
					}
				}
			}
		}

		if (shouldStartGraveSequence && !shouldWakeUp) {
			GameObject[] skyDreamObjects = GameObject.FindGameObjectsWithTag ("SkyDream");
			foreach (GameObject gameObject in skyDreamObjects) {
				gameObject.transform.Translate (0, Time.deltaTime * -15.0f, 0, Space.Self);
			}

			foreach (GameObject gameObject in graveSequenceGameObjectsReference) {
				gameObject.SetActive (true);
			}

			float distanceBetweenPlayerAndSarcophagus = Vector3.Distance (thePlayer.transform.position, gravestone.transform.position);
			if (distanceBetweenPlayerAndSarcophagus <= 6.0f && !hasDepressionClipBeenPlayed) {
				Debug.Log ("(Sigh) I know its going to end! I know I can do nothing about my illness. " +
					"Suffering through it is exhausting me. I see no happy future at all!"); //TODO: Play Audio here.
				hasDepressionClipBeenPlayed = true;
				shouldWakeUp = true;
			}
		}

		if (shouldWakeUp) {
			foreach (GameObject gameObject in graveSequenceGameObjectsReference) {
				if(gameObject != null) {
					gameObject.transform.Translate (0, Time.deltaTime * -15.0f, 0, Space.Self);
				}
			}

			GameObject[] celestialObjects = GameObject.FindGameObjectsWithTag ("InVincentVision");
			foreach (GameObject gameObject in celestialObjects) {
				Destroy (gameObject);
			}

			if (blend <= 0.5f) {
				secondSkyboxMaterial.SetFloat ("_Blend", blend);
				RenderSettings.skybox = secondSkyboxMaterial;
			}
			blend += Time.deltaTime * 0.1f;

			if(blend >= 0.8f) {
				BedroomScene.isDreamComplete = true;
				SceneManager.LoadSceneAsync (1);
			}
		}
	}
}
