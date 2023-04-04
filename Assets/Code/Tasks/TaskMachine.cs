using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMachine : MonoBehaviour
{
	[SerializeField] private int currentTask;
	public GameObject allTasks;

	// Start is called before the first frame update
	void Start()
	{
		currentTask = 1;
	}

	public void OnButtonClicked()
	{
		if(allTasks.transform.childCount > currentTask)
		{
			if (allTasks.transform.GetChild(currentTask-1).GetComponent<Task>().CheckTaskState())
			{
				currentTask++;
				OnButtonClicked();
			}
		}
	}
}
