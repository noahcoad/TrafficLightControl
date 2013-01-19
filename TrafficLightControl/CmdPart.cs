using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightControl
{
	struct CmdPart
	{
		public int UnitCode;
		public bool OnOff;
		public CmdPart(int UnitCode, bool OnOff)
		{ this.UnitCode = UnitCode; this.OnOff = OnOff; }
	}
}
