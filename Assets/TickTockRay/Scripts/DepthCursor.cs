using UnityEngine;
using System.Collections;

public class DepthCursor : MonoBehaviour {

	private Quaternion correction;
	public float zLower;
	public float zUpper;
	public float depthLower;
	public float depthUpper;


	// Use this for initialization
	void Start () {
	
		correction = Quaternion.identity;
		zLower = 50;
		zUpper = 290;
		depthLower = 1;
		depthUpper = 4;
	} 
	
	// Update is called once per frame
	void Update () {


		float z = GameObject.Find("Watch").transform.localRotation.eulerAngles.z;

		float depth;

		if(0 <= z && z <= 50)
		{
			depth = map(z, zLower, 0, depthLower,depthUpper / 2);

		}else if(290 <= z && z <= 360)
		{
			depth = map(z, 360, zUpper, depthUpper / 2, depthUpper);
		}else{
			return;
		}


		this.transform.position = this.transform.forward * depth;

		Debug.DrawRay(this.transform.position, this.transform.forward);

		TextMesh debug = GameObject.Find("Debug").GetComponent<TextMesh>();

		debug.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", this.transform.position.x, this.transform.position.y, this.transform.position.z, this.transform.forward.x, this.transform.forward.y, this.transform.forward.z);
	
	}


	float map(float value, float inRangeL, float inRangeH, float outRangeL, float outRangeH){

		return ((value - inRangeL) / (inRangeH - inRangeL)) * (outRangeH - outRangeL) + outRangeL;

	}



}
