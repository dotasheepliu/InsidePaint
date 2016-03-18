using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class enlarge : MonoBehaviour {
	public GameObject room;
	public AudioSource audios;
	// Use this for initialization
	public bool trig = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transform.localPosition.z);

		if ((transform.localPosition.z > 7f) && (transform.localPosition.x < -0.6f) && trig == false) {
			trig = true;
			StartCoroutine (Goodasdasd ());
		}
		audios.volume = 1f;
	}

	IEnumerator Goodasdasd () {
		for (int i = 1; i < 80; i++) {
			yield return new WaitForSeconds(0.01f);
			room.transform.localScale = new Vector3 (room.transform.localScale.x*1.01f, room.transform.localScale.y, room.transform.localScale.z*1.01f);
		}
	
			SceneManager.LoadScene (1);
	
	}
}
