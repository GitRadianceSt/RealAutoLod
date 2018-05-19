/* BaterryPickup.cs by ThunderWire Games / script for pick up battery */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryPickup : MonoBehaviour {
	private Transform myTransform;	
	private GameObject MessageLabel;
	private GameObject BatteryUIScript;
	public bool EnableMessageMax = true;
	
	public bool Enabled;
	public float BatteryAdd = 0.01f;	
	
	public AudioClip pickupSound;//sound to playe when picking up this item
	
	public string MaxBatteryText = "You have Max Batteries";
	public Color MaxBatteryTextColor = Color.white;	

	public bool PickupMessage;
	public string PickupTEXT = "Battery +1";
	public Color PickupTextColor = Color.white;	
	
	void Start () {
		myTransform = transform;//manually set transform for efficiency
	}
	 
public void UseObject (){
	BatteryUIScript = GameObject.Find("Flashlight");
	BatteryUI BatteryComponent = BatteryUIScript.GetComponent<BatteryUI>();
	
	if(BatteryComponent.EnableBattery == true){
		Enabled = true;
	}

	if(BatteryComponent.EnableBattery == false){
		Enabled = false;
		if(EnableMessageMax){StartCoroutine(MaxBatteries());}
	}

	if(Enabled){
		StartCoroutine(SendMessage());
		BatteryComponent.Batteries += BatteryAdd;
		if(pickupSound){AudioSource.PlayClipAtPoint(pickupSound, myTransform.position, 0.75f);}
		this.GetComponent<Renderer>().enabled = false;
		this.GetComponent<Collider>().enabled = false;
	}
  }
  
 	public IEnumerator SendMessage (){
		MessageLabel = GameObject.Find("UI_MessageLabel");
		Text Message = MessageLabel.GetComponent<Text>();
		/* Message Line */
		EnableMessageMax = false;
		Message.enabled = true;
		Message.color = PickupTextColor;
		Message.text = PickupTEXT;
		yield return new WaitForSeconds(2);
		Message.enabled = false;
		EnableMessageMax = true;
	}
	
 	public IEnumerator MaxBatteries (){
		MessageLabel = GameObject.Find("UI_MessageLabel");
		Text Message = MessageLabel.GetComponent<Text>();
		/* Message Line */
		if(!Enabled){
			EnableMessageMax = false;
			Message.enabled = true;
			Message.color = MaxBatteryTextColor;
			Message.text = MaxBatteryText;
			yield return new WaitForSeconds(3);
			Message.CrossFadeAlpha(0f, 2.0f, false);
			yield return new WaitForSeconds(4);
			Message.enabled = false;
			EnableMessageMax = true;
		}
	}
}