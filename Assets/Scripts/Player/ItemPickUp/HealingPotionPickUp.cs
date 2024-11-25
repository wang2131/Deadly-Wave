using UnityEngine;

public class HealingPotionPickUp : MonoBehaviour
{
    public SO_Inventory inventory;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            //Todo 加血相关逻辑

            inventory.healingPotionHeld++;
            Destroy(gameObject);
        }
    }
}
