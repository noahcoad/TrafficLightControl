using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TrafficLightControl
{
	class Program
	{
		private NotifyIcon trayIcon = null;
		private enum Colors { Red, Green, Yellow, None, Unknown };
		private Properties.Settings settings = Properties.Settings.Default;
		private Colors LastLight = Colors.Unknown;
		private IX10Controller x10device = new PythonFirecracker();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			new Program();
			Application.Run();
		}

		public Program()
		{
			CreateNotifyicon();
			if (settings.TurnAllLightsOffAtStartup) ChangeLights(Colors.None);
		}

		private void QueueCommands(List<CmdPart> cmds)
		{ TaskQueue.Default.EnqueueAction(() => x10device.SendCommands(settings.ComPort, settings.HouseCode, cmds)); }

		private void CreateNotifyicon()
		{
			// top level items
			var components = new Container();
			var mnuContext = new ContextMenuStrip();

			// create color menus
			Func<string, ToolStripMenuItem> ItemBuilder = a => new ToolStripMenuItem() 
				{ Text = "&" + a, Image = Bitmap.FromStream(GetResource(a.ToLower() + ".png")), Tag = Enum.Parse(typeof(Colors), a) };
			mnuContext.Items.AddRange(new[] { ItemBuilder("Green"), ItemBuilder("Yellow"), ItemBuilder("Red") });

			// attach event handlers
			mnuContext.Items.Cast<ToolStripMenuItem>().ToList().
				ForEach(a => a.Click += (b, c) => ChangeLights((Colors)(b as ToolStripMenuItem).Tag));

			// all off menu
			var mnuAllOff = new ToolStripMenuItem() { Text = "&All Off" };
			mnuAllOff.Click += (a, b) => ChangeLights(Colors.None);
			mnuContext.Items.Add(mnuAllOff);

			// add exit menu
			var mnuExit = new ToolStripMenuItem() { Text = "E&xit" };
			mnuExit.Click += (a, b) => Application.Exit();
			mnuContext.Items.AddRange(new ToolStripItem[] { new ToolStripSeparator(), mnuExit });

			// create the NotifyIcon
			this.trayIcon = new NotifyIcon(components) {
				Text = "Traffic Light Controller",
				Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
				ContextMenuStrip = mnuContext, 
				Visible = true };
		}

		private void ChangeLights(Colors light)
		{
			if (light == Colors.Unknown) throw new ArgumentException("setting unknown color not supported", "light");

			// queue of commands
			List<CmdPart> cmds = new List<CmdPart>();

			// turn on the desired color (if any)
			if (light != Colors.None && light != Colors.Unknown)
				cmds.Add(new CmdPart(GetUnitCode(light), true));

			// turn off other lights
			// light = none   last = ___      clear all starting with last
			// light = color  last = none     do nothing
			// light = color  last = color    do nothing
			// light = color  last = other    turn off last
			// light = color  last = unknown  turn off other colors
			var lights = new List<Colors> { Colors.Green, Colors.Yellow, Colors.Red };
			var turnoff = new List<Colors>();
			if (light == Colors.None)
			{
				if (lights.Contains(LastLight)) turnoff.Add(LastLight);
				turnoff.AddRange(lights.Except(turnoff));
			}
			else if (LastLight == Colors.None) { }
			else if (LastLight == Colors.Unknown) { turnoff.AddRange(lights.Except(new[] { light })); }
			else if (light != LastLight) turnoff.Add(LastLight);
			turnoff.ForEach(a => cmds.Add(new CmdPart(GetUnitCode(a), false)));

			// make sure the specified port is on the computer
			var ports = SerialPort.GetPortNames();
			if (!ports.Contains(settings.ComPort))
			{
				var msg = String.Format("The ComPort name specified in settings ({0}) is not one of the ports on the machine ({1}).  Please update your settings.", settings.ComPort, ports.Length == 0 ? "no ports detected" : String.Join(", ", ports));
				MessageBox.Show(msg, "ComPort Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error); 
				return; 
			}

			// queue up the list of commands to run
			QueueCommands(cmds);

			// remember the last set light
			LastLight = light;
		}

		private int GetUnitCode(Colors color)
		{ return (int)settings[Enum.GetName(typeof(Colors), color) + "UnitCode"]; }

		private static Stream GetResource(string key)
		{
			var a = Assembly.GetExecutingAssembly();
			return a.GetManifestResourceStream(a.GetName().Name + "." + key);
		}
	}
}
