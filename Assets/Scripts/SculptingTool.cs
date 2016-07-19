using UnityEngine;
using System.Collections;

public class SculptingTool : MonoBehaviour {

	public GameObject prefab;

	public int framesToWait;
	private int framesWaited;
	private bool isCounting;
	private int blockCount;

	// Use this for initialization
	void Start () {
	
		framesToWait = 40;
		framesWaited = 0;
		isCounting = false;
		prefab = this.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		TextMesh debug = GameObject.Find("Debug").GetComponent<TextMesh>();

		debug.text = string.Format("{0}\n{1}", framesWaited, blockCount);

		if(isCounting && framesWaited >= framesToWait)
		{
			isCounting = false;
			framesWaited = 0;
		}

		if(isCounting)
		{
			framesWaited++;
			return;
		}

		if(RotateInterface.getState() == RotateInterface.LEFT_CLICK || RotateInterface.getState() == RotateInterface.RIGHT_CLICK || RotateInterface.getState() == RotateInterface.LEFT_HALF || RotateInterface.getState() == RotateInterface.RIGHT_HALF)
		{
			Instantiate(prefab, this.transform.position, Quaternion.identity);
			isCounting = true;
			blockCount++;

		}


	
	}
}
