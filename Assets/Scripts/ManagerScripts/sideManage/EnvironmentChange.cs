using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using Vino.Devs;

[Serializable]
public struct ENV
{  
    public Color  C_Ground , C_UnderGround;
    public List<Color> C_Tower;
    public bool _isChecked;
    public void CheckOff() => _isChecked = false;
}
public class EnvironmentChange : MonoBehaviour
{
    public List<ENV> EnvironmentSet;
    public int PivotENV , _Indexer = 0;
    public Material M_Ground, M_UnderGround;
    public List<Material> M_Tower;    

    public void INIT()
    {
        PivotENV = PlayerPrefs.GetInt("PivotENV");
        Changing();
    }

    public void ChangeENV()
    {        
        PivotENV++;
        if (PivotENV > EnvironmentSet.Count) PivotENV = -1;
        if (PivotENV is -1) PivotENV = UnityEngine.Random.Range(0, EnvironmentSet.Count);
        else if (EnvironmentSet[PivotENV]._isChecked) PivotENV++;
        if(StateCheck() == EnvironmentSet.Count)
        {         
            foreach (ENV item in EnvironmentSet)
                item.CheckOff();
            PivotENV = UnityEngine.Random.Range(0, EnvironmentSet.Count);
        }

        Changing();
        PlayerPrefs.SetInt("PivotENV", PivotENV);
    }
    private void Changing()
    {
        for (int i = 0; i < M_Tower.Count; i++) M_Tower[i].color = EnvironmentSet[PivotENV].C_Tower[i];
        M_UnderGround.color = EnvironmentSet[PivotENV].C_UnderGround;
        M_Ground.color = EnvironmentSet[PivotENV].C_Ground;
    }

    private int StateCheck()
    {
        foreach (ENV item in EnvironmentSet)
        {
            if (item._isChecked)
            {
                _Indexer++;
                return _Indexer;
            }            
        }
        return _Indexer;
    }
}
