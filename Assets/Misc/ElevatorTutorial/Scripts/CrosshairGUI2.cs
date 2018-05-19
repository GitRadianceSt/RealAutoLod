/*
CrosshairGUI.cs ver. 31.3.16 - wirted by ThunderWire Games * Script for Crosshair with Interact function
*/

using UnityEngine;
using System.Collections;

public class CrosshairGUI2 : MonoBehaviour {

public Texture2D m_crosshairTexture;
public Texture2D m_useTexture;
public float RayLength = 3f;

[Tooltip("Set to layer mask, that be Interactable.")]
public LayerMask LayerInteract;

public bool m_ShowCursor = false;
public bool CursorButton;

private bool m_DefaultReticle = true;
private bool m_UseReticle;
private bool m_bIsCrosshairVisible = true;
private Rect m_crosshairRect;
private Ray playerAim;
private Camera playerCam;
 
	void  Update (){
		playerCam = Camera.main;
		Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;
		
		if (Physics.Raycast (playerAim, out hit, RayLength, LayerInteract))
		{
			m_DefaultReticle = false;
			m_UseReticle = true;
		}else{
			m_DefaultReticle = true;
			m_UseReticle = false;		
		}
		
		if(Input.GetKeyDown(KeyCode.Escape) && CursorButton) {
			m_ShowCursor = !m_ShowCursor;
		}
		
		if(m_ShowCursor){
			Cursor.visible = (true);
			Cursor.lockState = CursorLockMode.None;					
		}
		else {
			Cursor.visible = (false);
			Cursor.lockState = CursorLockMode.Locked;						
		}
	}
 
	void  Awake (){
	    if(m_DefaultReticle){
		  m_crosshairRect = new Rect((Screen.width - m_crosshairTexture.width) / 2, 
								(Screen.height - m_crosshairTexture.height) / 2, 
								m_crosshairTexture.width, 
								m_crosshairTexture.height);
	    }
		
	    if(m_UseReticle){
		  m_crosshairRect = new Rect((Screen.width - m_useTexture.width) / 2, 
								(Screen.height - m_useTexture.height) / 2, 
								m_useTexture.width, 
								m_useTexture.height);
	    }
	}
 
	void  OnGUI (){
		if(m_bIsCrosshairVisible)
		  if(m_DefaultReticle){
			GUI.DrawTexture(m_crosshairRect, m_crosshairTexture);
		 }
		  if(m_UseReticle){
			GUI.DrawTexture(m_crosshairRect, m_useTexture);
		 }
	}
}