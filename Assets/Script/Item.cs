using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemDetails ItemDetails;
    private GrabableItem itemCollector;
    
    void Awake()
    {
        FindItemCollector();

        if (itemCollector == null)
        {
            Debug.LogError("No active component implementing GrabableItem was found in the scene.", this);
        }
    }

    private void FindItemCollector()
    {
        foreach (MonoBehaviour component in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (component is GrabableItem collector)
            {
                itemCollector = collector;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ItemDetails != null)
        {
            if (itemCollector == null)
            {
                FindItemCollector();
            }

            if (itemCollector != null)
            {
                itemCollector.AddItem(ItemDetails);
                Destroy(gameObject);
            }
        }
    }
}