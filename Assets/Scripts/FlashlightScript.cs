/* FlashlightScript.cs by ThunderWire Games / script for Flashlight */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

	[System.Serializable]
	public class BatterySpritesClass{
		public Sprite Battery_0_red;
		public Sprite Battery_5;
		public Sprite Battery_10;
		public Sprite Battery_15;
		public Sprite Battery_20;
		public Sprite Battery_25;
		public Sprite Battery_30;
		public Sprite Battery_35;
		public Sprite Battery_40;
		public Sprite Battery_45;
		public Sprite Battery_50;
		public Sprite Battery_55;
		public Sprite Battery_60;
		public Sprite Battery_65;
		public Sprite Battery_70;
		public Sprite Battery_75;
		public Sprite Battery_80;
		public Sprite Battery_85;
		public Sprite Battery_90;
		public Sprite Battery_95;
		public Sprite Battery_100;
	}

public class FlashlightScript : MonoBehaviour {
	public BatterySpritesClass BatterySprites = new BatterySpritesClass();
	//public KeyCode FlashlightKey = KeyCode.F;
	public AudioClip ClickSound;
	public float batteryLifeInSec = 300f;
	public float batteryPercentage = 100;
	public GameObject FlashlightSprite;
	public bool PickedFlashlight = false;
	
	public bool on;
	private float timer;
	private Transform myTransform;	

	void Start () {
		myTransform = transform;//manually set transform for efficiency
	}

	void Update() 
	{
	    Image BatterySprite = FlashlightSprite.GetComponent<Image>();
		Light lite = this.GetComponent<Light>();
		timer += Time.deltaTime;
		
		if(PickedFlashlight)
		{	
			Debug.Log("PickedUp2");
			if(Input.GetButtonDown("Flashlight") && timer >= 0.3f && batteryPercentage > 0) 
			{
				
				on = !on;
            	if(ClickSound)
           		 {
            		AudioSource.PlayClipAtPoint(ClickSound, myTransform.position, 0.75f);
          		  }
				timer = 0;
			}
		}	

		if(on) {
			lite.enabled = true;
			batteryPercentage -= Time.deltaTime * (100 / batteryLifeInSec);
		}
		else {
			lite.enabled = false;
		}
	
		batteryPercentage = Mathf.Clamp(batteryPercentage, 0, 100);
	
			if (batteryPercentage > 95.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_100;
				lite.intensity = Mathf.Lerp(lite.intensity, 2, Time.deltaTime);
			}
			else if (batteryPercentage <= 95.0f && batteryPercentage > 90.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_95;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.9f, Time.deltaTime);
			}
			else if (batteryPercentage <= 90.0f && batteryPercentage > 85.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_90;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.8f, Time.deltaTime);
			}
			else if (batteryPercentage <= 85.0f && batteryPercentage > 80.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_85;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.7f, Time.deltaTime);
			}
			else if (batteryPercentage <= 80.0f && batteryPercentage > 75.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_80;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.6f, Time.deltaTime);
			}
			else if (batteryPercentage <= 75.0f && batteryPercentage > 70.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_75;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.5f, Time.deltaTime);
			}
			else if (batteryPercentage <= 70.0f && batteryPercentage > 65.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_70;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.4f, Time.deltaTime);
			}
			else if (batteryPercentage <= 65.0f && batteryPercentage > 60.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_65;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.3f, Time.deltaTime);
			}
			else if (batteryPercentage <= 60.0f && batteryPercentage > 55.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_60;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.2f, Time.deltaTime);
			}
			else if (batteryPercentage <= 55.0f && batteryPercentage > 50.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_55;
				lite.intensity = Mathf.Lerp(lite.intensity, 1.1f, Time.deltaTime);
			}
			else if (batteryPercentage <= 50.0f && batteryPercentage > 45.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_50;
				lite.intensity = Mathf.Lerp(lite.intensity, 1f, Time.deltaTime);
			}
			else if (batteryPercentage <= 45.0f && batteryPercentage > 40.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_45;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.9f, Time.deltaTime);
			}		
			else if (batteryPercentage <= 40.0f && batteryPercentage > 35.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_40;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.8f, Time.deltaTime);
			}	
			else if (batteryPercentage <= 35.0f && batteryPercentage > 30.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_35;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.7f, Time.deltaTime);
			}	
			else if (batteryPercentage <= 30.0f && batteryPercentage > 25.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_30;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.6f, Time.deltaTime);
			}			
			else if (batteryPercentage <= 25.0f && batteryPercentage > 20.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_25;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.5f, Time.deltaTime);
			}
			else if (batteryPercentage <= 20.0f && batteryPercentage > 15.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_20;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.4f, Time.deltaTime);
			}
			else if (batteryPercentage <= 15.0f && batteryPercentage > 10.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_15;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.3f, Time.deltaTime);
			}
			else if (batteryPercentage <= 10.0f && batteryPercentage > 5.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_10;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.2f, Time.deltaTime);
			}
			else if (batteryPercentage <= 5.0f && batteryPercentage > 1.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_5;
				lite.intensity = Mathf.Lerp(lite.intensity, 0.1f, Time.deltaTime);
			}
			else if (batteryPercentage <= 1.0f)
			{
				BatterySprite.sprite = BatterySprites.Battery_0_red;
				lite.intensity = Mathf.Lerp(lite.intensity, 0, Time.deltaTime * 2);
			}
	
	}
}