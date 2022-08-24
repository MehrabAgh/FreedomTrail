using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{
    public GameObject[] pivots;
    public GameObject[] ScoreStyle;

    private void Start()
    {
        pivots = GameObject.FindGameObjectsWithTag("ScorePivot");
        for (int i = 0; i < pivots.Length; i++)
        {
            Instantiate(ScoreStyle[Random.Range(0, ScoreStyle.Length)], pivots[i].transform.position, pivots[i].transform.rotation);
        }
    }
}
