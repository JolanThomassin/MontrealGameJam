
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEditor.VersionControl;
using System.Linq;

public class LevelGenerator : SerializedMonoBehaviour
{
    public int Widith;
    public int Height;
    public int NumberObstacle;

    HashSet<Vector2Int> PathPosition = new HashSet<Vector2Int>();
    HashSet<Vector2Int> WallPosition = new HashSet<Vector2Int>();
    HashSet<Vector2Int> ObstaclePosition = new HashSet<Vector2Int>();

    [SerializeField]
    private TilePainter tilePainter;

    [Button("Generate")]
    public void Generate()
    {
        Clear();
        GenerateRoom(Vector2Int.zero, Widith, Height);
        ExtendLevel();
        GenerateWall();
        GenerateObstacle();
        tilePainter.Paint(this);
    }

    void GenerateRoom(Vector2Int center, int Width, int height)
    {
        for (int x = center.x - Width; x < center.x + Width; x++)
        {
            for (int y = center.y - height; y < center.y + height; y++)
            {
                PathPosition.Add(new Vector2Int(x, y));
            }
        }
    }

    void ExtendLevel()
    {
        List<Vector2Int> allDirection = new List<Vector2Int> { Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up };
        System.Random random = new System.Random();
        foreach (Vector2Int direction in allDirection)
        {
            Vector2Int nextCenter = new Vector2Int();
            if(direction == Vector2Int.left || direction == Vector2Int.right)
            { 
                int distance = random.Next(Widith/2, Widith);
                nextCenter = Vector2Int.zero + direction * distance;
            }
            else if(direction == Vector2Int.up || direction == Vector2Int.down)
            {
                int distance = random.Next(Height/2, Height);
                nextCenter = Vector2Int.zero + direction * distance;
            }

            GenerateRoom(nextCenter, random.Next(Widith/2, Widith), random.Next(Height/2, Height));
        }
    }

    void GenerateWall()
    {
        List<Vector2Int> allDirection = new List<Vector2Int> { Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up , new Vector2Int(1,1), new Vector2Int(-1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1) };
        foreach (var position in PathPosition)
        {
            foreach (var direction in allDirection)
            {
                Vector2Int newPos = position + direction;
                if(!PathPosition.Contains(newPos))
                {
                    WallPosition.Add(position + direction);
                }
                
            }
        }
    }

    void GenerateObstacle()
    {
        System.Random random = new System.Random();
        
        foreach (var position in PathPosition)
        {

        }
    }

    public HashSet<Vector2Int> getPathPosition()
    {
        return PathPosition;
    }

    public HashSet<Vector2Int> getWallPosition()
    {
        return WallPosition;
    }


    void Clear()
    {
        PathPosition.Clear();
        WallPosition.Clear();
    }
}
