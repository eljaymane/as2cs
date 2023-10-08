using System;
namespace as2cs_home.Parser
{
	public class ASInstructionState : AbstractState
    {
		public ASInstructionState() 
		{
		}

        public override void handle(IContext ctx)
        {
            var context = (ASClassParser)ctx;
            context.parseNext();
        }
    }
}

