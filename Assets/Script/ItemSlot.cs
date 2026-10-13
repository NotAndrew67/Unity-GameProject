using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // Item Data
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    // Item Slot
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    void Start()
    {
        if (!isFull)
        {
            if (quantityText != null)
            {
                quantityText.text = "";
                quantityText.enabled = false;
            }
            if (itemImage != null && itemIcon == null)
            {
                itemImage.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void AddItem(ItemDetails item)
    {
        itemName = item.ItemName;
        itemQuantity = item.Quantity;
        itemIcon = item.ItemIcon;
        isFull = true;

        if (quantityText != null)
        {
            quantityText.text = itemQuantity.ToString();
            quantityText.enabled = true;
        }

        if (itemImage != null)
        {
            itemImage.sprite = itemIcon;
            itemImage.color = Color.white;
            itemImage.enabled = true;
        }
    }
}
