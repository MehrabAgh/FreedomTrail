using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Vino.Devs
{
    public class GiftPie : MonoBehaviour
    {
        public Image pivArrow;
        public float angle;
        public int multScore;
        public bool x, clicked;
        public Image coinimg;
        public Transform pivCoin;
        public List<GameObject> co;
        private NumberGiftUpdater numberGift;
        public void click()
        {
            clicked = true;
            for (int i = 0; i < multScore; i++)
            {
                co.Add(Instantiate(coinimg.gameObject, transform.position, transform.rotation));
                co[i].transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
                co[i].transform.localScale = new Vector3(1, 1, 1);
            }
            if (ScoreManager.instance.Coin > 0) ScoreManager.instance.Coin *= multScore; 
            else if (ScoreManager.instance.Coin == 0) ScoreManager.instance.Coin += multScore;
        }

        private void Start()
        {
            numberGift = FindObjectOfType<NumberGiftUpdater>();
            angle = -50;
        }
        private void StateScore()
        {
            if (angle > 40) multScore = 2;
            if (angle >= 15 && angle <= 30) multScore = 3;
            if (angle >= -12 && angle <= 10) multScore = 4;
            if (angle >= -34 && angle <= -15) multScore = 6;       
            if (angle < -40) multScore = 8;        
            numberGift.ScoreViewer.text = (ScoreManager.instance.CurrCoin * multScore).ToString();
        }
        void Update()
        {
            StateScore();
            if (!clicked)
            {
                if (angle >= 50) x = true;             
                if (angle <= -50) x = false; 
                if (x) angle -= 8;
                else angle += 8;
                //angle = Mathf.Clamp(angle, -50, 50);
                pivArrow.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {

                for (int i = 0; i < co.Count; i++)
                {
                    if (i - 1 != -1)
                    {
                        if (co[i - 1] == null)
                        {
                            if (co[i] != null)
                                co[i].transform.position = Vector3.Lerp(co[i].transform.position, pivCoin.transform.position, Time.deltaTime * 20);
                        }
                    }
                    else if (i - 1 == -1)
                    {
                        if (co[i] != null)
                            co[i].transform.position = Vector3.Lerp(co[i].transform.position, pivCoin.transform.position, Time.deltaTime * 20);
                    }
                }
            }
        }
    }
}