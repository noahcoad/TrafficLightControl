using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Imperium.X10;

namespace TrafficLightControl
{
	class ImperiumFirecracker : IX10Controller
	{
		public void SendCommands(string comPort, string houseCode, List<CmdPart> commands)
		{
			using (var cracker = CM17A.Instance(comPort))
				foreach (var cmd in commands)
				{ cracker.SendCommand((X10HouseCode)Enum.Parse(typeof(X10HouseCode), houseCode), cmd.UnitCode, cmd.OnOff ? X10Command.TurnOn : X10Command.TurnOff); Thread.Sleep(100); }
		}
	}
}
