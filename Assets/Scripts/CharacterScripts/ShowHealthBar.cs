using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vino.Devs;
public class ShowHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthView;
    private HealthCharacter GetHealth;

    private void Start()
    { 
        GetHealth = GetComponent<HealthCharacter>();
        healthView.maxValue = GetHealth.GetHealth();
        FollowforPlayer();
    }
    private void Update()
    {
        healthView.value = GetHealth.GetHealth();        
    }
    private void FollowforPlayer()
    {        
        if (GameManager.instance._isEndLoopGame || LevelManager.instance.levelMode == LevelManager.LevelMode.Boss)
        {
            if (GetComponentInParent<MainPlayer>()) {
                healthView.gameObject.transform.parent.SetParent(null);
                healthView.gameObject.transform.parent.position = new Vector3(-0.368999988f, -0.0799999982f, -324.325012f);
                healthView.gameObject.transform.parent.localScale = new Vector3(0.101945192f, 0.121608168f, 0.260676146f);
                healthView.gameObject.transform.parent.eulerAngles = new Vector3(20f, 0, 2.307f);
            }
        }
    }
}
