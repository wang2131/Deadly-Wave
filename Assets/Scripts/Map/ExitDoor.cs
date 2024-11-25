using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public SO_Inventory inventory;
    public PlayerSpawn playerSpawn;
    public EnemySpawn enemySpawn;

    private void Start()
    {
        playerSpawn = GameObject.Find("Generator").GetComponent<PlayerSpawn>();
        enemySpawn = GameObject.Find("Generator").GetComponent<EnemySpawn>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        if (inventory.exitKey)
        {
            if (inventory.dungeonLevel < inventory.totalDungeonLevel)
            {
                playerSpawn.GeneratorDungeon();
                Invoke(nameof(enemySpawn.SpawnEnemy), 0.1f);
                inventory.dungeonLevel ++;
                inventory.exitKey = false;
            }
            else
            {
                inventory.ClearInventory();
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
