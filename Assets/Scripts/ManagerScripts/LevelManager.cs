using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public CarController[] Cars; 
    public List<Transform> Levels;
    public string nameLevel;
    public Transform levelSubmit,pivStart,pivEnd,BonusLevel, BossLevel;
    public int indexLevel;
    public static LevelManager instance;
    public float indexDelayShoot;
    public Slider pathRow;
    public static bool _isbonusLevel , _isBossLevel;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        nameLevel = PlayerPrefs.GetString("Level");
        indexLevel = PlayerPrefs.GetInt("IndexLevel");
        indexDelayShoot = PlayerPrefs.GetFloat("IndexDelay");
      
        if (indexDelayShoot <= 0)
        {
            indexDelayShoot = 2f;
            PlayerPrefs.SetFloat("IndexDelay", indexDelayShoot);
        }       
        Cars =FindObjectsOfType<CarController>();      
        if (nameLevel == ""||name == null)
        {
            indexLevel = 1;
            indexDelayShoot = 2f;
            nameLevel = "Level"+indexLevel;
            PlayerPrefs.SetString("Level", nameLevel);
        }
        if (indexLevel % 5 == 0 && !_isBossLevel)
        {
            levelSubmit = Instantiate(BossLevel, transform.position, transform.rotation);
            nameLevel = "BossLevel";
            PlayerPrefs.SetString("Level", nameLevel);
            foreach (var item in ScoreManager.instance.TLevel)
            {
                item.text = nameLevel;
            }
            _isBossLevel = true;
        }
        else if (indexLevel % 2 == 0 && !_isbonusLevel)
        {
            levelSubmit = Instantiate(BonusLevel, transform.position, transform.rotation);
            nameLevel = "BonusLevel";
            PlayerPrefs.SetString("Level", nameLevel);
            foreach (var item in ScoreManager.instance.TLevel)
            {
                item.text = nameLevel;
            }
            _isbonusLevel = true;
        }
       
        else
        {
            _isBossLevel = false;
            _isbonusLevel = false;
            foreach (var item in ScoreManager.instance.TLevel)
            {
                item.text = nameLevel;
            }
            foreach (Transform item in Levels)
            {
                if (item.name == nameLevel)
                {
                    levelSubmit = Instantiate(item, transform.position, transform.rotation);
                }
            }
        }
        foreach (CarController item in Cars)
        {            
            item.SpeedMAX = 10 + (indexLevel*20);
            item.MaxSpeed = item.SpeedMAX;
        }      
        pivStart = levelSubmit.transform.Find("PivotStart");
        pivEnd = GameObject.Find("PivotEnd").transform;
        var dis = Vector3.Distance(GameManager.ins.Player.transform.position, pivEnd.transform.position);
        pathRow.maxValue = dis;
        GameManager.ins.Player.transform.position = pivStart.transform.position;
    }    
}
