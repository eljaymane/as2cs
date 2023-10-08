using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using as2cs_home.Abstractions;

namespace as2cs_home.Parser
{
    public class ASParserPackageState : AbstractState
    {
        public ASParserPackageState() : base()
        {
            
        }
        public override void handle(IContext ctx){
            var context = (ASClassParser)ctx;
            context._currentFile.setPackageName(context._currentLine.Replace("package","").Trim());
            context.parseNext();
        }
    }
}