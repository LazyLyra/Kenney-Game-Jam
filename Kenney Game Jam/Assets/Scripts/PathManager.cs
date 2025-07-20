using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int gridWidth = 40;
    [SerializeField] private int gridHeight = 40;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Vector3 gridOrigin = new Vector3(-19, -19, 0);
    [SerializeField] private Vector2Int[] blockedCells;
    [SerializeField] private Tilemap buildingTilemap;
    public static PathManager Instance { get; private set; }
    
    public Pathfinding pathfinder { get; private set; }
    public List<PathNode> StationPath { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        pathfinder = new Pathfinding(gridWidth, gridHeight, cellSize);
        var grid = pathfinder.GetGrid();
        var bounds = buildingTilemap.cellBounds;
        Vector3 exception = new Vector3(-1.47f, -1.55f, 0);
        grid.GetXY(exception, out int sx, out int sy);
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (buildingTilemap.HasTile(pos))
            {
                Vector3 worldPos = buildingTilemap.CellToWorld(pos)
                                 + buildingTilemap.tileAnchor;

                grid.GetXY(worldPos, out int x, out int y);

                if (x >= 0 && y >= 0 && x < grid.GetWidth() && y < grid.GetHeight())
                {
                    if(x == sx && y == sy)
                    {
                        continue;
                    }
                    var node = grid.GetGridObject(x, y);
                    node.isWalkable = false;
                }
            }
        }
    }

    public void ComputeStationPath(Vector3 stationPos)
    {
        var grid = pathfinder.GetGrid();
        grid.GetXY(stationPos, out int tx, out int ty);
        grid.GetXY(new Vector3(0, 0), out int sx, out int sy);

        StationPath = pathfinder.FindPath(sx, sy, tx, ty);
    }
    private void Start()
    {
        //DebugDrawUnwalkable();
        //DebugDrawFullGrid();
    }
    private void DebugDrawFullGrid()
    {
        var grid = pathfinder.GetGrid();
        float cs = grid.GetCellSize();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Vector3 worldPos = grid.GetWorldPosition(x, y) + new Vector3(cs, cs) * 1f;
                var node = grid.GetGridObject(x, y);
                Color color = node.isWalkable ? Color.green : Color.red;

                Debug.DrawLine(worldPos + new Vector3(-cs / 2f, -cs / 2f), worldPos + new Vector3(-cs / 2f, cs / 2f), color, 100f);
                Debug.DrawLine(worldPos + new Vector3(-cs / 2f, cs / 2f), worldPos + new Vector3(cs / 2f, cs / 2f), color, 100f);
                Debug.DrawLine(worldPos + new Vector3(cs / 2f, cs / 2f), worldPos + new Vector3(cs / 2f, -cs / 2f), color, 100f);
                Debug.DrawLine(worldPos + new Vector3(cs / 2f, -cs / 2f), worldPos + new Vector3(-cs / 2f, -cs / 2f), color, 100f);
            }
        }
    }
    public void DebugDrawUnwalkable()
    {
        var grid = Instance.pathfinder.GetGrid(); 
        float cs = grid.GetCellSize();
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                var node = grid.GetGridObject(x, y);
                if (!node.isWalkable)
                { 
                    Vector3 worldPos = grid.GetWorldPosition(x, y) + Vector3.one * cs * 1f;
                    float s = cs * 0.5f;
                    Debug.DrawLine(worldPos + Vector3.up * s + Vector3.left * s,
                                   worldPos + Vector3.down * s + Vector3.right * s,
                                   Color.red, 100f, false);
                    Debug.DrawLine(worldPos + Vector3.up * s + Vector3.right * s,
                                   worldPos + Vector3.down * s + Vector3.left * s,
                                   Color.red, 100f, false);
                }
            }
        }
    }

}
