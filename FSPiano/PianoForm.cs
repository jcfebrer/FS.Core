// This Code was created by Microgold Software Inc. for educational purposes
// Copyright Microgold Software Inc. Saturday, January 25, 2003

//----------------------------------------------------------------------------
// File: PianoForm.cs
//
// Copyright (c) Microsoft Corp. All rights reserved.
//-----------------------------------------------------------------------------
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace DotNetPiano
{
	public class PianoForm : System.Windows.Forms.Form
	{
		FSDirectShow.DirectSound directSound = new FSDirectShow.DirectSound();
		//const int maxFrequency = 200000; // The maximum frequency we'll allow this sample to support

		ArrayList BlackKeys = new ArrayList();
		ArrayList WhiteKeys = new ArrayList();

		public static PianoKey CurrentKey = null;

		#region WindowsForms Variables

		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textPan;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Timers.Timer tmrUpdate;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TrackBar tbarPan;
		private System.Windows.Forms.TrackBar tbarVolume;
		private System.Windows.Forms.TextBox textVolume;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		#endregion

		public PianoForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// add double buffering to reduce flicker
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);

			CreateKeys();

			try
			{
				// Load the icon from our resources
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(this.GetType());
				this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			}
			catch
			{
				// It's no big deal if we can't load our icons, but try to load the embedded one
				try { this.Icon = new System.Drawing.Icon(this.GetType(), "directx.ico"); } 
				catch {}
			}


			// Now that we have a sound device object set the frequency sliders correctly

			directSound.Initialize(this.Handle);

		}

		void CreateKeys()
		{
			int[] freqtable = { 8000, 9000, 10000, 10700, 12000, 13400, 15200, 16300, 18000, 20000 };
			int xpos = 20;
			int ypos = 125;
			int freq;
			for (int i = 0; i < 10; i++)
			{
				freq = freqtable[i];
				WhiteKeys.Add(new WhiteKey(xpos, ypos, freq));
				xpos += WhiteKey.kWidth;
			}

			xpos = 20 + WhiteKey.kWidth - BlackKey.kWidth / 2;

			int[] sfreqtable = { 8500, 9500, 10000, 11300, 12800, 14300, 15200, 17300, 19000, 20000 };

			for (int i = 0; i < 10; i++)
			{
				freq = sfreqtable[i];
				if ((i == 2) || (i == 6) || (i == 9))
				{
					// skip these
				}
				else
				{
					BlackKeys.Add(new BlackKey(xpos, ypos, freq));
				}
				xpos += WhiteKey.kWidth;

			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (null != components) 
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);		
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tmrUpdate = new System.Timers.Timer();
			this.textVolume = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbarPan = new System.Windows.Forms.TrackBar();
			this.buttonExit = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.tbarVolume = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textPan = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.tmrUpdate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbarPan)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).BeginInit();
			this.SuspendLayout();
			// 
			// tmrUpdate
			// 
			this.tmrUpdate.Enabled = true;
			this.tmrUpdate.SynchronizingObject = this;
			this.tmrUpdate.Elapsed += new System.Timers.ElapsedEventHandler(this.tmrUpdate_Elapsed);
			// 
			// textVolume
			// 
			this.textVolume.Location = new System.Drawing.Point(93, 366);
			this.textVolume.Name = "textVolume";
			this.textVolume.ReadOnly = true;
			this.textVolume.Size = new System.Drawing.Size(43, 20);
			this.textVolume.TabIndex = 1;
			this.textVolume.Text = "0";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(14, 358);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(73, 38);
			this.label10.TabIndex = 2;
			this.label10.Text = "Volume";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbarPan
			// 
			this.tbarPan.Location = new System.Drawing.Point(171, 300);
			this.tbarPan.Maximum = 20;
			this.tbarPan.Minimum = -20;
			this.tbarPan.Name = "tbarPan";
			this.tbarPan.Size = new System.Drawing.Size(236, 42);
			this.tbarPan.TabIndex = 4;
			this.tbarPan.TickFrequency = 5;
			this.tbarPan.Scroll += new System.EventHandler(this.tbarPan_Scroll);
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Location = new System.Drawing.Point(231, 421);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(74, 21);
			this.buttonExit.TabIndex = 0;
			this.buttonExit.Text = "Exit";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(6, 13);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(73, 15);
			this.label11.TabIndex = 2;
			this.label11.Text = "Focus";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbarVolume
			// 
			this.tbarVolume.Location = new System.Drawing.Point(173, 358);
			this.tbarVolume.Maximum = 0;
			this.tbarVolume.Minimum = -50;
			this.tbarVolume.Name = "tbarVolume";
			this.tbarVolume.Size = new System.Drawing.Size(236, 42);
			this.tbarVolume.TabIndex = 4;
			this.tbarVolume.Scroll += new System.EventHandler(this.tbarVolume_Scroll);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(139, 365);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 20);
			this.label8.TabIndex = 2;
			this.label8.Text = "Low";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(404, 367);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(47, 20);
			this.label9.TabIndex = 2;
			this.label9.Text = "High";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(137, 307);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 20);
			this.label5.TabIndex = 2;
			this.label5.Text = "Left";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(402, 309);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(47, 20);
			this.label6.TabIndex = 2;
			this.label6.Text = "Right";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 300);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 38);
			this.label7.TabIndex = 2;
			this.label7.Text = "Pan";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textPan
			// 
			this.textPan.Location = new System.Drawing.Point(92, 308);
			this.textPan.Name = "textPan";
			this.textPan.ReadOnly = true;
			this.textPan.Size = new System.Drawing.Size(43, 20);
			this.textPan.TabIndex = 1;
			this.textPan.Text = "0";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(163, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(193, 50);
			this.label1.TabIndex = 5;
			this.label1.Text = ".NET Piano";
			// 
			// PianoForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(551, 454);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1,
																		  this.textVolume,
																		  this.label8,
																		  this.label9,
																		  this.label10,
																		  this.tbarVolume,
																		  this.label5,
																		  this.label6,
																		  this.tbarPan,
																		  this.textPan,
																		  this.label7,
																		  this.buttonExit});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "PianoForm";
			this.Text = "Dot Net Piano";
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PianoForm_MouseDown);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PianoForm_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PianoForm_Paint);
			((System.ComponentModel.ISupportInitialize)(this.tmrUpdate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbarPan)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbarVolume)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}


		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			directSound.Stop();
		}

		private void tbarFreq_Scroll(object sender, System.EventArgs e)
		{
			directSound.Frequency = ((TrackBar)sender).Value;
			//((TrackBar)sender).Value = directSound.lastGoodFrequency;
		}

		private void buttonPlay_Click(object sender, System.EventArgs e)
		{
			PlayNote(5000);
		}

		private void PlayNote(int freq)
		{

			try
			{
				// Before we play, make sure we're using the correct settings
				//					tbarFreq_Scroll(tbarFreq, null);
				tbarPan_Scroll(tbarPan, null);
				tbarVolume_Scroll(tbarVolume, null);

				directSound.Frequency = freq;

				directSound.Load("ding.wav");
				directSound.Play();
			}
			catch
			{
				DefaultPlayUI();
			}
		}

		private void tbarPan_Scroll(object sender, System.EventArgs e)
		{
			textPan.Text = ((TrackBar)sender).Value.ToString();
			directSound.Pan = ((TrackBar)sender).Value * 500;
		}

		private void tbarVolume_Scroll(object sender, System.EventArgs e)
		{
			textVolume.Text = ((TrackBar)sender).Value.ToString();
			directSound.Volume = ((TrackBar)sender).Value * 100;
		}
		private void EnablePlayUI(bool bEnable)
		{
		}
		private void DefaultPlayUI()
		{
		}
		private void tmrUpdate_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if(!directSound.IsPlaying())
				buttonStop_Click(null, null);
		}
		private void UpdateBehaviorText()
		{
			string sText;
			bool FocusSticky = false;
			bool FocusGlobal = false;
			bool MixHardware = false;
			bool MixSoftware = false;

			// Figure what the user should expect based on the dialog choice
			if (FocusSticky)
			{
				sText = "Buffers with \"sticky\" focus will continue to play if the user switches to another application not using DirectSound.  However, if the user switches to another DirectSound application, all normal-focus and sticky-focus buffers in the previous application are muted.";

			}
			else if (FocusGlobal)
			{
				sText = "Buffers with global focus will continue to play if the user switches focus to another application, even if the new application uses DirectSound. The one exception is if you switch focus to a DirectSound application that uses the DSSCL_WRITEPRIMARY cooperative level. In this case, the global-focus buffers from other applications will not be audible.";
			}
			else
			{
				// Normal focus
				sText = "Buffers with normal focus will mute if the user switches focus to any other application";
			}

			if (MixHardware)
			{
				sText = sText + "\n\nWith the hardware mixing flag, the new buffer will be forced to use hardware mixing. If the device does not support hardware mixing or if the required hardware resources are not available, the call to the DirectSound.CreateSoundBuffer method will fail."; 
			}
			else if (MixSoftware)
			{
				sText = sText + "\n\nWith the software mixing flag, the new buffer will use software mixing, even if hardware resources are available.";
			}
			else 
			{
				// Default mixing
				sText = sText + "\n\nWith default mixing, the new buffer will use hardware mixing if available, otherwise software mixing will be used."; 
			}
		}

		private void RadioChecked(object sender, System.EventArgs e)
		{
			UpdateBehaviorText();
		}

		void DrawCurrentKey(Graphics g, PianoKey p)
		{
			Rectangle r = p.Border;
			g.DrawRectangle(Pens.Red, r);
		}

		private void PianoForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			// draw all the keys


			foreach (PianoKey k  in WhiteKeys)
			{
				k.Draw(g);
			}



			foreach (PianoKey k  in BlackKeys)
			{
				k.Draw(g);
			}

		}

		private int FindFrequency(Point p, out PianoKey matchKey)
		{
			// check the black keys first
			foreach (PianoKey k in BlackKeys)
			{
				if (k.IsContained(p))
				{
					matchKey = k;
					return k.TheFrequency;
				}
			}

			// check the white keys
			foreach (PianoKey k in WhiteKeys)
			{
				if (k.IsContained(p))
				{
					matchKey = k;
					return k.TheFrequency;
				}
			}

			matchKey = null;
			return -1;

		}

		private void PianoForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int freq = FindFrequency(new Point(e.X, e.Y), out CurrentKey);
			if (CurrentKey != null)
				Invalidate(CurrentKey.Border);
			PlayNote(freq);
		}

		private void PianoForm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (CurrentKey != null)
			{
				Invalidate(CurrentKey.Border);
				CurrentKey = null;
			}
		}
	}
}
