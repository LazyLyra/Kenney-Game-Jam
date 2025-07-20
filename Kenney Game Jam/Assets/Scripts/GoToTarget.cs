using System.Collections.Generic;
using UnityEngine;

public class GoToTarget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject station;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float waypointTolerance = 0.01f;

    private Pathfinding pathfinder;
    private Grid<PathNode> grid;
    private List<PathNode> path;
    private int currentWaypoint = 0;
    private bool reachedStation;

    private enum TargetMode { ToStation, ToPlayer }
    private TargetMode mode = TargetMode.ToStation;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        station = GameObject.FindGameObjectWithTag("Station");

        pathfinder = PathManager.Instance.pathfinder;
        grid = pathfinder.GetGrid();

    }

    private void Start()
    {
        RecomputePath();  
    }

    private void Update()
    {
        bool shouldAttack = Vector3.Distance(transform.position, player.transform.position) < attackRange;
        if (!reachedStation) {
            TargetMode newMode = shouldAttack ? TargetMode.ToPlayer : TargetMode.ToStation;
            if (newMode != mode)
            {
                mode = newMode;
                RecomputePath();
            }
        }


        if (path == null)
        {
            if (mode == TargetMode.ToStation && PathManager.Instance.StationPath != null)
            {
                path = new List<PathNode>(PathManager.Instance.StationPath);
                currentWaypoint = 0;
            }
            else
            {
                return; 
            }
        }
        if (reachedStation || path == null || currentWaypoint >= path.Count) return;
        Vector3 targetPos = GridToWorld(path[currentWaypoint]) + Vector3.one;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < waypointTolerance)
        {
            currentWaypoint++;

            if (currentWaypoint >= path.Count && mode == TargetMode.ToStation)
            {
                reachedStation = true;
                path = null; 
            }
        }

    }

    private void RecomputePath()
    {
        grid.GetXY(transform.position, out int sx, out int sy);

        if (mode == TargetMode.ToPlayer)
        {
            grid.GetXY(player.transform.position, out int tx, out int ty);
            path = pathfinder.FindPath(sx, sy, tx, ty); // unique path
        }
        else if(!reachedStation)
        {
            path = PathManager.Instance.StationPath != null ?
                   new List<PathNode>(PathManager.Instance.StationPath) : null;
        }

        currentWaypoint = 0;
    }

    private Vector3 GridToWorld(PathNode node)
    {
        return grid.GetWorldPosition(node.x, node.y);
    }
}
