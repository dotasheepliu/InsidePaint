using UnityEngine;
using System.Collections;

public class TransformVincentStar : MonoBehaviour {
	private bool isTransformVincentStar = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isTransformVincentStar) {
			this.transform.localScale = Vector3.Lerp (this.transform.localScale, new Vector3 (7.0f, 7.0f, 7.0f), 
				Time.deltaTime * 2.0f);
		}
	}

	public bool transformVincentStar() {
		isTransformVincentStar = true;
		return isTransformVincentStar;
	}
}
