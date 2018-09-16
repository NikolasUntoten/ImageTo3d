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
	public class Projection {
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

		public Projection(Vector3 initOrigin, Vector3 initSlope,  int initColor)
		{
			slope = initSlope;
			origin = initOrigin;
			color = initColor;
		}

		public Projection(float originX, float originY, float originZ,
				 float slopeX, float slopeY, float slopeZ, int initColor)
		{
			slope = new Vector3(slopeX, slopeY, slopeZ);
			origin = new Vector3(originX, originY, originZ);
			color = initColor;
		}

		/* Determines if two lines intersect.
		 */ 
		public static Boolean DoesCollide(Projection p1, Projection p2)
		{
			if (AreParrallel(p1.slope, p2.slope)) return false;
			Vector3 cross = Vector3.Cross(p1.slope, p2.slope);
			float val = Vector3.Dot(cross, p1.origin - p2.origin);
			return Math.Abs(val) < 0.001f;
		}

		/* Finds if the smalles distance between two projections is
		 * less than the given threshold.
		 */
		public static Boolean DoesNearCollide(Projection p1, Projection p2, double threshold)
		{
			return MinimumDistance(p1, p2) <= threshold;
		}

		/* Finds the smallest distance between two projections
		 * Uses the formula |(a-c) * (b x d) / |b x d|)| where a b c and d satisfy
		 * p1 = a + bt and p2 = c + ds where t and s are scalars.
		 */
		public static double MinimumDistance(Projection p1, Projection p2)
		{
			Vector3[] points = FindNearestPoints(p1, p2);
			return Vector3.Distance(points[0], points[1]);
			/*Vector3 cross = Vector3.Cross(p1.slope, p2.slope);
			double val = Vector3.Dot(p2.origin - p1.origin, cross) / cross.Length();
			return Math.Abs(val);*/ // This method should be faster, but doesn't currently work
		}

		/* Finds the two nearest points of the projections, and
		 * returns them in an array that has exactly 2 elements
		 */
		public static Vector3[] FindNearestPoints(Projection p1, Projection p2)
		{
			Vector3[] points = new Vector3[2];
			points[0] = FindNearestPoint(p1, p2);
			points[1] = FindNearestPoint(p2, p1);
			return points;
		}

		/* Helper method for FindNearestPoints, to condense 
		 * the equation for finding the actual point.
		 * TReturns the nearest point on line p1 to line p2.
		 * Uses formula found at https://en.wikipedia.org/wiki/Skew_lines#Nearest_Points
		 */
		private static Vector3 FindNearestPoint(Projection p1, Projection p2)
		{
			if (AreParrallel(p1.slope, p2.slope))
				throw new Exception("Projections are parallel!");
			if (DoesCollide(p1, p2)) throw new Exception("Projections collide!");

			Vector3 n1 = Vector3.Cross(p1.slope, p2.slope);
			Vector3 n2 = Vector3.Cross(p2.slope, n1);
			return p1.origin +
				(Vector3.Dot(p2.origin - p1.origin, n2)
				/ (Vector3.Dot(p1.slope, n2)))
				* p1.slope;
		}

		/* This was meant to be a short helper method, but turned
		 * into a large method due to the need to account for any 0
		 * in a parallel vector.
		 * Finds a non-zero scalar if there is one, then checks to see
		 * if the scalar works on each element.
		 */ 
		public static Boolean AreParrallel(Vector3 v1, Vector3 v2)
		{
			float scalar = 0;

			if (v1.X != 0 && v2.X != 0) scalar = v1.X / v2.X;
			if (v1.Y != 0 && v2.Y != 0) scalar = v1.Y / v2.Y;
			if (v1.Z != 0 && v2.Z != 0) scalar = v1.Z / v2.Z;

			if (scalar == 0) return false;
			if (v2.X * scalar != v1.X) return false;
			if (v2.Y * scalar != v1.Y) return false;
			if (v2.Z * scalar != v1.Z) return false;
			return true;
		}

		/* Finds the collision, using a derived formula starting from where p1 = p2
		 * so [A + Bt, E + Ft, I + Jt] = [C + Ds, G + Hs, K + Ls]
		 * by setting the first row equal, we find
		 * t = (c - a + Ds) / B
		 * substituting t for the formula above, we see that E + Ft = G + Hs becomes
		 * G + Hs = E + F((C - A + Ds) / B)
		 * which simplifies to
		 * s = (G - E - F(C - A) / B) / (F * D / B - H)
		 * which is translated into code below, then plugged s into p2.
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
