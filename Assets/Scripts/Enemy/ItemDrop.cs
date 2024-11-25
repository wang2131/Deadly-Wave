using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDrop : MonoBehaviour
{
    public GameObject exitKeyPrefab;
    public GameObject healingPotionPrefab;

    [Header("掉落概率")]
    public int healingPotionDropRate;
    public int keyDropRate;

    public bool isKeyDrop;

    private void Start()
    {
        isKeyDrop = false;
    }

    // 检测敌人是否为最后一个
    private bool CheckEnemyIsLast()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy == null)
            { 
                return true;
            }
        }

        return false;
    }

    // 掉落血瓶方法
    public void DropHealingPotion(Transform enemyTransform)
    {
        int randomNum = Random.Range(0, 100);
            
        if(randomNum < healingPotionDropRate)
        {
            Instantiate(healingPotionPrefab, enemyTransform.position, Quaternion.identity);
        }
    }
    
    // 掉落钥匙方法
    public void DropKey(Transform enemyTransform)
    {
        if (!isKeyDrop)
        {
            if (CheckEnemyIsLast())
            {
                Instantiate(exitKeyPrefab, enemyTransform.position, Quaternion.identity);
                isKeyDrop = true;
            }
            else
            {
                DropKeyWith30Percent(enemyTransform);
            }
        }
    }

    private void DropKeyWith30Percent(Transform enemyTransform)
    {
        int randomNum = Random.Range(0, 100);
        Debug.Log("RandomNum" + randomNum);
            
        if(randomNum < keyDropRate)
        {
            Instantiate(exitKeyPrefab, enemyTransform.position, Quaternion.identity);
            isKeyDrop = true;
        }
    }
}
