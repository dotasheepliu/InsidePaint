using UnityEngine;
using System.Collections;

public class DreamSequence : MonoBehaviour {
	private float waitBeforeSequenceStarts = 1.2f;
	private bool isDreamStartAudioPlayed = false;
	public LayerMask layerMask;
	public GameObject moon;

	// Use this for initialization
	void Start () {
	
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
			//Transform Moon
			Vector3 moonDirection = moon.transform.position - transform.position;
			if(Physics.Raycast(Cardboard.SDK.GetComponentInChildren<CardboardHead> ().Gaze, out hitInfo, 99999, layerMask)) {
				Debug.Log ("Raycast hit :: " + hitInfo.transform.gameObject.name);
				Vector3 lookDirection = Cardboard.SDK.GetComponentInChildren<CardboardHead> ().Gaze.direction;
				float moonAngle = Vector3.Angle (moonDirection, lookDirection);
				if (moonAngle <= 5.0f) {
					Debug.Log ("The moon!");
				}
			}
		}
	}
}
