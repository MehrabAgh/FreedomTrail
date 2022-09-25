using UnityEngine;

public class LifeTime : MonoBehaviour 
{
	public float life = 5;

	private void Update () 
	{
		if(life <= 0)
			Destroy(gameObject);
		life -= Time.deltaTime;
	}
}
