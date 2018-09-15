using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace Asteroids
{
	class Ship
	{		
		private Model3DGroup mainModel;
		public GeometryModel3D Model { get; set; }
		public Point Position { get; set; }
		public bool IsHit { get; set; } = false;
		public Ship(Model3DGroup model)
		{			
			mainModel = model;

			Model = Ship.GetShip(Colors.Red);
			Transform();			
			mainModel.Children.Add(Model);
		}

		public void Transform()
		{
			ScaleTransform3D scale = new ScaleTransform3D()
			{
				ScaleX = 0.1,
				ScaleY = 0.1,
				ScaleZ = 0.1
			};
			TranslateTransform3D translateTransform3D = new TranslateTransform3D();			
			translateTransform3D.OffsetX = Position.X;
			translateTransform3D.OffsetY = Position.Y;

			Transform3DGroup transform3DGroup = new Transform3DGroup();
			transform3DGroup.Children.Add(scale);
			transform3DGroup.Children.Add(translateTransform3D);
			Model.Transform = transform3DGroup;
		}
		
				

		public static GeometryModel3D GetShip(Color color)
		{
			GeometryModel3D model = new GeometryModel3D();
			MeshGeometry3D mesh = new MeshGeometry3D();


			Point3DCollection myPositionCollection = new Point3DCollection();
			myPositionCollection.Add(new Point3D(-1, 0, 0));
			myPositionCollection.Add(new Point3D(0, 0, 0.5));
			myPositionCollection.Add(new Point3D(0, 2, 0));
			myPositionCollection.Add(new Point3D(0, 0, 0.5));
			myPositionCollection.Add(new Point3D(1, 0, 0));
			myPositionCollection.Add(new Point3D(0, 2, 0));

			myPositionCollection.Add(new Point3D(-1, 0, 0));
			myPositionCollection.Add(new Point3D(0, -0.5, 0));
			myPositionCollection.Add(new Point3D(0, 0, 0.5));
			myPositionCollection.Add(new Point3D(0, 0, 0.5));
			myPositionCollection.Add(new Point3D(0, -0.5, 0));
			myPositionCollection.Add(new Point3D(1, 0, 0));
			mesh.Positions = myPositionCollection;

			Vector3DCollection myNormalCollection = new Vector3DCollection();
			for (int j = 0; j < mesh.Positions.Count; j+=3)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector3D a = new Vector3D(mesh.Positions[j].X, mesh.Positions[j].Y, mesh.Positions[j].Z);
					Vector3D b = new Vector3D(mesh.Positions[j+1].X, mesh.Positions[j + 1].Y, mesh.Positions[j + 1].Z);
					Vector3D c = new Vector3D(mesh.Positions[j + 2].X, mesh.Positions[j + 2].Y, mesh.Positions[j + 2].Z);					
					myNormalCollection.Add(Vector3D.CrossProduct(Vector3D.Subtract(b, a), Vector3D.Subtract(c, a)));
				}

			}			
			mesh.Normals = myNormalCollection;

			PointCollection myTextureCoordinatesCollection = new PointCollection();			
			myTextureCoordinatesCollection.Add(new Point(0, 0));
			myTextureCoordinatesCollection.Add(new Point(1, 0));
			myTextureCoordinatesCollection.Add(new Point(1, 1));
			myTextureCoordinatesCollection.Add(new Point(1, 1));
			myTextureCoordinatesCollection.Add(new Point(0, 1));
			myTextureCoordinatesCollection.Add(new Point(0, 0));
			myTextureCoordinatesCollection.Add(new Point(0, 0));
			myTextureCoordinatesCollection.Add(new Point(1, 0));
			myTextureCoordinatesCollection.Add(new Point(1, 1));
			myTextureCoordinatesCollection.Add(new Point(1, 1));
			myTextureCoordinatesCollection.Add(new Point(0, 1));
			myTextureCoordinatesCollection.Add(new Point(0, 0));			
			mesh.TextureCoordinates = myTextureCoordinatesCollection;

			Int32Collection myTriangleIndicesCollection = new Int32Collection();
			for (int i = 0; i < 12; i++)
			{
				myTriangleIndicesCollection.Add(i);
			}
			mesh.TriangleIndices = myTriangleIndicesCollection;

			model.Geometry = mesh;

			Brush brush = new SolidColorBrush(color);
			DiffuseMaterial material = new DiffuseMaterial(brush);
			model.Material = material;
			return model;
		}
	}
}
