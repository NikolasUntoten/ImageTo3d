using ImageTo3d.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace ImageTo3d
{
	/* Created by Nikolas Gaub
	 * 
	 * Maintains information about a world, seen through given
	 * camera images. The more images with useful coordinates
	 * are fed into this class, the more accurate the world
	 * depiction becomes
	 */ 
	public class WorldSimulation 
	{

		private PointCloud PointCloud;

		private ProjectionCloud ProjectionCloud;

		private CameraInfo cameraInfo;

		public WorldSimulation(CameraInfo initCamInfo) 
		{
			cameraInfo = initCamInfo;
			PointCloud = new PointCloud();
			ProjectionCloud = new ProjectionCloud(1000);
		}

		/* Given a certain image, and information about the camera,
		 * adds a list of projections of pixels to the projectioncloud.
		 */ 
		public void AddImage(Color[][] data, Vector3 camPosition, Vector3 camRotation) 
		{
			int width = data.GetLength(0);
			int height = data.GetLength(1);
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					Projection p = FindProjection(data[x][y], x, y, camPosition, camRotation);
					ProjectionCloud.Add(p);
				}
			}
		}

		public void UpdateSimulation()
		{
			/* Basically here I plan to go through and check all projections recently added for
			 * collisions with other projections, then update the confidences of the
			 * point cloud with the new information.
			 * 
			 * When checknig if a projection collides, I should insure that there is a significant
			 * difference in their origin (since this is a multivariable equation we're solving,
			 * the close the origin is, the more likely it is to give bad results. We need diverse
			 * data for accuraccy).
			 * 
			 * So for finding collisions, I am filtering by color first, then origin distance, then
			 * a minimum threshold for collision. After all of these are met, the lines "collide",
			 * and the point they create is added to the point cloud (although there should be a mechanism
			 * for defining if a point is near enough to another to be considered the same point, as this
			 * will be vital for an efficient use of data for confidences)
			 */ 
		}

		private Projection FindProjection(Color color, int x, int y,
			Vector3 camPosition, Vector3 camRotation) 
		{

			return null;
		}
	}
}
