using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowMouse : MonoBehaviour
{
    [SerializeField] RectTransform uiObject;
	[SerializeField] bool isCurrentObjectFollowing = false;
	
	[SerializeField] Vector3 offset;
	[SerializeField] float safeZ;
	
	[SerializeField] bool isUsingMainCamera = false;
	[SerializeField] Camera camera;
	
	void Awake()
	{
		if(isCurrentObjectFollowing)
		{
			uiObject = GetComponent<RectTransform>();
		}
		
		if(isUsingMainCamera)
		{
			camera = Camera.main;
		}
	}
	
	/*void OnEnable()
	{
		Cursor.visible = false;
	}
	
	void OnDisable()
	{
		Cursor.visible = true;
	}*/
	
	void Update()
	{
		FollowMouse();
	}
	
	void FollowMouse()
	{
		if(camera == null || uiObject == null)
		{
			return;
		}
		
		Vector3 newPosition = Input.mousePosition + offset;
		newPosition.z = safeZ;
		
		uiObject.position = camera.ScreenToWorldPoint(newPosition);
	}
}
