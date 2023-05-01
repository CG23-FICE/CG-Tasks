using System.Globalization;
using MainProject.Models.Basics;
using MainProject.Models.Shapes;

namespace MainProject.Readers
{
    public class ObjReader
    {
        public List<Triangle> Read(string path)
        {
            using StreamReader reader = new StreamReader(path);
            string? line;
            List<Triangle> faces = new List<Triangle>();
            List<Vector> points = new List<Vector>();

            while ((line = reader.ReadLine()) != null)
            {
                if (line == "") { continue; }
                char[] delimiters = new char[] { ' ', '\t', '\r', '\n' };
                string[] lineArray = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                switch (lineArray[0])
                {
                    // if the line defines a point, parse its x, y, and z coordinates and add them to the points list
                    case "v":
                        float x = float.Parse(lineArray[1], CultureInfo.InvariantCulture);
                        float y = float.Parse(lineArray[2], CultureInfo.InvariantCulture);
                        float z = float.Parse(lineArray[3], CultureInfo.InvariantCulture);

                        Vector point = new Vector(x, y, z);
                        points.Add(point);

                        break;

                    // if the line defines a face, parse its vertex indices and create triangles from them
                    case "f":
                        List<int> pointIndexes = new List<int>();
                        for (int i = 1; i < lineArray.Length; i++)
                        {
                            string[] indices = lineArray[i].Split('/');
                            int pointIndex = int.Parse(indices[0]) - 1;
                            int uvIndex = indices.Length > 1 && !string.IsNullOrEmpty(indices[1]) ? int.Parse(indices[1]) - 1 : -1;
                            int normalIndex = indices.Length > 2 ? int.Parse(indices[2]) - 1 : -1;
                            pointIndexes.Add(pointIndex);

                            // if this is the third or later vertex in the face, create a triangle from it and the two previous vertices
                            if (i >= 3)
                            {
                                faces.Add(new Triangle(points[pointIndexes[0]], points[pointIndexes[i - 2]], points[pointIndexes[i - 1]]));
                            }
                        }

                        break;

                }
            }
            return faces;
        }
    }
}