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
        healthView.maxValue = GetHealth.getHealth();
        StartCoroutine(update(0.09f));
    }
    private IEnumerator update(float fps)
    {
        while (true)
        {
            yield return new WaitForSeconds(fps);
            healthView.value = GetHealth.getHealth();
        }
    }
}
