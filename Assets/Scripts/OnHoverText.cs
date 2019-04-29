using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnHoverText : EventTrigger
{   
    public GameObject onHover;
    public Item shopItem;

    public Text itemName;
    public Text itemDescription;

    public bool itemBought = false;

    void Start()
    {
        itemName = onHover.transform.Find("Name").GetComponent<Text>();
        itemDescription = onHover.transform.Find("Text").GetComponent<Text>();
        transform.GetChild(0).Find("Sprite").GetComponent<Image>().sprite = shopItem.shopSprite;
    }

    public override void OnPointerEnter(PointerEventData data)
    {        
        itemName.text = shopItem.objectName;
        itemDescription.text = shopItem.description + "\n\n"
            + (itemBought || shopItem.price == 0 ? "ALREADY BOUGHT" : "Price: " + shopItem.price + "\n")
            ;
        onHover.transform.position = Input.mousePosition + new Vector3(onHover.GetComponent<RectTransform>().sizeDelta.x / 2, 0, 0);
        onHover.SetActive(true);
    }

    public override void OnPointerExit(PointerEventData data)
    {
        onHover.SetActive(false);
    }
}
