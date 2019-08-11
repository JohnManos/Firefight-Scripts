using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour {
	public float speed = 2f;
    public float maxRotation = 45f;
 
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
    }
	/* public Transform pointA;
    public Transform pointB;
    public float journeyTime = 1.0f;
	private float startTime;
	private bool isRight = true;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;
	private Vector3 center;

	// Use this for initialization
	void Start () {
		pointAPosition = new Vector3(pointA.position.x, pointA.position.y, pointA.position.z);
        pointBPosition = new Vector3(pointB.position.x, pointB.position.y, pointB.position.z);
		startTime = Time.time;
    }

    void Update()
    {
		
        // The center of the arc
        Vector3 center = transform.position;

        // move the center a bit downwards to make the arc vertical
        center -= new Vector3(0, 1, 0);

        // Interpolate over the arc relative to center
        Vector3 pointARelCenter = pointAPosition - center;
        Vector3 pointBRelCenter = pointBPosition - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired time for
        // the total journey.
        float fracComplete = (Time.time - startTime) / journeyTime;

        if (isRight)
		{
			transform.position = Vector3.Slerp(pointARelCenter, pointBRelCenter, fracComplete);
        	transform.position += center;
		} else
		{
			transform.position = Vector3.Slerp(pointBRelCenter, pointARelCenter, fracComplete);
        	transform.position += center;
		}
		if (transform.position == pointBPosition)
		{
			isRight = false;
		}
		if (transform.position == pointAPosition)
		{
			isRight = true;
		}
	}
*/
}
 