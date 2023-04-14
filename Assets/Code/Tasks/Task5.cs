using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task5 : Task
{
	[SerializeField] private GameObject rotaryKnob;

	public override bool CheckTaskState()
	{
		if (rotaryKnob.GetComponent<ToggleSwitch>().switchState)
		{
			// Additional functionality here

			return true;
		}
		else
		{
			return false;
		}
	}
}