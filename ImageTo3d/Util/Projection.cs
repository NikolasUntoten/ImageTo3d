using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ImageTo3d.Util
{
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
    }
}
