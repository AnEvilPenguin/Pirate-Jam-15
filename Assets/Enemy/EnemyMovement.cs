using Assets;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float UpdateTime = 1f;
    public CompassDirection Direction;
    public bool IsMoving = false;

    private Vector2 _lastPosition;
    private float _timer;

    private void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_timer <= 0)
        {
            ResetTimer();
            ProcessMovement();
        }
            
        _timer -= Time.deltaTime;
    }

    private void ResetTimer()
    {
        _timer = UpdateTime;
    }

    private void ProcessMovement()
    {
        var direction = (Vector2)transform.position - _lastPosition;

        var abs = direction.Abs();

        var magnitude = abs.x + abs.y;

        var xPercent = abs.x / magnitude * 100;
        var yPercent = abs.y / magnitude * 100;

        var diagonal = xPercent > 25 && yPercent > 25;
        var yDominant = abs.y >= abs.x;

        if (magnitude > 0.1f)
        {
            Direction = GetCompassDirection(direction, yDominant, diagonal);
            IsMoving = true;
        }
        else
            IsMoving = false;


        _lastPosition = transform.position;
    }

    private CompassDirection GetCompassDirection(Vector2 direction, bool yDominant, bool isdiagonal)
    {
        bool isNorth = direction.y > 0;
        bool isEast = direction.x > 0;


        if (yDominant && isNorth)
        {
            if (isdiagonal && isEast)
                return CompassDirection.NorthEast;
            else if (isdiagonal)
                return CompassDirection.NorthWest;

            return CompassDirection.North;
        }
        else if (yDominant)
        {
            if (isdiagonal && isEast)
                return CompassDirection.SouthEast;
            else if (isdiagonal)
                return CompassDirection.SouthWest;

            return CompassDirection.South;
        }
        else if (isEast)
        {
            if (isdiagonal && isNorth)
                return CompassDirection.NorthEast;
            else if (isdiagonal)
                return CompassDirection.SouthEast;

            return CompassDirection.East;
        }

        if (isdiagonal && isNorth)
            return CompassDirection.NorthWest;
        else if (isdiagonal)
            return CompassDirection.SouthWest;

        return CompassDirection.West;
    }
}
