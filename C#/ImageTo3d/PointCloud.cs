using System;
using System.Collections.Generic;
using System.Text;

namespace ImageTo3d.Util
{
    class PointCloud
    {
		Dictionary<Point, float> confidences;

		public PointCloud() 
		{
			confidences = new Dictionary<Point, float>();
		}

		public void UpdateConfindences(Point[] points)
		{
			foreach (Point p in points) 
			{
				//find similar point in cloud, add to confidence
			}
		}
	}
}
