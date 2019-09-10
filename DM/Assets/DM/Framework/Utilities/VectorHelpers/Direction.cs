using UnityEngine;

namespace GameFramework.Utilities.VectorHelpers
{
    public enum Directions : int
    {
        Forward = 0,
        ForwardRight = 1,
        Right = 2,
        RightBack = 3,
        Back = 4,
        BackLeft = 5,
        Left = 6,
        LeftForward = 7,
        Up = 8,
        UpFront = 9,
        UpRight = 10,
        UpBack = 11,
        UpLeft = 12,
        Down = 13,
        DownFront = 14,
        DownRight = 15,
        DownBack = 16,
        DownLeft = 18
    }

    public static class Direction
    {
        public static Vector3 GetDirection(Directions direction, Transform transform)
        {
            switch (direction)
            {
                case Directions.Forward:
                    return transform.forward;
                case Directions.ForwardRight:
                    return transform.right + transform.forward;
                case Directions.Right:
                    return transform.right;
                case Directions.RightBack:
                    return transform.right + -transform.forward;
                case Directions.Back:
                    return -transform.forward;
                case Directions.BackLeft:
                    return -transform.forward + -transform.right;
                case Directions.Left:
                    return -transform.right;
                case Directions.LeftForward:
                    return -transform.right + transform.forward;
                case Directions.Up:
                    return transform.up;
                case Directions.UpFront:
                    return transform.up + transform.forward;
                case Directions.UpRight:
                    return transform.up + transform.right;
                case Directions.UpBack:
                    return transform.up + -transform.forward;
                case Directions.UpLeft:
                    return transform.up + -transform.right;
                case Directions.Down:
                    return -transform.up;
                case Directions.DownFront:
                    return -transform.up + transform.forward;
                case Directions.DownRight:
                    return -transform.up + transform.right;
                case Directions.DownBack:
                    return -transform.up + -transform.forward;
                case Directions.DownLeft:
                    return -transform.up + -transform.right;
            }

            return Vector3.zero;
        }
    }
}
