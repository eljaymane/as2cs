using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using as2cs_home.Abstractions;
using as2cs_home.config;

namespace as2cs_home.AsFile
{
    public class ASMethod : AbstractMethod
    {
        public ASMethod(string declarationLine) : base()
        {
            this._methodAccessor = getMethodAccessor(declarationLine);
            this._methodStatic = getMethodStatic(declarationLine);
            this._methodOverride = declarationLine.Contains("override") ? "override" : "";
            this._methodName = getMethodName(declarationLine);
            this._methodArgs = getMethodArgs(declarationLine);
            this._returnType  = getReturnType(declarationLine);
            this._methodBody = new List<AbstractInstruction>();
        }

        public override string getMethodName(string declarationLine){
            Match m = Regex.Match(declarationLine,GlobalConf.asMethodName);
            return m.Value;
        }

        public override string getMethodAccessor(string declarationLine){
            Match m = Regex.Match(declarationLine.TrimStart(),GlobalConf.asMethodAccessor);
            return m.Value;
        }

        public override string getMethodStatic(string declarationLine){
            Match m = Regex.Match(declarationLine,GlobalConf.asMethodStatic);
            if(m.Value == this._methodAccessor) this._methodStatic = "";
            return this._methodStatic;
        }

        public override IDictionary<string,string> getMethodArgs(string declarationLine){
            Match m = Regex.Match(declarationLine,GlobalConf.asMethodArgs);
            this._methodArgs = new Dictionary<string,string>();
            string[] args = m.Value.Replace("(","").Replace(")","").Split(",");
            foreach(var arg in args)
            {
               if (arg.Contains("...")) {
                    this._methodArgs.Add(arg.Replace("...","").Trim(), "string[]");
                    continue;
                } else
                {
                    var result = arg;
                    if (arg.Contains("=")) result = result.Split("=")[0];
                    if (result != "") this._methodArgs.Add(result.Split(":")[0], result.Split(":")[1]);
                }
               
            }
            return this._methodArgs;
        }

        public override string getReturnType(string declarationLine){
            Match m = Regex.Match(declarationLine,GlobalConf.asMethodReturnType);
            return m.Value.Replace(":", "").Trim().ToLower();
        }

        public override bool isVoid(){
            return this._returnType == "void" ? true : false;
        }

    }
}