using MainProject.Models;
using MainProject.Models.Basics;

namespace MainProject.Objects
{
    public class Camera
    {
        public Point Position { get; set; }
        public Normal Direction { get; set; }
        public int FieldOfView { get; set; }

        public float Distance { get; set; }
        public int Height = 100;
        public int Width = 100;

        public Transformator CameraTransformator;
        public Camera() { }

        public Camera(Point position, Normal direction, int fieldOfView, float distance, Transformator? cameraTransformator = null)
        {
            Position = position;
            Direction = direction;
            FieldOfView = fieldOfView;
            Distance = distance;
            CameraTransformator = cameraTransformator ?? new Transformator();
        }

        public Point[,] GetImaginaryScreen()
        {
            Vector rightScreenDirection = Vector.Cross(new Vector(0, 0, 1), Direction).Normalize();

            Vector upScreenDirection = Vector.Cross(Direction, rightScreenDirection).Normalize();

            var ImaginaryScreen = new Point[Width, Height];

            float alpha = FieldOfView / 2;

            float leftOffset = (float)Math.Tan((Math.PI / 180) * alpha) * Distance;
            float bottomOffset = leftOffset * (Width / Height);

            float horizontalDistanceBetweenPixels = leftOffset / Width * 2;
            float verticalDistanceBetweenPixels = bottomOffset / Height * 2;


            Point leftBottomPoint = Position - rightScreenDirection.Scale(leftOffset) - upScreenDirection.Scale(bottomOffset);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ImaginaryScreen[x, y] = leftBottomPoint + Direction.Scale(Distance) + rightScreenDirection.Scale(x * horizontalDistanceBetweenPixels) + upScreenDirection.Scale(y * verticalDistanceBetweenPixels);
                    ImaginaryScreen[x, y] = CameraTransformator.Apply(ImaginaryScreen[x, y]);
                }
            }

            Position = CameraTransformator.Apply(Position);
            return ImaginaryScreen;
        }
    }
}
