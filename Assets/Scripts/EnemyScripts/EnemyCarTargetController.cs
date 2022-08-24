using UnityEngine;

public class EnemyCarTargetController : MonoBehaviour
{

    private CarAIControl car;
    [HideInInspector]
    public Transform player;
    public Transform alterTarget;

    private void Start()
    {
        car = GetComponent<CarAIControl>();
        
        /*
        if(Vector3.Distance(transform.position, CarWaypointMovement.leftTarget.position) < Vector3.Distance(transform.position, CarWaypointMovement.rightTarget.position))
        {
            car.m_Target = CarWaypointMovement.leftTarget;
        }
        else 
        {
            car.m_Target = CarWaypointMovement.rightTarget;
        }
        */

        player = GameObject.Find("Player Car").transform;
       
        car.m_Target = player;
    }

    void Update()
    {
        // if we are going towards the player
        if (Vector3.Angle(transform.forward, player.forward) < 120)
        {
            car.m_Target = player;
            return;
        }
            

        
        alterTarget.position = player.position + ((transform.position - player.position).normalized * -5);
        car.m_Target = alterTarget;

    }
}
