/* BatteryNGUI.cs by ThunderWire Games / script for Battery UI */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryUI : MonoBehaviour {
     public GameObject BatteryLabel;
	 public AudioClip ReloadBatteriesSound;
	 public KeyCode BatteryReloadKey = KeyCode.B;
	 
	 public float Batteries;
	 private float MinBatteries = 0.0f;
	 private float MaxBatteries = 0.05f;
	 private float BatteryDeduct = 1.0f;
	 private Transform myTransform;	
	 
	 public bool EnableBattery;
	 

	void Start () {
		myTransform = transform;//manually set transform for efficiency
	}
	 
	// Update is called once per frame
	void Update () {
	//Debug.Log ("Batteries: " + Batteries.ToString() );
	if(Input.GetKeyDown(BatteryReloadKey) && Batteries > 0 && Batteries <= 0.05f) {
	    FlashlightScript FlashlightScriptComponent = this.GetComponent<FlashlightScript>();
		
	if(FlashlightScriptComponent.batteryPercentage < 90.0f){
		FlashlightScriptComponent.batteryPercentage = 100;
		Batteries -= BatteryDeduct * 0.01f;
		if(ReloadBatteriesSound){AudioSource.PlayClipAtPoint(ReloadBatteriesSound, myTransform.position, 0.75f);}
		}
     }

	 	Text Battery = BatteryLabel.GetComponent<Text>();

		Batteries = Mathf.Clamp(Batteries, 0.0f, 0.05f);
		
	    if (Batteries <= MinBatteries)
			{
			     Batteries = MinBatteries;
				 Battery.text = "0 / 5";
				 EnableBattery = true;
			}
		
	    else if (Batteries <= 0.01f && Batteries > 0)
			{
				 Battery.text = "1 / 5";
				 EnableBattery = true;
			}				
		
	    else if (Batteries <= 0.02f && Batteries > 0.01f)
			{
				 Battery.text = "2 / 5";
				 EnableBattery = true;
			}				
		
	    else if (Batteries <= 0.03f && Batteries > 0.02f)
			{
				 Battery.text = "3 / 5";
				 EnableBattery = true;
			}				
		
	    else if (Batteries <= 0.04f && Batteries > 0.03f)
			{
				 Battery.text = "4 / 5";
				 EnableBattery = true;
			}				
		
	    else if (Batteries <= 0.05f && Batteries > 0.04f)
			{
				 Battery.text = "5 / 5";
				 EnableBattery = false;
			}
			
			//Setting for a max batteries
	    else if(Batteries > 0.05f)
		   {
              Batteries = MaxBatteries;
			  EnableBattery = false;
           }
  }
}