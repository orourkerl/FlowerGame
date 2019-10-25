using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    void OnMouseDown()
	{
		transform.parent.GetComponent<GridBehavior>().LerpToDestination(gameObject);
	}
}
