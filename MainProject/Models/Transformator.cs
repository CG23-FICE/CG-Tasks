using MainProject.Models.Shapes;
using MainProject.Models.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Models
{
    public class Transformator
    {
        //Get some examples of matrix from http://www.songho.ca/opengl/gl_matrix.html
        private float[,] TransformationMatrix = new float[4, 4]
        {{ 1, 0, 0, 0 },
         { 0, 1, 0, 0 },
         { 0, 0, 1, 0 },
         { 0, 0, 0, 1 }};

        public float AngleRadX { get; private set; } = 0;
        public float SinX { get; private set; } = 0;
        public float CosX { get; private set; } = 1;

        public float AngleRadY { get; private set; } = 0;
        public float SinY { get; private set; } = 0;
        public float CosY { get; private set; } = 1;

        public float AngleRadZ { get; private set; } = 0;
        public float SinZ { get; private set; } = 0;
        public float CosZ { get; private set; } = 1;

        public float ShiftX { get; private set; } = 0;
        public float ShiftY { get; private set; } = 0;
        public float ShiftZ { get; private set; } = 0;

        public float ScaleX { get; private set; } = 1;
        public float ScaleY { get; private set; } = 1;
        public float ScaleZ { get; private set; } = 1;

        public void RotateAngleX(float angle) => RotateX(angle * (float)Math.PI / 180.0f, true);
        private void RotateX(float angleRad, bool update)
        {
            AngleRadX += angleRad;
            SinX = (float)Math.Sin(AngleRadX);
            CosX = (float)Math.Cos(AngleRadX);
            if (update) UpdateTransformation();
        }

        public void RotateAngleY(float angle) => RotateY(angle * (float)Math.PI / 180.0f, true);
        private void RotateY(float angleRad, bool update)
        {
            AngleRadY += angleRad;
            SinY = (float)Math.Sin(AngleRadY);
            CosY = (float)Math.Cos(AngleRadY);
            if (update) UpdateTransformation();
        }

        public void RotateAngleZ(float angle) => RotateZ(angle * (float)Math.PI / 180.0f, true);
        private void RotateZ(float angleRad, bool update)
        {
            AngleRadZ += angleRad;
            SinZ = (float)Math.Sin(AngleRadZ);
            CosZ = (float)Math.Cos(AngleRadZ);
            if (update) UpdateTransformation();
        }

        public void Rotate(Vector angles)
        {
            RotateX(angles.X, false);
            RotateY(angles.Y, false);
            RotateZ(angles.Z, true);
        }

        public void MoveX(float directionX)
        {
            ShiftX += directionX;
            UpdateTransformation();
        }

        public void MoveY(float directionY)
        {
            ShiftY += directionY;
            UpdateTransformation();
        }

        public void MoveZ(float directionZ)
        {
            ShiftZ += directionZ;
            UpdateTransformation();
        }

        public void Move(Vector direction)
        {
            ShiftX += direction.X;
            ShiftY += direction.Y;
            ShiftZ += direction.Z;
            UpdateTransformation();
        }

        public void ToScaleX(float scaleX)
        {
            ScaleX *= scaleX;
            UpdateTransformation();
        }

        public void ToScaleY(float scaleY)
        {
            ScaleY *= scaleY;
            UpdateTransformation();
        }

        public void ToScaleZ(float scaleZ)
        {
            ScaleZ *= scaleZ;
            UpdateTransformation();
        }

        public void ToScale(Vector scale)
        {
            ScaleX *= scale.X;
            ScaleY *= scale.Y;
            ScaleZ *= scale.Z;
            UpdateTransformation();
        }

        private void UpdateTransformation()
        {
            var Rotate = new float[4, 4]
            {{ CosY * CosZ,                      -SinZ * CosY,                      SinY,         0 },
             { SinX * SinY * CosZ + SinZ * CosX, -SinX * SinY * SinZ + CosX * CosZ, -SinX * CosY, 0 },
             { SinX * SinZ - SinY * CosX * CosZ,  SinX * CosZ + SinY * SinZ * CosX,  CosX * CosY, 0 },
             { 0,                                                               0,            0,  1 }};

            var Scale = new float[4, 4]
            {{ ScaleX, 0, 0, 0 },
             { 0, ScaleY, 0, 0 },
             { 0, 0, ScaleZ, 0 },
             { 0, 0, 0, 1 }};

            var Shift = new float[4, 4]
            {{ 1, 0, 0, ShiftX },
             { 0, 1, 0, ShiftY },
             { 0, 0, 1, ShiftZ },
             { 0, 0, 0, 1 }};

            TransformationMatrix = Multiply(Multiply(Shift, Rotate), Scale);
        }

        //public void ResetTransformation()
        //{
        //    ShiftX = 0;
        //    ShiftY = 0;
        //    ShiftZ = 0;
        //    ScaleX = 1;
        //    ScaleY = 1;
        //    ScaleY = 1;
        //    RotateX(-AngleRadX, false);
        //    RotateY(-AngleRadY, false);
        //    RotateZ(-AngleRadZ, true);
        //}

        public Point Apply(Point point)
        {
            float[] pointArray = { point.X, point.Y, point.Z, 1 };
            float[] transformedPoint = { 0, 0, 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    transformedPoint[i] += (float)TransformationMatrix[i, j] * pointArray[j];
                }
            }
            return new Point(transformedPoint[0], transformedPoint[1], transformedPoint[2]);
        }

        public Triangle Apply(Triangle triangle)
        {
            return new Triangle(Apply(triangle.P1), Apply(triangle.P2), Apply(triangle.P3));
        }

        public Vector Apply(Vector vector)
        {
            float[] vectorArray = { vector.X, vector.Y, vector.Z };
            float[] transformedVector = { 0, 0, 0 };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    transformedVector[i] += (float)TransformationMatrix[i, j] * vectorArray[j];
                }
            }
            return new Vector(transformedVector[0], transformedVector[1], transformedVector[2]);
        }

        static float[,] Multiply(float[,] matrixA, float[,] matrixB)
        {
            var result = new float[matrixA.GetLength(0), matrixB.GetLength(1)];
            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixB.GetLength(1); j++)
                {
                    for (int m = 0; m < matrixA.GetLength(1); m++)
                    {
                        result[i, j] += matrixA[i, m] * matrixB[m, j];
                    }
                }
            }
            return result;
        }
    }
}
