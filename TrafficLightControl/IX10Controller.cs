using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightControl
{
	interface IX10Controller
	{
		void SendCommands(string comPort, string houseCode, List<CmdPart> commands);
	}
}
