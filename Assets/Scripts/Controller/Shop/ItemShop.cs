using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public Image ImgReview;
    public Text TextName;
    public GameObject Btn;
    int ID = 0;
    string Name;
    int Price = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetID(int id, string name, int price) {
        this.ID = id;
        this.Name = name;
        this.Price = price;
        TextName.text = name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
