using UnityEngine;
using System.Collections;

public class ClearParticleButton : MonoBehaviour {


	private ParticleSystem partSys;

	public static int buttonState;

	private ArrayList prevStates;

	// Use this for initialization
	void Start () {

		partSys = this.GetComponent<ParticleSystem>();

		partSys.Pause();
		partSys.Clear();

		buttonState = 0;

		prevStates = new ArrayList();

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			buttonState = 1;
		}
		else if(Input.GetKeyUp(KeyCode.Escape))
		{
			buttonState = 0;	
		}

		prevStates.Add(buttonState);
		if(prevStates.Count > 15){
			prevStates.RemoveAt(0);
		}

		if(buttonState == 1)
		{
			SculptingTool.Play();
			//partSys.Play();
		}else{
			SculptingTool.Pause();
			//partSys.Pause();
		}

		if(checkDoubleClick())
		{
			SculptingTool.Clear();
			//partSys.Clear();
		}

	
	}

	private bool checkDoubleClick(){
		if(prevStates.Count < 6)
			return false;
		
		int countOI = 0;
		for(int i = 0; i < prevStates.Count - 1; i++){

			if((int)prevStates[i] == 0 && (int)prevStates[i + 1] == 1){
				countOI++;
			}

		}
		return (countOI >= 2); 
	}
}
