
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;


public class LevelGenerator : SerializedMonoBehaviour
{
    public int Widith;
    public int Height;
    public int NumberObstacle;
    public int NumberAttemptMax;
    public int MinDistanceBetweenObstacleWall;
    public int MinDistanceBetweenObstaclePoint;

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
        GenerateObstaclePoint();
        ExtendObstacle();
        tilePainter.Paint(this);
    }

    void CreateStartRoom()
    {
        
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

    #region Obstacle
    private void GenerateObstaclePoint()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < NumberObstacle; i++)
        {
            bool pointFound = false;
            int attempts = 0;

            while (!pointFound && attempts < NumberAttemptMax)
            {
                attempts++;
                int index = random.Next(PathPosition.Count);
                Vector2Int newPoint = PathPosition.ElementAt(index);

                if (IsPointFarEnough(newPoint))
                {
                    ObstaclePosition.Add(newPoint);
                    pointFound = true;
                }
            }
        }
    }


    private bool IsPointFarEnough(Vector2 newPoint)
    {
        foreach (Vector2 existingPoint in ObstaclePosition)
        {
            if (Vector2.Distance(newPoint, existingPoint) < MinDistanceBetweenObstaclePoint)
            {
                return false;
            }
        }

        foreach (Vector2 position in WallPosition)
        {
            if (Vector2.Distance(newPoint, position) < MinDistanceBetweenObstaclePoint)
            {
                return false;
            }
        }

        return true;
    }

    private void ExtendObstacle()
    {
        System.Random random = new System.Random();
        HashSet<Vector2Int> ObstacleWall = new HashSet<Vector2Int>();
        foreach (var position in ObstaclePosition)
        {
            List<Vector2Int> allDirection = new List<Vector2Int> { Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up };
            int distance = random.Next(5,10);
            Vector2Int direction = allDirection[random.Next(0, 4)];
            HashSet<Vector2Int> currentObstacle = new HashSet<Vector2Int>();
            for (int i = 0; i < distance; i++)
            {
                Vector2Int nextPosition = position + direction * i;
                if(IsPointFarEnoughFromObstacle(nextPosition, ObstacleWall) && !WallPosition.Contains(nextPosition))
                {
                    currentObstacle.Add(nextPosition);
                }
                else
                {
                    break;
                }
                
            }
            int ExtendAgain = random.Next(0,1);
            if(ExtendAgain == 0)
            {
               
                distance = random.Next(8, 10);
                direction = allDirection[random.Next(0, 4)];
                for (int i = 0; i < distance; i++)
                {
                    Vector2Int nextPosition = position + direction * i;
                    if (IsPointFarEnoughFromObstacle(nextPosition, ObstacleWall) && !WallPosition.Contains(nextPosition))
                    {
                        currentObstacle.Add(nextPosition);
                    }
                    else 
                    {
                        break;
                    }
                }
            }
            ObstacleWall.UnionWith(currentObstacle);

        }
        ObstaclePosition.UnionWith(ObstacleWall);
        PathPosition.Except(ObstaclePosition);
    }

    private bool IsPointFarEnoughFromObstacle(Vector2 newPoint, HashSet<Vector2Int> ObstacleWall)
    {
        foreach (Vector2 existingPoint in ObstacleWall)
        {
            if (Vector2.Distance(newPoint, existingPoint) < MinDistanceBetweenObstacleWall)
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region Getter
    public HashSet<Vector2Int> getPathPosition()
    {
        return PathPosition;
    }

    public HashSet<Vector2Int> getWallPosition()
    {
        return WallPosition;
    }

    public HashSet<Vector2Int> getObstaclePosition()
    {
        return ObstaclePosition;
    }
    #endregion

    void Clear()
    {
        PathPosition.Clear();
        WallPosition.Clear();
        ObstaclePosition.Clear();
    }
}
