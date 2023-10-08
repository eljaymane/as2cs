using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using as2cs_home.Abstractions;
using as2cs_home.AsFile;
namespace as2cs_home.Parser
{
    public class ASParserClassState : AbstractState
    {
        public ASParserClassState() : base()
        {
            
        }

        public override void handle(IContext ctx){
            var context = (ASClassParser)ctx;
            context._currentFile._class =  new ASClass(context._currentLine);
            context.parseNext();
        }
    }
}