using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    // Item Slot
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int maxStackSize = 99;

    // Selected Item
    [SerializeField] public GameObject selectedPanel;
    public bool isItemSelected;

    public InventoryManager inventoryManager;

    public void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();

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

        if (selectedPanel != null)
            selectedPanel.SetActive(false);
    }

    public void AddItem(ItemDetails item)
    {
        itemName = item.ItemName;
        itemIcon = item.ItemIcon;
        AddQuantity(item.Quantity);
    }

    // Check conditions for stacking items in the same slot
    public bool CanStackWith(string otherItemName)
    {
        // Slot bisa di-stack kalau nama item sama dan belum mencapai max stack
        return itemName == otherItemName && itemQuantity > 0 && itemQuantity < maxStackSize;
    }

    public void AddQuantity(int amount)
    {
        itemQuantity += amount;
        isFull = itemQuantity >= maxStackSize;

        if (quantityText != null)
        {
            quantityText.text = itemQuantity.ToString();
            quantityText.enabled = true;
        }

        if (itemImage != null && itemIcon != null)
        {
            itemImage.sprite = itemIcon;
            itemImage.color = Color.white;
            itemImage.enabled = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
    }

    private void LeftClick()
    {
        Debug.Log("Left Clicked on " + itemName);
        if (inventoryManager != null)
            inventoryManager.DesselectAllSlot();
        if (selectedPanel != null)
        {
            selectedPanel.SetActive(true);
            isItemSelected = true;
        }
    }

    private void RightClick()
    {
        Debug.Log("Right Clicked on " + itemName);
    }
}
