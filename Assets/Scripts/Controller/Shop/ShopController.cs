using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopController : MonoBehaviour
{
    public TextAsset textJSON;
    public GameObject PrefabItem;
    public Transform BaseItem;
    public Text TextCoin;
    public MeshRenderer BallReview;
    public Texture2D[] TextureBall;
    public Sprite[] SprBall;
    public Text TextPage;

    int idPage = 0;
    bool canClick = true;
    public int maxPage;
    [System.Serializable]
    public class Item
    {
        public int ID;
        public string Name;
        public int Price;
    }
    [System.Serializable]
    public class ListItem
    {
        public Item[] Ball;
    }

    public ListItem listItem = new ListItem();

    void Start()
    {
        UpdateCoin();
        PlayerPrefs.SetInt("Coin", 5000);
        PlayerPrefs.SetInt("Ball0", 1);
        BallReview.material.mainTexture = TextureBall[PlayerPrefs.GetInt("BallUse", 0)];
        listItem = JsonUtility.FromJson<ListItem>(textJSON.text);
        // List<int> list = DataController.GetArrayForKey("Items");
        for (int i = 0; i < listItem.Ball.Length; i++)
        {
            // if (list.Count < listItem.Ball.Length)
            // {
            //     list.Add(0);
            // }
            Item item = listItem.Ball[i];
            int s = 0;
            bool idAds = false;
            if (PlayerPrefs.GetInt("Ball" + item.ID.ToString(), 0) == 1)
            {
                s = 1;
                if (PlayerPrefs.GetInt("BallUse", 0) == item.ID)
                {
                    s = 2;
                }
            }
            if (i > 5) {
                idAds = true;
            }
            OnCreateItem(item.ID, item.Name, item.Price, s, SprBall[item.ID], idAds);
        }
        Debug.Log(listItem.Ball);
    }

    public void OnClickNext()
    {
        if (idPage == maxPage || !canClick) { return; }
        canClick = false;
        idPage++;
        BaseItem.DOLocalMoveX(BaseItem.localPosition.x - 340, 0.5f).OnComplete(() =>
        {
            canClick = true;
        });
        UpdateTextPage();
    }

    public void OnClickBack()
    {
        if (idPage == 0 || !canClick) { return; }
        canClick = false;
        idPage--;
        BaseItem.DOLocalMoveX(BaseItem.localPosition.x + 340, 0.5f).OnComplete(() =>
        {
            canClick = true;
        });
        UpdateTextPage();
    }

    public void UpdateTextPage()
    {
        TextPage.text = (idPage + 1).ToString() + " OF " + (maxPage + 1).ToString();
    }

    public void UpdateCoin()
    {
        TextCoin.text = PlayerPrefs.GetInt("Coin", 0).ToString();
    }

    void OnCreateItem(int id, string name, int price, int s, Sprite spr, bool isAds)
    {
        GameObject item = Instantiate(PrefabItem);
        item.transform.SetParent(BaseItem);
        item.GetComponent<ItemShop>().SetID(id, name, price, s, this, spr, isAds);
    }

    public void SetBall(int id)
    {
        BallReview.material.mainTexture = TextureBall[id];
    }

    public void CheckItem()
    {
        for (int i = 0; i < BaseItem.childCount; i++)
        {
            ItemShop item = BaseItem.GetChild(i).GetComponent<ItemShop>();
            if (i == PlayerPrefs.GetInt("BallUse", 0))
            {
                item.BGChoose.SetActive(true);
            }
            else
            {
                item.BGChoose.SetActive(false);
                if (PlayerPrefs.GetInt("Ball" + i.ToString(), 0) == 1)
                {
                    item.State = 1;
                    item.CheckState();
                }
            }
        }
        GameController.Instance.SetBallTexture();
    }

    public void OnClickMenu()
    {
        gameObject.SetActive(false);
    }

    public void OnClickWatch()
    {
        FindObjectOfType<IronSourceAdsController>().ShowVideoAds(() =>
        {
        }, () =>
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + 500);
            UpdateCoin();
        });
    }

    // Update is called once per frame
    void Update()
    {
        BallReview.transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);
    }
}
