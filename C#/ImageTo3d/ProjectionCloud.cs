using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageTo3d.Util
{
	/* Created by Nikolas Gaub
	 * 
	 * Used to track a large number of projections, overwriting
	 * old projections as new projections are added.
	 * 
	 * All methods need to be as time efficient as possible, as
	 * these methods will be called often when calculating
	 * point confidences
	 */ 
    class ProjectionCloud
    {

		private Projection[] Projections;

		private int Index;

		/*
		 * Initializer
		 * Size should be large, as each frame will add
		 * the same number of projections as there are pixels
		 * in the frame.
		 */ 
		public ProjectionCloud(int size)
		{
			Index = 0;
			Projections = new Projection[size];
		}

		/*
		 * Adds given projection to array at current index.
		 * Index cycles through the array, overwriting previous data.
		 * This keeps fresh projections more relavant to confidences.
		 */
		public void Add(Projection p)
		{
			Projections[Index] = p;
			Index++;

			if (Index == Projections.Length)
			{
				Index = 0;
			}
		}

		/*
		 * Returns list of projections.
		 * If any space is empty, trims the array.
		 */
		public Projection[] GetProjections()
		{
			if (Projections[Index] == null)
			{
				Projection[] arr = new Projection[Index];
				for (int i = 0; i < arr.Length; i++)
				{
					arr[i] = Projections[i];
				}
				return arr;
			} else
			{
				return Projections;
			}
		}

		/* Returns list of projections that match a certain color
		 * within a threshold range.
		 * The threshold is used as a maximum difference of all combined aspects,
		 * so any difference in r, g, or b is added together, then checked
		 * against the threshold.
		 * Threshold is used in a less than or equal to operator.
		 */ 
		public Projection[] GetProjections(Color color, int threshold)
		{
			int count = 0;
			foreach (Projection p in Projections)
			{
				if (p != null)
				{
					if (FindColorDistance(color, p.GetColor()) <= threshold)
					{
						count++;
					}
				}
			}

			Projection[] matches = new Projection[count];
			count = 0;
			foreach (Projection p in Projections)
			{
				if (p != null)
				{
					if (FindColorDistance(color, p.GetColor()) <= threshold)
					{
						matches[count] = p;
						count++;
					}
				}
			}

			return matches;
		}

		/* Returns the distance between two colors, such that the
		 * number returned is the sum of differences between components
		 * of the color.
		 */ 
		private int FindColorDistance(Color c1, Color c2)
		{
			int total = 0;
			total += Math.Abs(c1.R - c2.R);
			total += Math.Abs(c1.G - c2.G);
			total += Math.Abs(c1.B - c2.B);
			return total;
		}
	}
}
