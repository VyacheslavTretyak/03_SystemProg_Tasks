using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Asteroids
{
	public class Scene
	{
		private Window wnd;
		private Model3DGroup modelGroup;
		private int countCubes = 5;
		private Cube[] cubes;
		private Random rnd;
		private Ship  ship;
		private Viewport3D view;
		private List<DependencyObject> hitResultList;
		private bool isTime = false;
		public Scene(Window wnd)
		{
			this.wnd = wnd;
			hitResultList = new List<DependencyObject>();
			cubes = new Cube[countCubes];
			rnd = new Random();			
			CompositionTarget.Rendering += CompositionTarget_Rendering;
			wnd.KeyDown += Wnd_KeyDown;
			LoadScene();
			ship = new Ship(modelGroup);
			
		}

		private void Wnd_KeyDown(object sender, KeyEventArgs e)
		{
			double x = 0;
			double y = 0;
			double step = 0.02;
			switch (e.Key)
			{
				case Key.Left:
					x -= step;
					break;
				case Key.Right:
					x += step;
					break;
				case Key.Up:
					y += step;
					break;
				case Key.Down:
					y -= step;
					break;
			}			
			Point pos = new Point(ship.Position.X + x, ship.Position.Y + y);
			ship.Position = pos;
			ship.Transform();
			
			
		}

		

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			Run();
		}		

		public void Run()
		{
			for (int i = 0; i < cubes.Length; i++)
			{
				if (cubes[i] == null || cubes[i].IsRemove)
				{					
					cubes[i] = new Cube(modelGroup, rnd);					
				}
				if (ship.Model.Bounds.IntersectsWith(cubes[i].Model.Bounds))
				{
					Brush brush = new SolidColorBrush(Colors.Red);
					DiffuseMaterial material = new DiffuseMaterial(brush);
					ship.Model.Material = material;
					Timer timer = new Timer(SetColorShip);
					timer.Change(1000, 0);
					
				}
				
				
			}
			if (isTime)
			{
				Brush brush = new SolidColorBrush(Colors.White);
				DiffuseMaterial material = new DiffuseMaterial(brush);
				ship.Model.Material = material;
				isTime = false;
			}
		}

		private void SetColorShip(object sender)
		{
			isTime = true;
			//MessageBox.Show("timer");
			(sender as Timer).Dispose();
			
		}

		public void LoadScene()
		{
			view = new Viewport3D();			
			modelGroup = new Model3DGroup();			
			ModelVisual3D visualModel = new ModelVisual3D();			

			PerspectiveCamera camera = new PerspectiveCamera();
			camera.Position = new Point3D(0, 0, 2);
			camera.LookDirection = new Vector3D(0, 0, -1);
			camera.FieldOfView = 60;
			view.Camera = camera;

			DirectionalLight directionalLight = new DirectionalLight();
			directionalLight.Color = Colors.White;
			directionalLight.Direction = new Vector3D(-0.61, -0.5, -0.61);
			modelGroup.Children.Add(directionalLight);

			visualModel.Content = modelGroup;
			view.Children.Add(visualModel);
			wnd.Content =  view;
		}
	}
}
