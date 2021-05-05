
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.IO;
using FSDirectShow;
using System.Collections.Generic;

namespace DirectSoundDemo {
	public class FrmMain : System.Windows.Forms.Form {
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.TrackBar trkBalance;
		private System.Windows.Forms.TrackBar trkVolume;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.TrackBar trkSound;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbSpeakers;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbAudioCards;
		private System.Windows.Forms.ComboBox cmbAudioEffects;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblSound;

		private FSDirectShow.DirectSound directSound = new FSDirectShow.DirectSound();
		private List<DeviceInfo> devList;
		//private Device dSound;
		//private BufferDescription d;
		//private SecondaryBuffer sound;
		private System.Windows.Forms.Timer t;
		private int len;
		private bool playing = false;
		private string currFile = Application.StartupPath + "\\Audio.wav";

		public FrmMain() {
			InitializeComponent();

			directSound.Initialize(this.Handle);

			// List all Audio Cards
			devList = directSound.Devices();
			// Populate cmbAudioCards
			cmbAudioCards.Items.Clear();
			for(int i = 0; i < devList.Count; i++) {
				cmbAudioCards.Items.Add(devList[i].Description);
			}
			cmbAudioCards.SelectedIndex = 0;

			// Create Device
			//dSound = new Device(devList[0].DriverGuid);
			//dSound.SetCooperativeLevel(this.Handle, CooperativeLevel.Priority);
			//d = new BufferDescription();
			//d.Flags = BufferDescriptionFlags.ControlVolume | BufferDescriptionFlags.ControlFrequency | BufferDescriptionFlags.ControlPan | BufferDescriptionFlags.ControlEffects;
		}

		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Forms Design Tools generated code
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnLoad = new System.Windows.Forms.Button();
			this.trkBalance = new System.Windows.Forms.TrackBar();
			this.btnPlay = new System.Windows.Forms.Button();
			this.btnPause = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.trkVolume = new System.Windows.Forms.TrackBar();
			this.lblSound = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.cmbSpeakers = new System.Windows.Forms.ComboBox();
			this.cmbAudioCards = new System.Windows.Forms.ComboBox();
			this.cmbAudioEffects = new System.Windows.Forms.ComboBox();
			this.trkSound = new System.Windows.Forms.TrackBar();
			this.lblInfo = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trkBalance)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkVolume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkSound)).BeginInit();
			this.SuspendLayout();
			// 
			// btnLoad
			// 
			this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLoad.Location = new System.Drawing.Point(8, 8);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(80, 24);
			this.btnLoad.TabIndex = 0;
			this.btnLoad.Text = "Load";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// trkBalance
			// 
			this.trkBalance.Location = new System.Drawing.Point(56, 104);
			this.trkBalance.Maximum = 10000;
			this.trkBalance.Minimum = -10000;
			this.trkBalance.Name = "trkBalance";
			this.trkBalance.Size = new System.Drawing.Size(160, 45);
			this.trkBalance.TabIndex = 1;
			this.trkBalance.TickFrequency = 1000;
			this.toolTip.SetToolTip(this.trkBalance, "Balance");
			this.trkBalance.Scroll += new System.EventHandler(this.trkBalance_Scroll);
			// 
			// btnPlay
			// 
			this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPlay.Location = new System.Drawing.Point(96, 8);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(80, 24);
			this.btnPlay.TabIndex = 2;
			this.btnPlay.Text = "Play";
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// btnPause
			// 
			this.btnPause.Enabled = false;
			this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPause.Location = new System.Drawing.Point(184, 8);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(80, 24);
			this.btnPause.TabIndex = 3;
			this.btnPause.Text = "Pause";
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStop.Location = new System.Drawing.Point(272, 8);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(80, 24);
			this.btnStop.TabIndex = 4;
			this.btnStop.Text = "Stop";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// trkVolume
			// 
			this.trkVolume.Location = new System.Drawing.Point(8, 96);
			this.trkVolume.Maximum = 0;
			this.trkVolume.Minimum = -3000;
			this.trkVolume.Name = "trkVolume";
			this.trkVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trkVolume.Size = new System.Drawing.Size(45, 112);
			this.trkVolume.TabIndex = 5;
			this.trkVolume.TickFrequency = 200;
			this.trkVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.toolTip.SetToolTip(this.trkVolume, "Volume");
			this.trkVolume.Scroll += new System.EventHandler(this.trkVolume_Scroll);
			// 
			// lblSound
			// 
			this.lblSound.Location = new System.Drawing.Point(8, 40);
			this.lblSound.Name = "lblSound";
			this.lblSound.Size = new System.Drawing.Size(344, 16);
			this.lblSound.TabIndex = 10;
			this.lblSound.Text = "0/0";
			this.lblSound.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmbSpeakers
			// 
			this.cmbSpeakers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSpeakers.Items.AddRange(new object[] {
															 "Stereo Speakers",
															 "Stereo Headphones",
															 "Dolby 4.0",
															 "Dolby 5.1",
															 "Dolby 7.1"});
			this.cmbSpeakers.Location = new System.Drawing.Point(56, 176);
			this.cmbSpeakers.Name = "cmbSpeakers";
			this.cmbSpeakers.Size = new System.Drawing.Size(152, 21);
			this.cmbSpeakers.TabIndex = 15;
			this.toolTip.SetToolTip(this.cmbSpeakers, "Speakers configuration (seems to have no effect on std stereo speakers)");
			this.cmbSpeakers.SelectedIndexChanged += new System.EventHandler(this.cmbSpeakers_SelectedIndexChanged);
			// 
			// cmbAudioCards
			// 
			this.cmbAudioCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAudioCards.Location = new System.Drawing.Point(8, 224);
			this.cmbAudioCards.Name = "cmbAudioCards";
			this.cmbAudioCards.Size = new System.Drawing.Size(344, 21);
			this.cmbAudioCards.TabIndex = 17;
			this.toolTip.SetToolTip(this.cmbAudioCards, "Output Audio Card");
			this.cmbAudioCards.SelectedIndexChanged += new System.EventHandler(this.cmbAudioCards_SelectedIndexChanged);
			// 
			// cmbAudioEffects
			// 
			this.cmbAudioEffects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAudioEffects.Items.AddRange(new object[] {
																 "None",
																 "Chorus",
																 "Compressor",
																 "Distortion",
																 "Echo",
																 "Flanger",
																 "Gargle",
																 "Interactive 3D Level2 Reverb",
																 "Param Equalization",
																 "Waves Reverb"});
			this.cmbAudioEffects.Location = new System.Drawing.Point(8, 272);
			this.cmbAudioEffects.Name = "cmbAudioEffects";
			this.cmbAudioEffects.Size = new System.Drawing.Size(344, 21);
			this.cmbAudioEffects.TabIndex = 19;
			this.toolTip.SetToolTip(this.cmbAudioEffects, "Output Audio Card");
			this.cmbAudioEffects.SelectedIndexChanged += new System.EventHandler(this.cmbAudioEffects_SelectedIndexChanged);
			// 
			// trkSound
			// 
			this.trkSound.Location = new System.Drawing.Point(8, 56);
			this.trkSound.Name = "trkSound";
			this.trkSound.Size = new System.Drawing.Size(344, 45);
			this.trkSound.TabIndex = 12;
			this.trkSound.Scroll += new System.EventHandler(this.trkSound_Scroll);
			// 
			// lblInfo
			// 
			this.lblInfo.Location = new System.Drawing.Point(224, 104);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(128, 96);
			this.lblInfo.TabIndex = 13;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(56, 160);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 16);
			this.label1.TabIndex = 14;
			this.label1.Text = "Speakers Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 208);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(344, 16);
			this.label2.TabIndex = 16;
			this.label2.Text = "Audio Card";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 256);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(344, 16);
			this.label3.TabIndex = 18;
			this.label3.Text = "Audio Effect";
			// 
			// FrmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(362, 306);
			this.Controls.Add(this.cmbAudioEffects);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmbAudioCards);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbSpeakers);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.trkSound);
			this.Controls.Add(this.lblSound);
			this.Controls.Add(this.trkVolume);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.btnPlay);
			this.Controls.Add(this.trkBalance);
			this.Controls.Add(this.btnLoad);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DirectSound";
			this.Load += new System.EventHandler(this.FrmMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.trkBalance)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkVolume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkSound)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread] static void Main() {
			Application.Run(new FrmMain());
		}

		private void FrmMain_Load(object sender, System.EventArgs e) {
			prepareSound(currFile);
		}

		private void prepareSound(string file) {
			try {
				playing = false;
				directSound.Stop();
			}
			catch {}
			directSound.Load(file);
			directSound.Volume = trkVolume.Value;
			len = directSound.BufferBytes;

			trkSound.Minimum = 0;
			trkSound.Maximum = len;
			trkSound.SmallChange = 1;
			trkSound.LargeChange = 1;
			trkSound.TickFrequency = len;
			
			writeTime(0);

			t = new System.Windows.Forms.Timer();
			t.Interval = 100;
			t.Tick += new EventHandler(t_Tick);

			string info = "Sound Info:\n";
			info += "Sample Freq: " + directSound.SamplesPerSecond.ToString() + "\n";
			info += "Bit/Sample: " + directSound.BitsPerSample.ToString() + "\n";
			info += "Channels: " + directSound.Channels.ToString() + "\n";
			info += "Tot Bytes: " + directSound.BufferBytes.ToString() + "\n";
			info += "Bytes/sec: " + directSound.AverageBytesPerSecond.ToString() + "\n";
			info += "Duration: " + ((int)(len / directSound.AverageBytesPerSecond)).ToString() + " sec\n";
			lblInfo.Text = info;
		}

		private void btnLoad_Click(object sender, System.EventArgs e) {
			OpenFileDialog d = new OpenFileDialog();
			d.Filter = "Wave Files (*.wav)|*.wav";
			if(d.ShowDialog() == DialogResult.OK) {
				currFile = d.FileName;
				prepareSound(currFile);
			}
		}

		private void btnPlay_Click(object sender, System.EventArgs e) {
			directSound.Play();
			btnPause.Enabled = true;
			btnStop.Enabled = true;
			btnPlay.Enabled = false;
			playing = true;
			t.Enabled = true;
			t.Start();
		}

		private void btnPause_Click(object sender, System.EventArgs e) {
			directSound.Stop(); // It works like a Pause
			btnPlay.Enabled = true;
			btnPause.Enabled = false;
			btnStop.Enabled = true;
		}

		private void btnStop_Click(object sender, System.EventArgs e) {
			playing = false;
			directSound.Stop();
			directSound.SetCurrentPosition(0); // Rewind
			writeTime(0);
			btnPlay.Enabled = true;
			btnPause.Enabled = false;
			btnStop.Enabled = false;
		}

		private void trkVolume_Scroll(object sender, System.EventArgs e) {
			try {
				if(trkVolume.Value == trkVolume.Minimum) {
					directSound.Volume = -10000;
				}
				else {
					directSound.Volume = trkVolume.Value;
				}
			}
			catch {}
		}

		private void trkBalance_Scroll(object sender, System.EventArgs e) {
			try {
				directSound.Pan = trkBalance.Value;
			}
			catch {}
		}

		private void t_Tick(object sender, System.EventArgs e) {
			if(playing) {
				// Update UI
				writeTime(directSound.PlayPosition);
				if(directSound.IsPlaying()) {
					// All done
				}
				else {
					// Sound Paused or finished
					if(directSound.PlayPosition >= len - 1 || directSound.PlayPosition == 0) {
						// Finished
						playing = false;
					}
					else {
						// Paused: stop timer (the next hit on Play will restart it)
						t.Stop();
						t.Enabled = false;
					}
				}
			}
			if(!playing) {
				// Sound finished or stopped
				t.Stop();
				t.Enabled = false;
				writeTime(0);
				btnPlay.Enabled = true;
				btnPause.Enabled = false;
				btnStop.Enabled = false;
				playing = false;
			}
		}

		private void trkSound_Scroll(object sender, System.EventArgs e) {
			if(trkSound.Value < len) {
				if(playing) {
					directSound.Stop();
					directSound.SetCurrentPosition(trkSound.Value);
					writeTime(trkSound.Value);
					directSound.Play();
				}
				else {
					directSound.SetCurrentPosition(trkSound.Value);
					writeTime(trkSound.Value);
				}
			}
			else {
				directSound.SetCurrentPosition(0);
				writeTime(0);
			}
		}

		private void writeTime(int pos) {
			int toth = 0;
			int totm = 0;
			int tots = 0;

			int tot = len / directSound.AverageBytesPerSecond;
			toth = tot / 3600;
			totm = (tot - (toth * 3600)) / 60;
			tots = tot - (toth * 3600) - (totm * 60);

			int h = 0;
			int m = 0;
			int s = 0;

			int now = pos / directSound.AverageBytesPerSecond;
			h = now / 3600;
			m = (now - (h * 3600)) / 60;
			s = now - (h * 3600) - (m * 60);

			string res = "";
			if(toth > 0) {
				res += h.ToString() + ":";
			}
			res += m.ToString() + ":" + string.Format("{0:00}", s) + " / ";
			if(toth > 0) {
				res += toth.ToString() + ":";
			}
			res += totm.ToString() + ":" + string.Format("{0:00}", tots);
			trkSound.Value = pos;
			lblSound.Text = res;
		}

		private void cmbSpeakers_SelectedIndexChanged(object sender, System.EventArgs e) {

			try
			{
				switch (cmbSpeakers.SelectedIndex)
				{
					case 0: // Stereo Speakers
						directSound.SpeakerConfig(true, false, false, false, false);
						break;
					case 1: // Stereo Headphones
						directSound.SpeakerConfig(false, true, false, false, false);
						break;
					case 2: // Dolby 4.0
						directSound.SpeakerConfig(false, false, true, false, false);
						break;
					case 3: // Dolby 5.1
						directSound.SpeakerConfig(false, false, false, true, false);
						break;
					case 4: // Dolby 7.1
						directSound.SpeakerConfig(false, false, false, false, true);
						break;
				}

			}
			catch { }
		}

		private void cmbAudioCards_SelectedIndexChanged(object sender, System.EventArgs e) {
			try {
				playing = false;
				directSound.Stop();
				directSound.SetCurrentPosition(0);
				writeTime(0);

				directSound.Initialize(this.Handle, devList[cmbAudioCards.SelectedIndex].DriverGuid);

				prepareSound(currFile);
			}
			catch {}
		}

		private void cmbAudioEffects_SelectedIndexChanged(object sender, System.EventArgs e) {

			// Documentation at
			// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/directx9_m/directx/ref_entry.asp

			//EffectDescription[] fx = new EffectDescription[1]; // For this example use only 1 effect
			// To use more effects, use more elements in the array
			//fx[0] = new EffectDescription();
			prepareSound(currFile);
			switch(cmbAudioEffects.SelectedIndex) {
				case 0: // None
					break;
				case 1: // Chorus
					directSound.SetEffect(EffectList.Chorus);
					//fx[0].GuidEffectClass = DSoundHelper.StandardChorusGuid;
					//sound.SetEffects(fx);
					//EchoEffect echoFx = (EchoEffect)sound.GetEffects(0);
					//EffectsEcho echoParams = echoFx.AllParameters;
					//echoParams.LeftDelay = 0;
					//echoParams.PanDelay = 0;
					//echoParams.RightDelay = 0;
					//echoFx.AllParameters = echoParams;
					break;
				case 2: // Compressor
					directSound.SetEffect(EffectList.Compressor);
					//fx[0].GuidEffectClass = DSoundHelper.StandardChorusGuid;
					//sound.SetEffects(fx);
					break;
				case 3: // Distortion
					directSound.SetEffect(EffectList.Distortion);
					//fx[0].GuidEffectClass = DSoundHelper.StandardDistortionGuid;
					//sound.SetEffects(fx);
					break;
				case 4: // Echo
					directSound.SetEffect(EffectList.Echo);
					//fx[0].GuidEffectClass = DSoundHelper.StandardEchoGuid;
					//sound.SetEffects(fx);
					break;
				case 5: // Flanger
					directSound.SetEffect(EffectList.Flanger);
					//fx[0].GuidEffectClass = DSoundHelper.StandardFlangerGuid;
					//sound.SetEffects(fx);
					break;
				case 6: // Gargle
					directSound.SetEffect(EffectList.Gargle);
					//fx[0].GuidEffectClass = DSoundHelper.StandardGargleGuid;
					//sound.SetEffects(fx);
					break;
				case 7: // Interactive 3D Level 2 Reverb
					directSound.SetEffect(EffectList.Interactive3D);
					//fx[0].GuidEffectClass = DSoundHelper.StandardInteractive3DLevel2ReverbGuid;
					//sound.SetEffects(fx);
					break;
				case 8: // Param Aqualizer
					directSound.SetEffect(EffectList.ParamAqualizer);
					//fx[0].GuidEffectClass = DSoundHelper.StandardParamEqGuid;
					//sound.SetEffects(fx);
					//ParamEqEffect eqEffect = (ParamEqEffect)sound.GetEffects(0);
					//EffectsParamEq eqParams = eqEffect.AllParameters;
					//eqParams.Bandwidth = 36;
					//eqParams.Gain = ParamEqEffect.GainMax;
					//eqEffect.AllParameters = eqParams;
					break;
				case 9: // Waves Reverb
					directSound.SetEffect(EffectList.WavesReverb);
					//fx[0].GuidEffectClass = DSoundHelper.StandardWavesReverbGuid;
					//sound.SetEffects(fx);
					break;
			}
		}

	}
}
