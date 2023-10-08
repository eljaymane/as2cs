using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using as2cs_home.Abstractions;
using as2cs_home.AsFile;
using as2cs_home.config;
namespace as2cs_home.Parser
{
    public class ASParserFunctionState : AbstractState
    {
        private ASMethod asMethod;
        public ASParserFunctionState() : base()
        {
            
        }

        public override void handle(IContext ctx){
            var context = (ASClassParser)ctx;
            asMethod = new(context._currentLine);
            if(context._currentFile._class.GetType() == typeof(ASInterface))
            {
                context._currentFile._class.addMethod(new ASMethod(context._currentLine));

            } else
            {
                context._iterator += 2;
                do
                {
                    asMethod.addInstruction(new ASInstruction(context._currentFile.asFile[context._iterator].Trim()));
                    context._iterator++;

                } while (context._currentFile.asFile[context._iterator].Trim() != "}");
                if (GlobalConf.ASIsCtor(context._currentLine))
                {
                    context._currentFile._class.setConstructor(asMethod);
                }
                else
                {
                    context._currentFile.addMethod(asMethod);
                }
            }
            
            context.parseNext();
        }
    }
}