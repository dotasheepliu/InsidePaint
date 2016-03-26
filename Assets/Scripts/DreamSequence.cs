using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DreamSequence : MonoBehaviour {
	private float waitBeforeSequenceStarts = 1.2f;
	private bool isDreamStartAudioPlayed = false;
	public LayerMask layerMask;
	private bool isMoonTransformed = false;
	private bool isStarTransformed;
	private GameObject[] gameObjectsForTransform;
	public Dictionary<string, GameObject> starsInVincentVisionReference;

	// Use this for initialization
	void Start () {
		gameObjectsForTransform = GameObject.FindGameObjectsWithTag ("ForVincentVision");
		GameObject[] starsInVincentVision = GameObject.FindGameObjectsWithTag ("VincentVisionStars");
		starsInVincentVisionReference = new Dictionary<string, GameObject> ();
		foreach (GameObject gameObject in starsInVincentVision) {
			starsInVincentVisionReference.Add (gameObject.name, gameObject);
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
				Debug.Log ("Whoa! Where am I? Is this a dream?"); //TODO: Play audio here.
				isDreamStartAudioPlayed = true;
			}
		}

		if (isDreamStartAudioPlayed) {
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
						if (starAngle <= 5.0f) {
							Debug.Log ("The Star!"); //TODO: Play Audio here.
							isStarTransformed = gameObject.GetComponentInChildren<TransformStars>().transformStar();
							if (isStarTransformed) {
								gameObject.tag = "InVincentVision";
							}
						}
					}
				}
			}
		}
	}
}
