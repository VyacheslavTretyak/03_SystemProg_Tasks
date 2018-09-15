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
	public class Cube
	{
		public bool IsRemove { get; set; } = false;
		private Model3DGroup mainModel;
		public Model3DGroup Model { get; set; }
		public Cube(Model3DGroup model, Random rnd)
		{			
			int time = rnd.Next(5000, 10000);
			mainModel = model;
			Duration duration = TimeSpan.FromMilliseconds(time);

			Model = GetCube(Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256)));
			double size = (rnd.NextDouble()+0.1)*0.1;
			ScaleTransform3D scale = new ScaleTransform3D()
			{
				ScaleX = size,
				ScaleY = size,
				ScaleZ = size
			};			
			TranslateTransform3D translateTransform3D = new TranslateTransform3D();
			translateTransform3D.OffsetY = 0;
			translateTransform3D.OffsetX = rnd.NextDouble()*2.2 - 1.1;
			RotateTransform3D rotateTransform3D = new RotateTransform3D();			
			AxisAngleRotation3D axisAngleRotation3d = new AxisAngleRotation3D();
			axisAngleRotation3d.Axis = new Vector3D(1, 1, 0);
			axisAngleRotation3d.Angle = 0;
			rotateTransform3D.Rotation = axisAngleRotation3d;

			Transform3DGroup transform3DGroup = new Transform3DGroup();
			transform3DGroup.Children.Add(rotateTransform3D);
			transform3DGroup.Children.Add(scale);
			transform3DGroup.Children.Add(translateTransform3D);
			Model.Transform = transform3DGroup;
			
			DoubleAnimation translateAnimation = new DoubleAnimation();
			translateAnimation.Completed += TranslateAnimation_Completed;
			translateAnimation.Duration = duration;
			translateAnimation.RepeatBehavior = new RepeatBehavior(1);
			translateAnimation.From = 0.5;
			translateAnimation.To = -0.5;
			translateTransform3D.BeginAnimation(TranslateTransform3D.OffsetYProperty, translateAnimation);
			
			DoubleAnimation angleAnimation = new DoubleAnimation();
			angleAnimation.Duration = duration;
			angleAnimation.RepeatBehavior = new RepeatBehavior(1);
			angleAnimation.From = 0;
			angleAnimation.To = 360;
			axisAngleRotation3d.BeginAnimation(AxisAngleRotation3D.AngleProperty, angleAnimation);
			
			Vector3DAnimation axisAnimation = new Vector3DAnimation();
			axisAnimation.Duration = duration;
			axisAnimation.AutoReverse = true;
			axisAnimation.RepeatBehavior = new RepeatBehavior(1);
			axisAnimation.From = new Vector3D(rnd.Next(-1, 2), rnd.Next(-1, 2), rnd.Next(-1, 2));
			axisAnimation.To = new Vector3D(rnd.Next(-1, 2), rnd.Next(-1, 2), rnd.Next(-1, 2));
			axisAngleRotation3d.BeginAnimation(AxisAngleRotation3D.AxisProperty, axisAnimation);

			
			mainModel.Children.Add(Model);
		}

		private void TranslateAnimation_Completed(object sender, EventArgs e)
		{
			mainModel.Children.Remove(Model);
			IsRemove = true;
		}

		public static Model3DGroup GetCube(Color color)
		{
			Model3DGroup model = new Model3DGroup();
			model.Children.Add(Side.GetSide(new Vector3D(0, 1, 0), 0, color));
			model.Children.Add(Side.GetSide(new Vector3D(0, 1, 0), 90, color));
			model.Children.Add(Side.GetSide(new Vector3D(0, 1, 0), 180, color));
			model.Children.Add(Side.GetSide(new Vector3D(0, 1, 0), 270, color));
			model.Children.Add(Side.GetSide(new Vector3D(1, 0, 0), 90, color));
			model.Children.Add(Side.GetSide(new Vector3D(1, 0, 0), 270, color));
			return model;
		} 
	}
}
