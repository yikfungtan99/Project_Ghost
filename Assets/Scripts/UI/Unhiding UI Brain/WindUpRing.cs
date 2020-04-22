using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindUpRing : MonoBehaviour
{
	GameManager gm;
    [SerializeField, ReadOnly] Image ring;
	[SerializeField, ReadOnly] float unhideTime;
	
	void Awake()
	{
		gm = GameManager.Instance;
		ring = GetComponent<Image>();
		unhideTime = GameManager.Instance.playerMovement.UnhideTime;
	}
	
	public void UpdateFill(float holdTime)
	{
		if(unhideTime <= 0)
		{
			Debug.LogWarning("Variable unhideTime is a negative or of null status.");
			return;
		}
		
		float fillRatio = holdTime / unhideTime;
		
		ring.fillAmount = fillRatio;
	}
	
	public float RingFillAmount
	{
		get
		{
			return ring.fillAmount;
		}
		set
		{
			ring.fillAmount = value;
		}
	}
}
