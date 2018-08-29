using System;
using System.Collections.Generic;
using System.Text;

namespace ImageTo3d.Util
{
	/* Created by Nikolas Gaub
	 * 
	 * Stores point data, along with confidence data.
	 * Point data is a representation of the world as many points,
	 * each with their own color. With enough points, the world
	 * is replicated.
	 * 
	 * This is the end product, confidences should be used
	 * to inform whether or not a point is likely
	 * an accurate representation of the world.
	 */ 
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
