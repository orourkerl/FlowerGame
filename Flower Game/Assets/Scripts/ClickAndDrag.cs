using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;

    void Update()
	{
		Vector3 mousePos;
		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		if (isBeingHeld == true)
		{
			this.gameObject.transform.localPosition = new Vector3(Mathf.Clamp(mousePos.x, -8.5f, 8.5f), Mathf.Clamp(mousePos.y, -4.6f, 4.6f), 0); ;
		}
	}

    private void OnMouseDown()
	{
		Vector3 mousePos;
		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		isBeingHeld = true;
	}

    private void OnMouseUp()
	{
		isBeingHeld = false;
	}


}
