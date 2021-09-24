using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject Player;

    public Vector3[] points;
    public int point_number = 0;
    public Vector3 current_target;

    public float tolerance;
    public float speed;
    public float delay_time;


    public bool isReversable;

    public bool isActivated; 

    void Start()
    {
        if(points.Length > 0)
        {
            current_target = points[0];
        }
        tolerance = .08f;
    }

    void FixedUpdate()
    {
        if (!isActivated)
        {
            return;
        }

        if(this.transform.localPosition != current_target)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }

    }

    void MovePlatform()
    {
        Vector3 heading = current_target - this.transform.localPosition;
        this.transform.localPosition += (heading / heading.magnitude) * (speed * Time.deltaTime);
        if(heading.magnitude < tolerance) 
        {
            this.transform.localPosition = current_target;
        }

    }

    void UpdateTarget()
    {

        NextPlatform();

    }

    public void NextPlatform()
    {
        point_number ++;
        if(point_number >= points.Length)
        {
            if (isReversable)
            {
               System.Array.Reverse(points);

            }
            point_number = 0;
        }
        current_target = points[point_number];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated)
        {
            isActivated = true;
        }

        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
            Player.transform.parent = null;
    }
}
