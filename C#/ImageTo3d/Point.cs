using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ImageTo3d.Util
{
    class Point
    {
		public Vector3 position;
		public int color;

		public Point(Vector3 initPosition, int initColor)
		{
			position = initPosition;
			color = initColor;
		} 
	}
}
