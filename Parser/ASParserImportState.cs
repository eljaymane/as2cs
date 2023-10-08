using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using as2cs_home.Abstractions;
namespace as2cs_home.Parser
{
    public class ASParserImportState : AbstractState
    {
        public ASParserImportState() : base()
        {
            
        }

        public override void handle(IContext ctx){
            var context = (ASClassParser)ctx;
            context._currentFile.addImport(context._currentLine);
            context.parseNext();
        }
    }
}