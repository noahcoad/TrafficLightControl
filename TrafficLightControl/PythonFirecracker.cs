using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightControl
{
	class PythonFirecracker : IX10Controller
	{
		public void SendCommands(string comPort, string houseCode, List<CmdPart> commands)
		{
			// c:\Python27\python.exe x10.py com11 B All Off, B2 On, B2 Off, B3 On, B3 Off, B4 On, B4 Off, B2 On
			string cmds = String.Join(", ", commands.Select(a => String.Format("{0}{1} {2}", houseCode, a.UnitCode, a.OnOff ? "On" : "Off")).ToArray());
			string args = String.Format(@"c:\code\py\x10.py {0} {1}", comPort.ToLower(), cmds);
			ProcessStartInfo psi = new ProcessStartInfo(@"c:\Python27\python.exe", args) { WindowStyle = ProcessWindowStyle.Hidden };
			Trace.WriteLine(String.Format("Running command: {0} {1}", psi.FileName, psi.Arguments));
			Process.Start(psi).WaitForExit();
		}
	}
}
