using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game")]
public class Game : ScriptableObject
{
    static Game _instance;

    public static Game Instance =>
        _instance ??= Resources.Load<Game>("Game");

    public static GameObject base_prefab => Instance._basePrefab;

    [SerializeField] GameObject _basePrefab;
}