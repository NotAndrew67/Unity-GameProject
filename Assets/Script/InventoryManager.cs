using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, GrabableItem
{
    private InputAction inventoryAction;
    [UnityEngine.Serialization.FormerlySerializedAs("inventoryCanvas")]
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int slotCount = 16;
    private bool InventoryActive;
    public ItemSlot[] itemSlot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateSlots();
        if (InventoryCanvas != null)
        {
            InventoryCanvas.SetActive(false);
        }
        inventoryAction = InputSystem.actions != null ? InputSystem.actions.FindAction("Inventory") : null;
        InventoryActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool triggered = false;
        if (inventoryAction != null)
        {
            triggered = inventoryAction.triggered;
        }
        else if (Keyboard.current != null)
        {
            // Fallback tombol 'I' atau 'Tab' jika Action "Inventory" belum di-set di Input Actions asset
            triggered = Keyboard.current.iKey.wasPressedThisFrame || Keyboard.current.tabKey.wasPressedThisFrame;
        }

        if (triggered && InventoryCanvas != null)
        {
            if (!InventoryActive)
            {
                InventoryCanvas.SetActive(true);
                InventoryActive = true;
                Time.timeScale = 0f;
                Debug.Log("Inventory Muncul");
            }
            else
            {
                InventoryCanvas.SetActive(false);
                Time.timeScale = 1f;
                InventoryActive = false;
                Debug.Log("Inventory Hilang");
            }
        }
    }

    // Build the slot objects at runtime so no manual wiring is needed in the scene
    private void CreateSlots()
    {
        if (InventoryCanvas == null)
        {
            Debug.LogError("InventoryCanvas is not assigned in InventoryManager.", this);
            return;
        }

        ItemSlot slotPrefab = Resources.Load<ItemSlot>("ItemSlot");
        if (slotPrefab == null)
        {
            Debug.LogError("ItemSlot prefab was not found in a Resources folder.", this);
            itemSlot = new ItemSlot[0];
            return;
        }

        // Put the slots inside the GridLayoutGroup container (InventorySlots), if there is one
        Transform parent = InventoryCanvas.transform;
        GridLayoutGroup grid = InventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);
        if (grid != null)
        {
            parent = grid.transform;
        }

        // Bersihkan slot dummy placeholder jika ada di scene editor
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        itemSlot = new ItemSlot[slotCount];
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(slotPrefab, parent);
        }
    }

    public void AddItem(ItemDetails item)
    {
        Debug.Log("Item added to inventory: " + item.ItemName + " Quantity: " + item.Quantity);

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogWarning("Inventory has no slots, '" + item.ItemName + "' was lost.");
            return;
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }

        Debug.LogWarning("Inventory is full, '" + item.ItemName + "' was lost.");
    }
}
