using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName ="GamePrefabsContainer")]
public class GamePrefabsContainer : ScriptableObject
{
    [Header("Prefabs")]
    public Player PlayerPrefab;
    public ItemCollector ItemCollectorPrefab;
    public CanvasGroupHolder CanvasGroupHolder;
    public bool HaveEnemy;
    [ShowIf("HaveEnemy")]
    public Enemy Enemy;
}
