using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Pathfinding pathfinding;

    [Header("References")]

    [Header("VariableView")]
    [SerializeField] int gridWidth = 35;
    [SerializeField] int gridHeight = 20;

    private Vector3 bottomLeftWorldPos;
    private void Start()
    {
        pathfinding = new Pathfinding(gridWidth, gridHeight);
        bottomLeftWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
        CreateGridVisual();
        for (int x = 16; x <= 19; x++)
        {
            for (int y = 4; y <= 15; y++)
            {
                pathfinding.GetGrid().GetGridObject(x, y).isWalkable = false;
                Vector3 pos = pathfinding.GetGrid().GetWorldPosition(x, y) + Vector3.one * 0.25f;
                Debug.DrawLine(pos + Vector3.left * 0.2f, pos + Vector3.right * 0.2f, Color.red, 100f);
                Debug.DrawLine(pos + Vector3.up * 0.2f, pos + Vector3.down * 0.2f, Color.red, 100f);
            }
        }

    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(bottomLeftWorldPos, out int startX, out int startY);//useless
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Vector3 start = pathfinding.GetGrid().GetWorldPosition(path[i].x, path[i].y);
                    Vector3 end = pathfinding.GetGrid().GetWorldPosition(path[i + 1].x, path[i + 1].y);
                    Debug.DrawRay(start, end-start, Color.green, 5f);
                }
            }
        }

    }
    void CreateGridVisual()
    {
        float cellSize = pathfinding.cellSize;

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