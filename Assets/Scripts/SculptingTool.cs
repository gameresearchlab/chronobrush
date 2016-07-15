using UnityEngine;
using System.Collections;

public class SculptingTool : MonoBehaviour {

	public GameObject prefab;

	// Use this for initialization
	void Start () {
	
		prefab = this.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if(RotateInterface.getState() == RotateInterface.LEFT_CLICK || RotateInterface.getState() == RotateInterface.RIGHT_CLICK)
		{

			Instantiate(prefab, this.transform.position, Quaternion.identity);

		}
	
	}
}
