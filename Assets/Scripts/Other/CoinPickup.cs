using System.Collections;
using UnityEngine;
using Vino.Devs;

public class CoinPickup : MonoBehaviour 
{

	[SerializeField] private int count = 1; // how many coins are in this pickup
	private Transform visual; // the visual of the pickup which is different than this gameobject
	
	public GameObject pickupParticleEffect;

	private void Start () 
	{
		visual = transform.GetChild(0).transform;
		StartCoroutine(PickupFloatingAnimation());
		
		// fail proof
		if(transform.CompareTag("ScorePickup") == false)
			transform.tag = "ScorePickup";
		GetComponent<Collider>().isTrigger = true;
	}
	public void GetPicked()
	{
		ScoreManager.instance.CurrCoin += count;
		Destroy(gameObject);
		PlayPickedupEffects();				
	}
	private void PlayPickedupEffects() 
	{
		Destroy(Instantiate(pickupParticleEffect, transform.position, transform.rotation), 3);
		ResourceSpawner.instance.coin2uiFx(transform.position);
		// spawn a particle with trail that goes to the coin ui and fire an event when it does trigger the ui animation
	}
	
	private IEnumerator PickupFloatingAnimation()
	{
		while (true)
		{
			visual.Rotate(Vector3.up, 60 * Time.deltaTime, Space.World);
			visual.localPosition = new Vector3(0, Mathf.Sin(Time.time) * 0.5f,0);
			yield return null;			
		}
	}
}
