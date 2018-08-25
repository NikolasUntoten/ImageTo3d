using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace ImageTo3d
{
	static class DataManipulator {
		static void AddImageToDatabase(Color[][] data, Vector3 camPosition, Vector3 camRotation) {
			int width = data.GetLength(0);
			int height = data.GetLength(1);
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					AddProjectionToDatabase(data[x][y], x, y, width, height, camPosition, camRotation);
				}
			}
		}

		static void AddProjectionToDatabase(Color color, int x, int y, 
			int screenWidth, int screenHeight, Vector3 camPosition, Vector3 camRotation) {

		}
	}
}
