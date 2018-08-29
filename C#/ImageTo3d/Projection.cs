using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ImageTo3d.Util
{
	/* Created by Nikolas Gaub
	 * 
	 * A projection is a representation of all possible points
	 * an image from the camera could represent in 3D space.
	 * 
	 * This function stores the equation for a line in 3 dimensional space
	 * as a slope / origin form, such that the vector r, final position, is
	 * r.x = slope.x * t + origin.x
	 * r.y = slope.y * t + origin.y
	 * r.z = slope.z * t + origin.z
	 * for all t
	 * 
	 * This class is meant to be used for finding intersections between
	 * similarly colored lines, to determine where the original 3D point is
	 * most likely to lie.
	 */
	class Projection {
		Vector3 slope;
		Vector3 origin;
		int color;

		public Projection(Vector3 initSlope, Vector3 initOrigin, int initColor)
		{
			slope = initSlope;
			origin = initOrigin;
			color = initColor;
		}

		/*
		 * Find the point at a given distance along the line
		 */ 
		private Vector3 GetPoint(int t)
		{
			Vector3 point = new Vector3
			{
				X = origin.X + slope.X * t,
				Y = origin.Y + slope.Y * t,
				Z = origin.Z + slope.Z * t
			};

			return point;
		}
    }
}
