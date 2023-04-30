using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilePainter : MonoBehaviour
{
    [SerializeField]
    Tilemap map;
    [SerializeField]
    RuleTile wallTile;
    [SerializeField]
    RuleTile groundTile;

    public void Paint(LevelGenerator level)
    {
        Clear();
        paintPath(level.getPathPosition());
        paintWall(level.getWallPosition());
        paintObstacle(level.getObstaclePosition());
    }

    void paintPath(HashSet<Vector2Int> pathPosition)
    {
        foreach (var position in pathPosition)
        {
            var paintPosition = map.WorldToCell((Vector3Int)position);
            map.SetTile(paintPosition, groundTile);
        }
    }
    void paintWall(HashSet<Vector2Int> wallPosition)
    {
        foreach (var position in wallPosition)
        {
            var paintPosition = map.WorldToCell((Vector3Int)position);
            map.SetTile(paintPosition, wallTile);
        }
    }

    void paintObstacle(HashSet<Vector2Int> obstaclePosition)
    {
        foreach (var position in obstaclePosition)
        {
            var paintPosition = map.WorldToCell((Vector3Int)position);
            map.SetTile(paintPosition, wallTile);
        }
    }

    void Clear()
    {
        map.ClearAllTiles();
    }
}
