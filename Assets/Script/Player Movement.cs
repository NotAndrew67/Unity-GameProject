using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    public float kecepatan = 5f;
    private Vector2 arahGerak; 
    private GameManager gameManager;
    public int skor = 0;


void Start()
{
    gameManager = FindFirstObjectByType<GameManager>();
}
  
    void OnMove(InputValue value)
    {
       
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
  
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);

  
        transform.position += arah * kecepatan * Time.deltaTime;
    }

   
void OnTriggerEnter2D(Collider2D other)
{
    if (other.GetComponent<Item>() != null)
    {
        return;
    }

    if (other.CompareTag("Coin"))
    {
        Destroy(other.gameObject);

        skor++;
        Debug.Log("Skor: " + skor);

        if (gameManager != null)
        {
            gameManager.AmbilKoin();
        }
    }
}

}