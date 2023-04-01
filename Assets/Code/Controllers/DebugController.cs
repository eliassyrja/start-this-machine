using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugController : MonoBehaviour
{
    [SerializeField] private GameObject stateText;
    [SerializeField] private GameObject fpsObject;
    private StateMachine stateMachine;
    private TextMeshProUGUI fpsText;
    float fps;

	private void Start()
	{
		fpsText = fpsObject.GetComponent<TextMeshProUGUI>();
        stateMachine = FindAnyObjectByType<StateMachine>();
    }

	// Update is called once per frame
	void Update()
    {
        if(stateMachine.GetCurrentState() != StateMachine.State.PauseMenu)
		{
            fps = 1.0f / Time.smoothDeltaTime;
            fpsText.text = fps.ToString("F0");
        }
        
    }
    public void UpdateStateText(string currentState)
	{
        stateText.GetComponent<TextMeshProUGUI>().text = currentState;
    }
}
