using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Asteroids
{
	public class Side
	{
		public static GeometryModel3D GetSide(Vector3D axis, double angle, Color color)
		{
			GeometryModel3D model = new GeometryModel3D();
			MeshGeometry3D side = new MeshGeometry3D();

			Vector3DCollection myNormalCollection = new Vector3DCollection();
			for (int i = 0; i < 6; i++)
				myNormalCollection.Add(new Vector3D(0, 0, 1));
			side.Normals = myNormalCollection;

			Point3DCollection myPositionCollection = new Point3DCollection();
			myPositionCollection.Add(new Point3D(-1, -1, 1));
			myPositionCollection.Add(new Point3D(1, -1, 1));
			myPositionCollection.Add(new Point3D(1, 1, 1));
			myPositionCollection.Add(new Point3D(1, 1, 1));
			myPositionCollection.Add(new Point3D(-1, 1, 1));
			myPositionCollection.Add(new Point3D(-1, -1, 1));	
			side.Positions = myPositionCollection;

			PointCollection myTextureCoordinatesCollection = new PointCollection();			
			myTextureCoordinatesCollection.Add(new Point(0, 0));
			myTextureCoordinatesCollection.Add(new Point(1, 0));
			myTextureCoordinatesCollection.Add(new Point(1, 1));
			myTextureCoordinatesCollection.Add(new Point(1, 1));
			myTextureCoordinatesCollection.Add(new Point(0, 1));
			myTextureCoordinatesCollection.Add(new Point(0, 0));			
			side.TextureCoordinates = myTextureCoordinatesCollection;

			Int32Collection myTriangleIndicesCollection = new Int32Collection();
			for (int i = 0; i < 6; i++)
			{
				myTriangleIndicesCollection.Add(i);
			}
			side.TriangleIndices = myTriangleIndicesCollection;
			
			model.Geometry = side;
			
			Brush brush = new SolidColorBrush(color);
			
			DiffuseMaterial material = new DiffuseMaterial(brush);
			model.Material = material;
			
			RotateTransform3D rotate = new RotateTransform3D();
			AxisAngleRotation3D axisAngle = new AxisAngleRotation3D();
			axisAngle.Axis = axis;
			axisAngle.Angle = angle;
			rotate.Rotation = axisAngle;			
			model.Transform = rotate;
			return model;
		}
	}
}
