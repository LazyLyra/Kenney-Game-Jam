using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    private Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost; //walking cost from start node
    public int hCost; //heuristic cost to reach end node(est cost to reach assuming no walls
    public int fCost; // F = G + H

    public bool isWalkable;
    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    //overrides the ToString() version to own's modification
    public override string ToString() 
    {
        return x + "," + y;
    }
}
