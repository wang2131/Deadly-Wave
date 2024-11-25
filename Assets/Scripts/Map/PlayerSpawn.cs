using Edgar.Unity;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private DungeonGeneratorGrid2D generator;

    private void Awake()
    {
        generator = GetComponent<DungeonGeneratorGrid2D>();
    }

    private void Start()
    {
        GeneratorDungeon();
    }

    public void GeneratorDungeon()
    {
        generator.Generate();
        Invoke(nameof(ChangeLayer), 0.1f);
        Invoke(nameof(SetSpawnPosition), 0.1f);
    }
    
    private void SetSpawnPosition()
    {
        var spawnPosition = GameObject.Find("SpawnPosition").transform.position;
        var player = GameObject.Find("Player");
        player.transform.position = spawnPosition;
    }

    private void ChangeLayer()
    {
        GameObject GeneratorLevel = GameObject.Find("Generated Level");
        Transform Tilemaps = GeneratorLevel.transform.Find("Tilemaps");
        Transform Walls = Tilemaps.transform.Find("Walls");
        Transform Collideable = Tilemaps.transform.Find("Collideable");

        Walls.gameObject.layer = LayerMask.NameToLayer("Ground");
        Collideable.gameObject.layer = LayerMask.NameToLayer("Ground");
    }

    // 设置角色位置至出生点
    // private void SetSpawnPosition(DungeonGeneratorLevelGrid2D level)
    // {
    //     foreach (var entranceRoomInstance in level.RoomInstances)
    //     {
    //         if (entranceRoomInstance == null)
    //         {
    //             throw new InvalidOperationException("Could not find Entrance room");
    //         }
    //
    //         var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;
    //
    //         // Find the spawn position marker
    //         var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");
    //
    //         // Move the player to the spawn position
    //         var player = GameObject.FindWithTag("Player");
    //         player.transform.position = spawnPosition.position;
    //     }
    // }
}
