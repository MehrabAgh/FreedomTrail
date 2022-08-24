using UnityEngine;

public class CarWaypointMovement : MonoBehaviour
{

    public CarAIControl car;
    private Transform carObj;
    private Transform[] waypoints;
    private int curWpIndx = 0;
    private float dist = 0.5f;
    public bool pathFinished = false;
    public bool loopedPath = false;

    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<CarAIControl>();
        carObj = car.gameObject.transform;
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        if (car == null)
        {
            car = GameObject.FindGameObjectWithTag("Player").GetComponent<CarAIControl>();
            carObj = car.gameObject.transform;
        }
        if (waypoints.Length <= 0) {
            waypoints = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                waypoints[i] = transform.GetChild(i);
            }
        }
       
        if(pathFinished)
            return;
       
        car.m_Target = waypoints[curWpIndx];

        // check if we arrived to waypoint
        if(Mathf.Abs(waypoints[curWpIndx].position.magnitude - carObj.position.magnitude) <= dist)
        // then check if it was the last waypoint
            if (curWpIndx + 1 >= waypoints.Length)
            {
                if (loopedPath)
                    curWpIndx = 0;
                else
                    pathFinished = true;
            }
            else
                 curWpIndx ++;

    }
}
