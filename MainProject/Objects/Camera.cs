using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Camera : IBaseObject
    {
        public Point Position { get; set; }
        public Vector Direction { get; set; }

        public Camera() { }

        public Camera(Point position, Vector direction)
        {
            Position = position;
            Direction = direction;
        }

        public double Distance { get; set; }
        public int Height = 20;
        public int Width = 20;

        private Point TopLeft;
        private Point TopRight;
        private Point BottomLeft;

        public Camera(int scaleX, int scaleY)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;

            TopLeft = new Point(0, Height / 2.0, Width / 2.0);
            TopRight = new Point(0, Height / 2.0, -Width / 2.0);
            BottomLeft = new Point(0, -Height / 2.0, Width / 2.0);
        }


    }
}
