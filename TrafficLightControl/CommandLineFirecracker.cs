using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightControl
{
	class CommandLineFirecracker : IX10Controller
	{
		public void SendCommands(string comPort, string houseCode, List<CmdPart> commands)
		{
			// C:\Noah\Projects\X-10\Apps\cm17a\cm17a 11 b2off b3on b3off b4on b4off b2on
			string cmds = String.Join(" ", commands.Select(a => String.Format("{0}{1}{2}", houseCode.ToLower(), a.UnitCode, a.OnOff ? "on" : "off")).ToArray());
			string args = String.Format(@"{0} {1}", String.Concat(comPort.ToCharArray().Where(a => char.IsDigit(a))), cmds);
			ProcessStartInfo psi = new ProcessStartInfo(@"C:\Noah\Projects\X-10\Apps\cm17a\cm17a.exe", args) { WindowStyle = ProcessWindowStyle.Hidden };
			Trace.WriteLine(String.Format("Running command: {0} {1}", psi.FileName, psi.Arguments));
			Process.Start(psi).WaitForExit();
		}
	}
}
