using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    ShopController Shop;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetID(int id, string name, int price, int state, ShopController shop, Sprite spr)
    {
        this.ID = id;
        this.Name = name;
        this.Price = price;
        this.State = state;
        this.Shop = shop;
        this.ImgReview.sprite = spr;
        TextButton.text = Price.ToString();
        TextName.text = name;
        CheckState();
    }

    public void OnClickBuy()
    {
        switch (State)
        {
            case 0:
                int coin = PlayerPrefs.GetInt("Coin", 0);
                // List<int> dataItem = DataController.GetArrayForKey("Items");
                Debug.Log(ID);
                if (coin >= Price)
                {
                    PlayerPrefs.SetInt("Ball" + ID.ToString(), 1);
                    State = 1;
                    coin -= Price;
                    PlayerPrefs.SetInt("Coin", coin);
                    Shop.UpdateCoin();

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

    public void CheckState()
    {
        switch (State)
        {
            case 0:
                Btn.GetComponent<Image>().sprite = sprBtns[0];
                break;
            case 1:
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
        Debug.Log(this.ID);
        Shop.SetBall(this.ID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
