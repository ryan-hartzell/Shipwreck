using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShrinkingCircle : MonoBehaviour
{
	float theta_scale = 0.01f;        //Set lower to add more points
	int size; //Total number of points in circle
	public float radius;
	LineRenderer lineRenderer;

	void Awake()
	{	
		float sizeValue = (2.0f * Mathf.PI) / theta_scale;
		size = (int)sizeValue;
		size++;

		radius = gameObject.GetComponent<ShipController> ().moveRange;

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.startWidth = 0.1f;
		lineRenderer.endWidth = 0.1f;
		lineRenderer.positionCount = size;
		lineRenderer.sortingLayerName = "Foreground";
	}

	void Update()
	{
		Vector3 pos;
		float theta = 0f;

		radius = gameObject.GetComponent<ShipController> ().moveRange;

		for (int i = 0; i < size; i++)
		{
			theta += (2.0f * Mathf.PI * theta_scale);
			float x = radius * Mathf.Cos(theta);
			float y = radius * Mathf.Sin(theta);
			x += gameObject.transform.position.x;
			y += gameObject.transform.position.y;
			pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
		}
	}
}