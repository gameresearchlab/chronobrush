using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DepthCursor : MonoBehaviour {

	private Quaternion correction;
	public float zLower;
	public float zUpper;
	public float depthLower;
	public float depthUpper;
	private List<float> accHistory;


	// Use this for initialization
	void Start () {
	
		correction = Quaternion.identity;
		zLower = 50;
		zUpper = 290;
		depthLower = 1;
		depthUpper = 4;

		accHistory = new List<float>();
	} 
	TextMesh debug;
	// Update is called once per frame
	void Update () {


		debug = GameObject.Find("Debug").GetComponent<TextMesh>();

		accHistory.Add(WatchRotationJNI.accZ);
		if(accHistory.Count > 8){
			accHistory.RemoveAt(0);
		}

		useToggleMethod();


	
	}
		
	int numFrames = 0;

	bool fRise = false;
	bool fZero = false;
	bool fFall = false;
	bool fToggle = false;


	void useToggleMethod()
	{
		float acc = smooth(accHistory);

		if(numFrames > 60)
		{
			fRise = fRise = fZero = fFall = false;
			numFrames = 0;
		}
		if(acc > 1.9f)
			fRise = true;
		if(fRise && (-0.5f < acc && acc < 0.5f))
			fZero = true;
		if(fZero && acc < -1.9f)
			fFall = true;
		if(fFall && (-0.5f < acc && acc < 0.5f))
		{
			fToggle = true;
			fRise = fZero = fFall = false;
		}
		if(fToggle && acc < -1.9f)
			fToggle = false;

		if(fRise || fZero || fFall)
			numFrames++;

		if(fToggle)
			this.transform.position = this.transform.forward * 0.02f;
			
	}



	private int framesToWait = 16;
	private int framesWaited = 0;
	private float prevVelocity = 0.0f;
	void useAccMethod()
	{

		float currentVelocity = ( 0.5f * (-10f * smooth(accHistory)) * Mathf.Pow(Time.deltaTime, 2) );

		this.transform.position = ((this.transform.forward * 3) * (currentVelocity + (prevVelocity * Time.deltaTime)) + this.transform.position);

		debug.text = string.Format("Watch:\n{0}\n{1}", smooth(accHistory), this.transform.position.z);

		prevVelocity = currentVelocity;
		
	}

	void useRotationMethod()
	{
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
		
	}


	float smooth(List<float> data)
	{
		return (data.Count >= 3) ? ( data[0] + ( 2 * data[1] ) + ( 3 * data[2] ) ) / 6 : 0.0f;
	}

	float average(List<float> data)
	{
		if(data.Count < 4)
			return 0.0f;
		
		float sum = 0f;
		foreach(float acc in data)
		{
			sum += acc;
		}
		return sum / data.Count;
	}

	float map(float value, float inRangeL, float inRangeH, float outRangeL, float outRangeH){

		return ((value - inRangeL) / (inRangeH - inRangeL)) * (outRangeH - outRangeL) + outRangeL;

	}



}
