using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemShop : MonoBehaviour
{
    public Image ImgReview;
    public Text TextName;
    public GameObject Btn;
    public Text TextButton;
    public Sprite[] sprBtns;
    public GameObject BGChoose;
    int ID = 0;
    string Name;
    int Price = 0;
    public int State = 0;
    public bool IsAds = false;
    bool IsComming;
    ShopController Shop;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetID(int id, string name, int price, int state, ShopController shop, Sprite spr, bool isAds, bool isComming)
    {
        this.ID = id;
        this.Name = name;
        this.Price = price;
        this.State = state;
        this.Shop = shop;
        this.IsAds = isAds;
        this.IsComming = isComming;
        TextButton.text = Price.ToString();
        TextName.text = name;
        if (spr != null)
        {
            this.ImgReview.sprite = spr;
        }
        CheckState();
    }

    public void OnClickBuy()
    {
        switch (State)
        {
            case 0:
                if (IsAds && PlayerPrefs.GetInt("Ball" + ID.ToString()) == 1)
                {
                    AdsController.Instance.ShowVideoAds(() =>
                        {
                        }, () =>
                        {
                            PlayerPrefs.SetInt("Ball" + ID.ToString(), 5);
                            Unlock();
                            State = 1;
                        });
                }
                int coin = PlayerPrefs.GetInt("Coin", 0);
                // List<int> dataItem = DataController.GetArrayForKey("Items");
                Debug.Log(ID);
                if (coin >= Price)
                {
                    PlayerPrefs.SetInt("Ball" + ID.ToString(), 5);
                    State = 1;
                    coin -= Price;
                    PlayerPrefs.SetInt("Coin", coin);
                    Shop.UpdateCoin();
                    GameController.Instance.uiController.UpdateTextCoin(coin);
                }
                break;
            case 1:
                State = 2;
                PlayerPrefs.SetInt("BallUse", this.ID);
                Shop.CheckItem();
                break;
            case 2:
                break;
        }
        CheckState();
    }

    void Unlock()
    {
        Debug.Log("??");
        ImgReview.DOColor(Color.white, 1);
    }
    public void CheckState()
    {
        switch (State)
        {
            case 0:
                Btn.GetComponent<Image>().sprite = sprBtns[0];
                if (IsAds)
                {
                    ImgReview.color = Color.black;
                    ImgReview.transform.GetChild(0).gameObject.SetActive(true);
                    if (PlayerPrefs.GetInt("Ball" + ID.ToString()) == 0)
                    {
                        Btn.SetActive(false);
                    }
                    else if (PlayerPrefs.GetInt("Ball" + ID.ToString()) == 1)
                    {
                        Btn.SetActive(true);
                        TextButton.transform.localPosition = new Vector3(0, TextButton.transform.localPosition.y);
                        Btn.transform.GetChild(1).gameObject.SetActive(false);
                        TextButton.text = "Unlock Now";
                    }
                }
                if (IsComming)
                {
                    ImgReview.color = Color.black;
                    ImgReview.transform.GetChild(0).gameObject.SetActive(true);
                    Btn.SetActive(false);
                }
                break;
            case 1:
                ImgReview.transform.GetChild(0).gameObject.SetActive(false);
                Btn.GetComponent<Image>().sprite = sprBtns[0];
                TextButton.transform.localPosition = new Vector3(0, TextButton.transform.localPosition.y);
                Btn.transform.GetChild(1).gameObject.SetActive(false);
                TextButton.text = "EQUIP";
                break;
            case 2:
                BGChoose.SetActive(true);
                Btn.GetComponent<Image>().sprite = sprBtns[1];
                TextButton.transform.localPosition = new Vector3(0, TextButton.transform.localPosition.y);
                Btn.transform.GetChild(1).gameObject.SetActive(false);
                TextButton.text = "EQUIPPED";
                break;
        }
    }

    public void OnClickChoose()
    {
        if (IsComming || IsAds && PlayerPrefs.GetInt("Ball" + ID.ToString()) != 5) { return; }
        Shop.SetBall(this.ID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
