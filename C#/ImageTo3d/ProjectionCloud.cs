using System;
using System.Collections.Generic;
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
	}
}
