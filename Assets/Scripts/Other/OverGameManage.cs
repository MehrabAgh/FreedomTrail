using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverGameManage : MonoBehaviour
{    
    public GameObject PlayerChar , co;
    public List<GameObject> coinimg;
    public Transform pivStartCoin, pivEndCoin;
    private bool _nextClick ;
    private void Awake()
    {
     //   PlayerChar = transform.Find("PlayerChar").gameObject;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            GameManager.ins.DisableCar();
            FinishLevel();
            PlayerChar.GetComponent<Animator>().SetLayerWeight(2, 1);
            PlayerChar.GetComponent<FullBodyBipedIK>().enabled = false;
         //   PlayerChar.GetComponent<PlayerWeapon>().DisableGuns();
            GameManager.ins.UIFinish.SetActive(true);
            GameManager.ins.UIOver.SetActive(false);
            GameManager.ins.UIGame.SetActive(false);
            PlayerChar.transform.localRotation = Quaternion.Euler(-1.148f, 141.081f, 0.927f);
            
            if(LevelManager.instance.nameLevel == "BonusLevel")
            {
                LevelManager._isbonusLevel = true;
            }
            if (LevelManager.instance.nameLevel == "BonusLevel")
            {
                LevelManager._isBossLevel = true;
            }
        }
    }
    public void FinishLevel()
    {
        GameManager.ins.EndGame();
    }
    public void NextLevel()
    {
        for (int i = 0; i < ScoreManager.instance.Coin; i++)
        {
            coinimg.Add(Instantiate(co.gameObject, pivStartCoin.transform.position, pivStartCoin.transform.rotation));
            coinimg[i].transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
            coinimg[i].transform.localScale = new Vector3(1, 1, 1);
        }
        _nextClick = true;
    }
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
        LevelManager._isbonusLevel = true;
    }
    private void Update()
    {
        if (_nextClick) {
            coinimg = coinimg.Where(item => item != null).ToList();
            if (coinimg.Count <= 0)
            {
                var index = LevelManager.instance.indexLevel;
                var nameLevel = PlayerPrefs.GetString("Level");
                if (nameLevel == "BonusLevel")
                {
                    index = LevelManager.instance.indexLevel;
                }
                else
                {
                  
                    index++;
                    //
                    if (LevelManager.instance.indexDelayShoot <= 0.1f)
                    {
                        LevelManager.instance.indexDelayShoot = 0.3f;
                        PlayerPrefs.SetFloat("IndexDelay", LevelManager.instance.indexDelayShoot);
                    }
                    else
                    {
                        LevelManager.instance.indexDelayShoot -= 0.1f;
                        PlayerPrefs.SetFloat("IndexDelay", LevelManager.instance.indexDelayShoot);
                    }
                }
                //
                PlayerPrefs.SetInt("IndexLevel", index);
                print("Go to Level = " + index);
                var nameL = "Level" + index;
                PlayerPrefs.SetString("Level", nameL);
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                for (int i = 0; i < coinimg.Count; i++)
                {
                    if (i - 1 != -1)
                    {
                        if (coinimg[i - 1] == null)
                        {
                            if (coinimg[i] != null)
                                coinimg[i].transform.position = Vector3.Lerp(coinimg[i].transform.position, pivEndCoin.transform.position, Time.deltaTime * 20);
                        }
                    }
                    else if (i - 1 == -1)
                    {
                        if (coinimg[i] != null)
                            coinimg[i].transform.position = Vector3.Lerp(coinimg[i].transform.position, pivEndCoin.transform.position, Time.deltaTime * 20);
                    }
                }
            }
        }
    }
}
