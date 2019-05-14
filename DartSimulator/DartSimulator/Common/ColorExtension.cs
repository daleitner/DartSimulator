using System.Drawing;

namespace DartSimulator.Common
{
	public static class ColorExtension
	{
		public static bool IsEquivalentTo(this Color source, Color target)
		{
			return source.R == target.R && source.G == target.G && source.B == target.B;
		}

		public static bool IsLike(this Color source, Color target, int tolerance)
		{
			return source.R <= target.R + tolerance && source.R >= target.R - tolerance 
				&& source.G <= target.G + tolerance && source.G >= target.G - tolerance
				&& source.B <= target.B + tolerance && source.B >= target.B - tolerance;
		}
	}
}
