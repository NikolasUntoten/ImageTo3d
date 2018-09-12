using System;
using System.Collections.Generic;
using System.Drawing;
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
		/* Stores the slope of the line, such that the pixel shown
		 * on the camera must have originated from some point on
		 * the line.
		 */ 
		Vector3 slope;

		/* Stores the starting point of the line
		 * This point relates to the relative position of the camera
		 * when this projection was found as compared to when the simulation
		 * began.
		 */
		Vector3 origin;

		/* Stores the color of the projection in integer form using bitshifting
		 */
		int color;

		public Projection(Vector3 initSlope, Vector3 initOrigin, int initColor)
		{
			slope = initSlope;
			origin = initOrigin;
			color = initColor;
		}

		/*
		 * Method returns the color of the projection in the form
		 * of Drawing's color.
		 * Uses bitshifting to unpack color from int
		 */ 
		public Color GetColor()
		{
			return new Color();
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
