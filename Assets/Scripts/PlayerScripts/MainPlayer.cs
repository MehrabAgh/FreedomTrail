using System.Collections;
using UnityEngine;
using Vino.Devs;

public class MainPlayer : PlayerMovement
{
    private PlayerState mystate = new PlayerState();
    private mainGun gun;
    private void Awake() => gun = GetComponent<mainGun>();    
    private void Start() => StartCoroutine(update(0.003f));    

    private IEnumerator update(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);

            if (Input.GetMouseButtonDown(0))StartMove();

            if (Input.GetMouseButton(0))
            {
                PlayerState.myState = PlayerState.playerState.ATTACK;
                mystate.EventStates(PlayerState.myState);
                UpdateMove();
                gun.ShootGun();
            }

            if (Input.GetMouseButtonUp(0))
            {
                PlayerState.myState = PlayerState.playerState.COVER;
                mystate.EventStates(PlayerState.myState);
                EndMove();
            }
            print(mystate.GetState());
        }
    }   
}
