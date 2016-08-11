using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SculptingTool : MonoBehaviour {

	public GameObject prefab;

	public int framesToWait;
	private int framesWaited;
	private int blockCount;
	private List<float> frameHistory;
	private static List<Object> meshPointers;
	private static bool pause;

	// Use this for initialization
	void Start () {
	
		framesToWait = 1;
		framesWaited = 0;
		frameHistory = new List<float>();
		meshPointers = new List<Object>();
		pause = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(pause)
			return;


		if(framesWaited >= framesToWait)
		{
			meshPointers.Add(Instantiate(prefab, this.transform.position, Quaternion.identity));
			framesWaited = 0;
			blockCount++;
		}

		framesWaited++;
			
			
	
	}

	public static void Play(){
		pause = false;
	}

	public static void Pause(){
		pause = true;
	}

	public static void Clear(){

		foreach(Object mesh in meshPointers){
			Destroy(mesh);
		}
		meshPointers = new List<Object>();
	}

}
