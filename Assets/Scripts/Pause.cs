using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	private float savedTimeScale;

	public void PauseGame(){
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
	}

	public void UnPauseGame(){
		Time.timeScale = savedTimeScale;
	
	}

}
