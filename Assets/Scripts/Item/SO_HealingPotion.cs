using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item/Healing_Potion", fileName = "new Healing_Potion")]
public class SO_HealingPotion : ScriptableObject
{
    public Sprite sprite;
    public Transform transform;
    public Rigidbody2D rigidbody2D;
    public int healingPower;
}
