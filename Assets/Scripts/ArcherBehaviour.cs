using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherBehaviour : MonoBehaviour {

	private GameObject currentTarget;
	public Queue<GameObject> q;
	GameObject[] gos;
	public string targetTag;

	// fire arrows
	bool readyToFire = true;
	public Rigidbody arrow;
	public Transform archerEnd;
	// hack
	int count;

	// Use this for initialization
	void Start () {
		currentTarget = null;
		q = new Queue<GameObject>();
		targetTag = "footsoldiers";
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTarget) {
			Debug.Log("Archer A.I.!");
			float damping = .01f;
			Transform target = currentTarget.transform;
			Vector3 lookPos = target.position - this.transform.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);

			//OLD OLD OLD CODE
			//			Vector3 tarPos = currentTarget.transform.position;
			//			Quaternion neededRotation = Quaternion.LookRotation(tarPos - this.transform.position);
			//			Quaternion interpolatedRotation = Quaternion.Slerp(this.transform.rotation, neededRotation, Time.deltaTime * .01f);
			//			this.transform.rotation = interpolatedRotation;
			//			wpPos.y = 5;//adjusted for realistic soldier movement
			//			transform.LookAt (wpPos);
			//			transform.position = Vector3.MoveTowards (transform.position, wpPos, 0.10f);

			/* COPY PASTED CODE (javascript)
			 * var damping:int=2;
			 * var target:Transform;
			 * var lookPos = target.position - transform.position;
			 * lookPos.y = 0;
			 * var rotation = Quaternion.LookRotation(lookPos);
			 * transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping); 
			 */
		} else {
			setCurrentTarget();
		}

		count++;

		// readyToFire == true
		if(count % 100 == 0) {
//			Debug.Log ("fire");
			Rigidbody arrowInstance;
			arrowInstance = Instantiate(arrow, archerEnd.position, archerEnd.rotation) as Rigidbody;
			arrowInstance.AddForce(archerEnd.forward * 500);
//			readyToFire = false;
		}
//		StartCoroutine ("WaitToFireAgain");
	}

	public void addNewTarget() {
		gos = GameObject.FindGameObjectsWithTag (targetTag);
		foreach (GameObject go in gos) {
			if (!q.Contains(go)) {
				q.Enqueue(go);
			}
		}
	}

	void setCurrentTarget() {
		if (q.Count != 0) {
			currentTarget = q.Dequeue ();
		}
	}

//	IEnumerator WaitToFireAgain() {
//		readyToFire = false;
//		Debug.Log ("reachable");
//		yield return new WaitForSeconds (5);
//		readyToFire = true;
//	}
}
