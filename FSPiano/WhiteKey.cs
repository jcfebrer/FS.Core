// This Code was created by Microgold Software Inc. for educational purposes
// Copyright Microgold Software Inc. Saturday, January 25, 2003

using System;
using System.Drawing;

namespace DotNetPiano
{
	/// <summary>
	/// Summary description for WhiteKey.
	/// </summary>
	public class WhiteKey : PianoKey
	{
		const int kHeight = 150;
		public const  int kWidth  = 50;

		public WhiteKey()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public WhiteKey(int x, int y, int frequency) : base(x, y, frequency)
		{
		//
		// TODO: Add constructor logic here
		//
			m_Border = new Rectangle(x, y, kWidth, kHeight);
		}


		public override void Draw (Graphics g)
		{
			if (PianoForm.CurrentKey == this)
			{
				g.FillRectangle(Brushes.White, Position.X, Position.Y, kWidth, kHeight);
			}
			else
			{
				g.FillRectangle(Brushes.SkyBlue, Position.X, Position.Y, kWidth, kHeight);
			}


			g.DrawRectangle(Pens.Black, Position.X, Position.Y, kWidth, kHeight);
		}

		public override bool IsContained(Point p)
		{
			Rectangle r = new Rectangle(Position.X, Position.Y, kWidth, kHeight);
			if (r.Contains(p))
				return true;

			return false;
		}


	}
}
