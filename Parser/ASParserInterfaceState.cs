using System;
using as2cs_home.AsFile;

namespace as2cs_home.Parser
{
	public class ASParserInterfaceState : AbstractState
    {
		public ASParserInterfaceState(): base()
		{
			
		}

        public override void handle(IContext ctx)
        {
            var context = (ASClassParser)ctx;
            context._currentFile._class = new ASInterface(context._currentLine);
            context.parseNext();
        }
    }
}

