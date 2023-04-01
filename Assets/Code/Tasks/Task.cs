using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
	public string taskName;
	public bool taskDone;
	public SubTask[] subTask;
}
