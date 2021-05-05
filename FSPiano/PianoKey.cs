// This Code was created by Microgold Software Inc. for educational purposes
// Copyright Microgold Software Inc. Saturday, January 25, 2003

using System;
using System.Drawing;

namespace DotNetPiano
{
	/// <summary>
	/// Summary description for PianoKey.
	/// </summary>
	public class PianoKey
	{ 
		protected Point Position = new Point(0,0);
		protected int Frequency = 5000;

		protected Rectangle m_Border;

		public Rectangle Border
		{
			get
			{
				return m_Border;
			}
		}	

		public PianoKey()
		{
			//
			// TODO: Add constructor logic here
			//
			m_Border = new Rectangle(20, 20, WhiteKey.kWidth, 80);
		}

		public PianoKey(int x, int y, int frequency)
		{
			//
			// TODO: Add constructor logic here
			//
			Frequency = frequency;
			Position.X = x;
			Position.Y = y;
		}

		public virtual void Draw(Graphics g)
		{
        
		}

		public int TheFrequency
		{
			get
			{
			  return this.Frequency;
			}
		}

		public virtual bool IsContained(Point p)
		{
			Rectangle r = new Rectangle(Position.X, Position.Y, 20, 100);
			if (r.Contains(p))
				return true;

			return false;
		}

	}
}
