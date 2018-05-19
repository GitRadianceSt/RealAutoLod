/*
Notes.cs - wirted by ThunderWire Games * Script for Show Notes
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class Notes : MonoBehaviour {

public GameObject UI;
public GameObject NotesUIImage;
public GameObject FPSScript;
public GameObject playerChar;
public GameObject WeaponHolder;
public Sprite NotesTexture;

private bool showNote;
private bool backNote;

public GameObject pauseMenu;

	void Start(){

		//playerChar.GetComponent<FirstPersonController>().enabled = true;
		showNote = false;
		Cursor.visible = (false);
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void FixedUpdate () {

		Image NoteImage = NotesUIImage.GetComponent<Image>();
		CrosshairGUI Crosshair = Camera.main.GetComponent<CrosshairGUI>();
		//FirstPersonController FPSComp = FPSScript.GetComponent<FirstPersonController>();
		SimpleWeaponSway WeaponComp = WeaponHolder.GetComponent<SimpleWeaponSway>();
		
		if(showNote){
			NoteImage.enabled = true;
			Crosshair.enabled = false;
			UI.SetActive(true);
			//gameObject.enabled = false;

			//FPSComp.enabled = false;
			playerChar.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
			WeaponComp.enabled = false;
			Debug.Log ("showNote");
			Cursor.visible = (true);
			Cursor.lockState = CursorLockMode.None;
			//Time.timeScale = 0;
			//pauseMenu.GetComponent<Pause>().PauseGame();

		}

		if(backNote){
			//Time.timeScale = 1;
			//pauseMenu.GetComponent<Pause>().UnPauseGame();
			NoteImage.enabled = false;
			Crosshair.enabled = true;
			UI.SetActive(false);
			Crosshair.m_DefaultReticle = true;
			Crosshair.m_UseReticle = false;
			//GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
			//FPSComp.enabled = true;
			//playerChar.GetComponent<FirstPersonController>().enabled = true;
			playerChar.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
			WeaponComp.enabled = true;
			Debug.Log ("backNote");
			Remove();
		}
	}

	public void ShowNotes () {
		Image NoteImage = NotesUIImage.GetComponent<Image>();
		NoteImage.sprite = NotesTexture;
		showNote = true;
		backNote = false;
		//Cursor.visible = (true);
		//Cursor.lockState = CursorLockMode.None;
		Debug.Log ("ShowNotes");
	}
	
	public void BackNotes () {
		showNote = false;
		backNote = true;
		Cursor.visible = (false);
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Remove () {
		Destroy(gameObject);
	}
}