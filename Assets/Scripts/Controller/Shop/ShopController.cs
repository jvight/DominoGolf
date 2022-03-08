using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public TextAsset textJSON;
    public GameObject PrefabItem;
    public Transform BaseItem;
    public Text TextCoin;
    public Material BallReview;
    public Texture2D[] TextureBall;
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
            if (PlayerPrefs.GetInt("Ball" + item.ID.ToString(), 0) == 1)
            {
                s = 1;
                if (PlayerPrefs.GetInt("BallUse", -1) == item.ID)
                {
                    s = 2;
                }
            }
            OnCreateItem(item.ID, item.Name, item.Price, s);
        }
        Debug.Log(listItem.Ball);
    }

    public void UpdateCoin()
    {
        TextCoin.text = PlayerPrefs.GetInt("Coin", 0).ToString();
    }

    void OnCreateItem(int id, string name, int price, int s)
    {
        GameObject item = Instantiate(PrefabItem);
        item.transform.SetParent(BaseItem);
        item.GetComponent<ItemShop>().SetID(id, name, price, s, this);
    }

    public void SetBall(int id) {
        BallReview.SetTexture(id.ToString(), TextureBall[id]);
    }

    public void OnClickMenu()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
