using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using as2cs_home.config;
using as2cs_home.Abstractions;

namespace as2cs_home.AsFile
{
    public class ASAttribute : AbstractAttribute
    {
        public ASAttribute(string attributeLine) : base()
        {
            this._accessor = getAccessor(attributeLine);
            this._static = getStatic(attributeLine);
            this._const = getConst(attributeLine);
            this._const = getFinal(attributeLine);
            this._type = getType(attributeLine);
            this._name = getName(attributeLine);
            this._value = getValue(attributeLine);
        }

        public override string getAccessor(string line){
            Match m = Regex.Match(line,GlobalConf.asAttributeAccessor);
            return m.Value.Trim();
        }

        public override string getStatic(string line){
            if(line.Contains("static")) return "static";
            else return "";
        }

        public override string getFinal(string line){
            if(line.Contains("final")) return "final";
            else return "";
        }

        public override string getConst(string line){
            if(line.Contains("const")) return "const";
            else return "";
        }

        public override string getType(string line){
            Match m = Regex.Match(line,GlobalConf.asAttributeType);
            return m.Value.Replace(":", "").Trim();
        }

        public override string getName(string line){
            Match m = Regex.Match(line,GlobalConf.asAttributeName);
            return m.Value.Replace(":", "").Trim();
        }

        public override string getValue(string line){
            Match m = Regex.Match(line,GlobalConf.asAttributeValue);
            return m.Value.Replace(";", "").Replace("=", "").Trim();
        }

    }
}