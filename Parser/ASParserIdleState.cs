using System;
namespace as2cs_home.Parser
{
        public class ASParserIdleState : AbstractState
        {

        public ASParserIdleState() : base()
        {

        }

        public override void handle(IContext ctx)
        {
            var context = (ASClassParser)ctx;
            context._iterator = -1;
            context._currentLine = "";
        }

        }
}

