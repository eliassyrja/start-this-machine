using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private int points;
    [SerializeField] private float amplitude = 1;
    [SerializeField] private float frequency = 1;
    [SerializeField] private Vector2 xLimits = new Vector2(0, 1);
    [SerializeField] private float movementSpeed = 1;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    void Draw()
	{
        float xStart = xLimits.x;
        float tau = 2 * Mathf.PI;
        float xFinish = xLimits.y;

        lineRenderer.positionCount = points;
        for (int currentPoint = 0; currentPoint < points; currentPoint++)
		{
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((tau * frequency * x) + (Time.timeSinceLevelLoad * movementSpeed));
            lineRenderer.SetPosition(currentPoint, new Vector3(x/2, y, 0));
		}
	}

    void Update()
    {
        Draw();
    }
}
