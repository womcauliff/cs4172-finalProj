using UnityEngine;
using System.Collections;

public class ArcherAI : MonoBehaviour {

	public float rotationDegreesPerSecond = 45f;
	public float rotationDegreesAmount = 90f;
	private float totalRotation = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if we haven't reached the desired rotation, swing
		
		if(Mathf.Abs(totalRotation) < Mathf.Abs(rotationDegreesAmount))
			SwingOpen();
	}
	
	void SwingOpen()
	{   
		float currentAngle = transform.rotation.eulerAngles.y;
		transform.rotation = 
			Quaternion.AngleAxis(currentAngle + (Time.deltaTime * rotationDegreesPerSecond), Vector3.up);
		totalRotation += Time.deltaTime * rotationDegreesPerSecond;
	}
}
