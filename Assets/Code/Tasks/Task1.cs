using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : Task
{
    [SerializeField] private GameObject powerSwitch;

	public override bool CheckTaskState()
	{
        return powerSwitch.GetComponent<Button>().buttonState;
    }
}
