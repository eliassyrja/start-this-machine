using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivityController : MonoBehaviour
{
	private CameraController cameraController;
	private Slider slider;
	[SerializeField] private GameObject valueText;

	private void Start()
	{
		cameraController = FindObjectOfType<CameraController>();
		slider = gameObject.GetComponent<Slider>();
	}

	public void SetMouseSensitivity()
	{
		cameraController.SetSensitivity(slider.value);
		valueText.GetComponent<TextMeshProUGUI>().text = slider.value.ToString("F1");
	}
}
