using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public SO_Inventory inventory;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            inventory.exitKey = true;
            Destroy(gameObject);
        }
    }
}
