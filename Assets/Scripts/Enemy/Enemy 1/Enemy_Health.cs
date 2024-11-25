using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField]private int currentHealth;

    public ItemDrop itemDrop;
    public Transform enemyTransform;

    private void Start()
    {
        currentHealth = maxHealth;
        itemDrop = GameObject.Find("GameManager").GetComponent<ItemDrop>();
        enemyTransform = transform;
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log(damageAmount);
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 在这里写下敌人死亡的逻辑
        Debug.Log("Enemy died!");
        itemDrop.DropKey(enemyTransform);
        itemDrop.DropHealingPotion(enemyTransform);

        // 销毁敌人游戏对象
        Destroy(gameObject);
    }
}
