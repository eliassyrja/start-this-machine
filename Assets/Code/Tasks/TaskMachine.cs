using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMachine : MonoBehaviour
{
    public Task[] tasks;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubTaskDone(string _subTaskName)
	{
		foreach (Task task in tasks)
		{
            foreach (SubTask subTask in task.subTask)
            {
                if (subTask.subTaskName == _subTaskName)
                {
                    subTask.subTaskDone = true;
                }
            }
            int subTasksCompeleted = 0;
			foreach (SubTask subTask in task.subTask)
			{
                if (subTask.subTaskDone)
				{
                    subTasksCompeleted++;
				}
			}
            if(subTasksCompeleted == task.subTask.Length && task.subTask.Length != 0)
			{
                task.taskDone = true;
			}
            
		}
	}
}
