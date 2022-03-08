using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public TextAsset textJSON;
    public GameObject PrefabItem;
    public Transform BaseItem;
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
        DataController.setArrayForKey("Items", "");
        Debug.Log(DataController.getArrayForKey("Items"));
        listItem = JsonUtility.FromJson<ListItem>(textJSON.text);
        // Item levelData = objectList.level[StaticData.level];
    
        for (int i = 0; i < listItem.Ball.Length; i++)
        {
            Item item = listItem.Ball[i];
            OnCreateItem(item.ID, item.Name, item.Price);
        }
        Debug.Log(listItem.Ball);
    }

    void OnCreateItem(int id, string name, int price) {
        GameObject item = Instantiate(PrefabItem);
        item.transform.SetParent(BaseItem);
        item.GetComponent<ItemShop>().SetID(id, name, price);
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
