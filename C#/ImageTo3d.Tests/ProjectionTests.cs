using System;
using Xunit;
using ImageTo3d.Util;
using System.Diagnostics;
using System.Numerics;

namespace ImageTo3d.Tests
{
    public class ProjectionTests
    {
		[Fact]
		public void Projections_AreCreatedProperly()
		{
			Projection p1 = new Projection(3, 6, -1, 1, 5.55f, 0, 0);

			Assert.Equal(3, p1.origin.X);
			Assert.Equal(6, p1.origin.Y);
			Assert.Equal(-1, p1.origin.Z);
			Assert.Equal(1, p1.slope.X);
			Assert.Equal(5.55f, p1.slope.Y);
			Assert.Equal(0, p1.slope.Z);
		}

		[Fact]
		public void MinimumDistance_IsCorrect()
		{
			Projection p1 = new Projection(0, 0, 0, 1, 0, 0, 0);
			Projection p2 = new Projection(2, 10, 0, -1, 0.1f, 0, 0);

			double minimum = Projection.MinimumDistance(p1, p2);

			Assert.NotEqual(0, minimum);
		}

		[Fact]
        public void Two_CollidingProjections_CollideProperly()
        {
			Projection p1 = new Projection(0, 0, 0, 1, 0, 0, 0);
			Projection p2 = new Projection(2, 0, 0, -1, 0, 0, 0);

			Assert.True(Projection.DoesCollide(p1, p2));

			p1 = new Projection(0, 0, 0, 1, 0, 0, 0);
			p2 = new Projection(2, 1, 0, -1, 0, 0, 0);

			Assert.True(Projection.DoesCollide(p1, p2));
		}

		[Fact]
		public void NearestPoint_IsCorrect()
		{
			Projection p1 = new Projection(0, 1, 0, 1, 0, 0, 0);
			Projection p2 = new Projection(0, 2, 0, 0, 0, 1, 0);

			Vector3 actual = Projection.FindNearestPoint(p1, p2);
			Vector3 expected = new Vector3(0, 1, 0);
			Assert.Equal(expected, actual);
		}
	}
}
