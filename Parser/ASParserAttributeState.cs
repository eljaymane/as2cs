using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using as2cs_home.AsFile;
using as2cs_home.Abstractions;

namespace as2cs_home.Parser
{
    public class ASParserAttributeState : AbstractState
    {
        public ASParserAttributeState() : base()
        {
            
        }

        public override void handle(IContext ctx){
            var context = (ASClassParser)ctx;
            ASAttribute attribute = new(context._currentLine);
            context._currentFile.addAttribute(attribute);
            context.parseNext();
        }
    }
}