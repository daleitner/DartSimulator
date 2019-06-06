using System.Drawing;

namespace Dart.Base
{
	internal static class ColorExtension
	{
		/// <summary>
		/// returns true if RGB Values of source and target are the same.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		internal static bool IsEquivalentTo(this Color source, Color target)
		{
			return source.R == target.R && source.G == target.G && source.B == target.B;
		}

		/// <summary>
		/// returns true if RGB Values of source are the same as of target +/- tolerance.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="tolerance"></param>
		/// <returns></returns>
		internal static bool IsLike(this Color source, Color target, int tolerance)
		{
			return source.R <= target.R + tolerance && source.R >= target.R - tolerance 
				&& source.G <= target.G + tolerance && source.G >= target.G - tolerance
				&& source.B <= target.B + tolerance && source.B >= target.B - tolerance;
		}
	}
}
