using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GoToTarget : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject Target;

    [Header("Variables")]
    public float moveSpeed = 5f;
    private List<PathNode> path;
    private int currentIndex = 0;
    private bool isMoving = false;
    private Pathfinding pathfinder;

    [SerializeField] int gridWidth = 20;
    [SerializeField] int gridHeight = 40;

    private Vector3 bottomLeftWorldPos;
    private void Start()
    {
        pathfinder = new Pathfinding(35, 20);
        bottomLeftWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
        CreateGridVisual();
        pathfinder.GetGrid().GetXY(transform.position, out int startX, out int startY);
        Debug.Log(startX+ ","+ startY);
        Debug.Log(Target.transform.position);
        pathfinder.GetGrid().GetXY(Target.transform.position, out int x, out int y);
        Debug.Log(x + "," + y);
        for (int a = 16; a <= 19; a++)
        {
            for (int b = 4; b <= 15; b++)
            {
                pathfinder.GetGrid().GetGridObject(a, b).isWalkable = false;
                Vector3 pos = pathfinder.GetGrid().GetWorldPosition(a, b) + Vector3.one * 0.25f;
                Debug.DrawLine(pos + Vector3.left * 0.2f, pos + Vector3.right * 0.2f, Color.red, 100f);
                Debug.DrawLine(pos + Vector3.up * 0.2f, pos + Vector3.down * 0.2f, Color.red, 100f);
            }
        }
        SetPath(pathfinder.FindPath(startX, startY, x, y));
        
    }
    public void SetPath(List<PathNode> PathToTarget)
    {
        path = PathToTarget;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 start = pathfinder.GetGrid().GetWorldPosition(path[i].x, path[i].y);
            Vector3 end = pathfinder.GetGrid().GetWorldPosition(path[i + 1].x, path[i + 1].y);
            Debug.DrawRay(start, end - start, Color.green, 5f);
        }

        currentIndex = 0;
        isMoving = path != null && path.Count > 0;
    }

    private void Update()
    {
        if (!isMoving || path == null || currentIndex >= path.Count) return;

        Vector3 cellBasePos = pathfinder.GetGrid().GetWorldPosition(path[currentIndex].x, path[currentIndex].y);
        Vector3 targetPosition = cellBasePos + new Vector3(0.25f, 0.25f);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            currentIndex++;
            if (currentIndex >= path.Count)
            {
                isMoving = false;
            }
        }
    }
    void CreateGridVisual()
    {
        float cellSize = pathfinder.cellSize;

        for (int x = 0; x <= gridWidth; x++)
        {
            Vector3 start = bottomLeftWorldPos + new Vector3(x * cellSize, 0);
            Vector3 end = bottomLeftWorldPos + new Vector3(x * cellSize, gridHeight * cellSize);
            CreateLine(start, end);
        }

        for (int y = 0; y <= gridHeight; y++)
        {
            Vector3 start = bottomLeftWorldPos + new Vector3(0, y * cellSize);
            Vector3 end = bottomLeftWorldPos + new Vector3(gridWidth * cellSize, y * cellSize);
            CreateLine(start, end);
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineGO = new GameObject("GridLine", typeof(LineRenderer));
        LineRenderer lr = lineGO.GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = 2;
        lr.useWorldSpace = true;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.gray;
        lr.endColor = Color.gray;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

}
