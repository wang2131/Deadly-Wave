using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item/Inventory", fileName = "new Item")]
public class SO_Inventory : ScriptableObject
{
    public int healingPotionHeld;
    public int bombHeld;
    public bool exitKey;
    
    // 关卡数量放这里一起写了
    public int dungeonLevel;
    public int totalDungeonLevel;

    public void ClearInventory()
    {
        healingPotionHeld = 0;
        bombHeld = 0;
        exitKey = false;
        
        dungeonLevel = 0;
    }
}
