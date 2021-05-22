using OxyPlot;
using OxyPlot.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxyPlot_Test
{
    public class CustomEllipseAnnotation : ShapeAnnotation
    {
		/// <summary>
		/// The rectangle transformed to screen coordinates.
		/// </summary>
		private OxyRect screenRectangle;

		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }

		public CustomEllipseAnnotation()
        {

        }

		/// <summary>
		/// Renders the polygon annotation.
		/// </summary>
		/// <param name="rc">The render context.</param>
		public override void Render(IRenderContext rc)
		{
			base.Render(rc);
			screenRectangle = new OxyRect(Transform(X - Width / 2.0, Y - Height / 2.0), Transform(X + Width / 2.0, Y + Height / 2.0));
			OxyRect clippingRect = GetClippingRect();
			rc.DrawClippedEllipse(clippingRect, screenRectangle, GetSelectableFillColor(base.Fill), GetSelectableColor(base.Stroke), base.StrokeThickness);
			if (!string.IsNullOrEmpty(base.Text))
			{
				ScreenPoint actualTextPosition = GetActualTextPosition(() => screenRectangle.Center);
				rc.DrawClippedText(clippingRect, actualTextPosition, base.Text, base.ActualTextColor, base.ActualFont, base.ActualFontSize, base.ActualFontWeight, base.TextRotation, base.TextHorizontalAlignment, base.TextVerticalAlignment);
			}
		}

		/// <summary>
		/// When overridden in a derived class, tests if the plot element is hit by the specified point.
		/// </summary>
		/// <param name="args">The hit test arguments.</param>
		/// <returns>
		/// The result of the hit test.
		/// </returns>
		protected override HitTestResult HitTestOverride(HitTestArguments args)
		{
			if (screenRectangle.Contains(args.Point))
			{
				return new HitTestResult(this, args.Point);
			}
			return null;
		}
	}
}
