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

		}

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

		private Projection FindProjection(Color color, int x, int y,
			Vector3 camPosition, Vector3 camRotation) 
		{

			return null;
		}
	}
}
