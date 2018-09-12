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
		public Vector3 slope;

		/* Stores the starting point of the line
		 * This point relates to the relative position of the camera
		 * when this projection was found as compared to when the simulation
		 * began.
		 */
		public Vector3 origin;

		/* Stores the color of the projection in integer form using bitshifting
		 */
		int color;

		public Projection(Vector3 initSlope, Vector3 initOrigin, int initColor)
		{
			slope = initSlope;
			origin = initOrigin;
			color = initColor;
		}

		/* Finds if the smalles distance between two projections is
		 * less than the given threshold.
		 */ 
		public static Boolean DoesCollide(Projection p1, Projection p2, double threshold)
		{
			return MinimumDistance(p1, p2) <= threshold;
		}

		/* Finds the smallest distance between two projections
		 * Uses the formula |(a-c) * (b x d) / |b x d|)| where a b c and d satisfy
		 * p1 = a + bt and p2 = c + ds where t and s are scalars.
		 * The method itself simplifies this formula into pure algebra, and
		 * as such is mildly unreadable.
		 */
		private static double MinimumDistance(Projection p1, Projection p2)
		{
			Vector3 cross = Vector3.Cross(p1.slope, p2.slope);
			double val = Vector3.Dot(p2.origin - p1.origin, cross) / cross.Length();
			return Math.Abs(val);
		}

		/* Finds the nearest point between two projections.
		 * uses formula found at https://en.wikipedia.org/wiki/Skew_lines#Nearest_Points
		 */
		public static Vector3 FindNearestPoint(Projection p1, Projection p2)
		{
			Vector3 n1 = Vector3.Cross(p1.slope, p2.slope);
			Vector3 n2 = Vector3.Cross(p2.slope, n1);
			Vector3 c1 = p1.origin + 
				(Vector3.Dot(p2.origin - p1.origin, n2) 
				/ (Vector3.Dot(p1.slope, n2))) 
				* p1.slope;
			//TODO get the nearest point on second line, and average the two?
			return c1;
		}

		/* Finds the collision, using a derived formula starting from where p1 = p2
		 * so [A + Bt, E + Ft, I + Jt] = [C + Ds, G + Hs, K + Ls]
		 * by setting the first row equal, we find
		 * t = (c - a + Ds) / B
		 * substituting t for the formula above, we see that E + Ft = G + Hs becomes
		 * G + Hs = E + F((C - A + Ds) / B)
		 * which simplifies to
		 * s = (G - E - F(C - A) / B) / (F * D / B - H)
		 * which I have translated into code below, then plugged s into p2.
		 */
		public static Vector3 FindCollision(Projection p1, Projection p2)
		{
			float s = (p2.origin.Y - p1.origin.Y - (p1.slope.Y * (p2.origin.X - p1.origin.X) / p1.slope.X))
				/ ((p1.slope.Y * p2.slope.X / p1.slope.X) - p2.slope.Y);
			return p2.origin + p2.slope * s;
		}

		/*
		 * Method returns the color of the projection in the form
		 * of Drawing's color.
		 * Uses bitshifting to unpack color from int
		 */ 
		public Color GetColor()
		{
			return Utility.IntToColor(color);
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
