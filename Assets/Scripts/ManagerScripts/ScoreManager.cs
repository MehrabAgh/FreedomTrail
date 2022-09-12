using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;    
    public int Coin , Key , Xp;      
    private void Awake()
    {     
        instance = this;                  
    } 
}
