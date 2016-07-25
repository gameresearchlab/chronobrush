using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SculptingTool : MonoBehaviour {

	public GameObject prefab;

	public int framesToWait;
	private int framesWaited;
	private int blockCount;
	private List<float> frameHistory;

	// Use this for initialization
	void Start () {
	
		framesToWait = 5;
		framesWaited = 0;
		frameHistory = new List<float>();
	}
	
	// Update is called once per frame
	void Update () {

		TextMesh debug = GameObject.Find("Debug").GetComponent<TextMesh>();

		if(frameHistory.Count >= 60)
		{
			frameHistory.RemoveAt(0);
		}
		frameHistory.Add(1.0f / Time.deltaTime);

		debug.text = string.Format("{0}\n{1}\n{2}", framesWaited, blockCount, getAverage(frameHistory));

		if(framesWaited >= framesToWait)
		{
			Instantiate(prefab, this.transform.position, Quaternion.identity);
			framesWaited = 0;
			blockCount++;
		}

		framesWaited++;
			
			
	
	}

	float getAverage(List<float> lst){
		if(lst.Count == 0)
		{
			return 0.0f;
		}
			
		float sum = 0.0f;

		for(int i = 0; i < lst.Count; i++)
		{
			sum += lst[i];
		}


		return sum / lst.Count;
	}

}
