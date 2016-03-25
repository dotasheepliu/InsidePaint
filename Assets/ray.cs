using UnityEngine;
using System.Collections;

public class ray : MonoBehaviour {
	public GameObject brushtable;
	public GameObject brushcam;
	private GameObject objectInHand;
	private int canvcount = 0;
	// Use this for initialization
	public LayerMask layerMask;
	public bool isbrushtake = false;
	public GameObject p0;
	public GameObject p;
	public GameObject p1;
	public GameObject p2;
	public GameObject p3;
	public GameObject p4;
	public GameObject p5;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
				pickObject();
			//Debug.Log("Fire1");
				}
	}




	void pickObject() {
		RaycastHit hitInfo;
		objectInHand = null;
		if (Physics.Raycast (Cardboard.SDK.GetComponentInChildren<CardboardHead> ().Gaze, out hitInfo, Mathf.Infinity, layerMask)) {
			Debug.Log ("Hit something " + hitInfo.transform.name);
			if (hitInfo.transform.gameObject.name == "Brush 2") {
				if (!isbrushtake) {
					brushtable.SetActive (false);
					brushcam.SetActive (true);
					isbrushtake = true;
				}
			}
			if (hitInfo.transform.gameObject.tag == "canv") {
				if (isbrushtake) {
					switch (canvcount) {
					case 0:
						p0.SetActive (false);
						p.SetActive (true);
						canvcount++;
						break;
					case 1:
						p.SetActive (false);
						p1.SetActive (true);
						canvcount++;
						break;
					case 2:
						p1.SetActive (false);
						p2.SetActive (true);
						canvcount++;
						break;
					case 3:
						p2.SetActive (false);
						p3.SetActive (true);
						canvcount++;
						break;
					case 4:
						p3.SetActive (false);
						p4.SetActive (true);
						canvcount++;
						break;
					case 5:
						p4.SetActive (false);
						p5.SetActive (true);
						canvcount++;
						break;
					case 6:
						brushtable.SetActive (true);
						brushcam.SetActive (false);
						canvcount++;
						break;
					default:
						break;
					}
				}

			}	
		} else {
			Debug.Log("Did not hit");
		}
			
}
}