using UnityEngine;

[System.Flags]
public enum GameInstantiateItemFlags
{
    None = 0,
    Grabbable = 1 << 0,
}

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game")]
public class Game : ScriptableObject
{
    static Game _instance;

    public static Game Instance =>
        _instance ??= Resources.Load<Game>("Game");

    public static GameObject base_prefab => Instance._basePrefab;

    [SerializeField] GameObject _basePrefab;

    public static GameObject InstantiateItem(ItemData item, Transform parent, GameInstantiateItemFlags flags = GameInstantiateItemFlags.None)
    {
        GameObject instance = Instantiate(Game.base_prefab, parent, false);
        instance.transform.GetChild(0).GetComponent<MeshFilter>().mesh = item.mesh;
        instance.transform.GetChild(0).GetComponent<MeshRenderer>().materials = item.materials;
        if (flags.HasFlag(GameInstantiateItemFlags.Grabbable))
        {
            var bc = instance.AddComponent<BoxCollider>();
            bc.center = new Vector3();
            var radius = item.radius();
            var height = item.height();
            bc.center = new Vector3(0f, height / 2f, 0f);
            bc.size = new Vector3(radius * 2, height, radius * 2);

            if (item.onReleaseOverride != null)
            {
                instance.AddComponent(item.onReleaseOverride.GetClass());
            }
            else
            {
                instance.AddComponent<ItemRelease>().Initialize(item);
            }
        }
        return instance;
    }
}