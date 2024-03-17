using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Vino.Devs
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public List<TMP_Text> T_Coin, T_Key, T_CurrentCoin, T_Kill, t_StateLevel;
        public List<Slider> S_XpProgress;
        [SerializeField] private List<Text> t_Level;
        public TMP_Text t_Price;
        public Button StartGame, RestartGameOver, NextLevelWin;
        public Slider S_DistEnd;
        public GameObject LosePanel, WinPanel, MainMenu, InGamePanel;

        public RectTransform PageShop , GiftMenu , LevelUpMenu;
        public Button btn_NextPage, btn_BackPage , btn_EndOpenGift;
        private Vector3 pagePosition;

        public NumberCounterUpdater tr_Coin , tr_Kill , tr_End;

        [SerializeField] private List<Button> BuyButtons;
        [SerializeField] private List<Transform> GroupBuyButton;
        #region Standard Events
        private void Awake() => instance = this;
        private void Start()
        {
            S_DistEnd.maxValue = GameManager.instance.GetDistanceToEnd();
            StartGame.onClick.AddListener(StartGameEvent);
            RestartGameOver.onClick.AddListener(RestartLevel);
            NextLevelWin.onClick.AddListener(NextLevel);
            btn_NextPage.onClick.AddListener(NextPage);
            btn_BackPage.onClick.AddListener(BackPage);
            btn_EndOpenGift.onClick.AddListener(EndGiftShow);          
            
            foreach (Button item in BuyButtons)
            {               
                if (ItemManager.instance.price > 0)
                {
                    EnableGroupBuyButton();
                    item.GetComponentInChildren<Text>().text = ItemManager.instance.price.ToString();
                }
                else DisableGroupBuyButton();
            }
            StartCoroutine(_update(0.001f));
        }
        IEnumerator _update(float time)
        {
            while (true)
            {
                yield return new WaitForSeconds(time);
                UpdateResources();
                //
                if (GameManager.instance.Player.GetState() == PlayerState.playerState.DEATH)
                {
                    LosePanel.SetActive(true);
                    InGamePanel.SetActive(false);
                }
                else if (GameManager.instance.gameState == GameManager.GameState.EndGame)
                {                   
                    if(!WinPanel.activeSelf)
                        ScoreManager.instance.XpEnder(.25f);
                    WinPanel.SetActive(true);
                    InGamePanel.SetActive(false);
                    SetValueEnd();
                }
                //
                if (S_DistEnd) S_DistEnd.value = GameManager.instance.GetDistanceToEnd();

                foreach (Button item in BuyButtons)
                {                 
                    if (ItemManager.instance.price > 0)
                    {
                        EnableGroupBuyButton();
                        item.GetComponentInChildren<Text>().text = ItemManager.instance.price.ToString();
                    }
                    else DisableGroupBuyButton();
                }
            }
        }
        #endregion

        #region Custom Events for Buttons
        private void StartGameEvent() => GameManager.instance.gameState = GameManager.GameState.LoopGame;
        public void RestartLevel() => SceneManager.LoadScene("SampleScene");
        private void NextLevel()
        {
            LevelManager.instance.NextLevel();
            ScoreManager.instance.SaveResource();
            ScoreManager.instance.XpCollectsEnd();
            SceneManager.LoadScene("SampleScene");
        }
        public void ChangeSettings(Text TextProp)
        {
            if (TextProp.text == "ON")
                TextProp.text = "OFF";
            else TextProp.text = "ON";
        }
        private void EndGiftShow()
        {
            ItemManager.instance.giftItems.OnRecived();
            GiftMenu.transform.GetChild(0).GetComponent<Animator>().SetBool("OpenGift", false);
            ItemManager.instance.btn.gameObject.SetActive(false);
        }
        private IEnumerator PageMove()
        {
            while (true)
            {
                PageShop.localPosition = Vector3.Lerp(PageShop.localPosition, pagePosition, Time.deltaTime * 3);
                yield return null;
            }
        }
        public void NextPage()
        {
            pagePosition = new Vector3(-1586f, -30.65f, 0);
            StartCoroutine(PageMove());
        }
        public void BackPage()
        {
            pagePosition = new Vector3(116, -30.65f, 0);
            StartCoroutine(PageMove());
        }
        public void OpenURL() => Application.OpenURL("http://google.com/");
        public void BackShop() => ItemManager.instance.price = 0;
        #endregion

        #region OtherMethod      
        public void XPUiUpdated(float max)
        {
            foreach (Slider item in S_XpProgress)
            {
                item.maxValue = max;              
            }           
        }
        private void UpdateResources()
        {
            foreach (TMP_Text CurrCoin in T_Coin) CurrCoin.SetText(ScoreManager.instance.Coin.ToString());
            foreach (Slider CurrXp in S_XpProgress) CurrXp.value = ScoreManager.instance.Xp;
            foreach (TMP_Text CurrKey in T_Key) CurrKey.SetText(ScoreManager.instance.Key.ToString());
            foreach (TMP_Text coinIndex in T_CurrentCoin) coinIndex.SetText(ScoreManager.instance.CurrCoin.ToString());
            foreach (TMP_Text kill in T_Kill) kill.SetText(ScoreManager.instance.CurrKill.ToString());
        }
        public void DisableGroupBuyButton()
        {
            foreach (Transform item in GroupBuyButton)item.gameObject.SetActive(false);   
        }
        public void EnableGroupBuyButton()
        {
            foreach (Transform item in GroupBuyButton) item.gameObject.SetActive(true);
        }
        public void SetValueEnd()
        {            
            tr_Coin.GetComponent<TMP_Text>().text = ScoreManager.instance.CurrCoin.ToString() + "C";
            tr_Kill.GetComponent<TMP_Text>().text = ScoreManager.instance.CurrKill.ToString() + "XP";
            tr_End.GetComponent<TMP_Text>().text = ScoreManager.instance.CurrXp.ToString() + "XP";                     
        }
        public void ChangeLevelText(string text)
        {
            foreach (Text item in t_Level) item.text = "Level " + text;
        }
        public void ChangeStateLevel(string text)
        {
            foreach (var item in t_StateLevel) item.text = text;            
        }
        public IEnumerator LevelUpAnimShower(float delay)
        {
            yield return new WaitForSeconds(delay);
            LevelUpMenu.gameObject.SetActive(false);
        }
        #endregion
    }
}
