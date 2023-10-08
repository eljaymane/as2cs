using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using as2cs_home.Abstractions;
using as2cs_home.AsFile;
using as2cs_home.config;
namespace as2cs_home.CsFile
{
    public class CsClass : AbstractClass
    {
        public CsClass() : base()
        {
            
        }

        public CsClass(AbstractClass aSClass) : base()
        {
            this._classAccessor = aSClass._classAccessor;
            this._className = aSClass._className;
            this._classType = aSClass._classType;
            this._attributes = aSClass._attributes != null ? convertAttributes(aSClass._attributes) : null;
            this._methods = aSClass._methods != null ? convertMethods(aSClass._methods) : null;
            this._implements = aSClass._implements != null ? convertImplements(aSClass._implements) : null;
            this._constructor = aSClass._constructor != null ? convertConstructor(aSClass._constructor) : null;
        }

        public AbstractMethod convertConstructor(AbstractMethod asConstructor)
        {
            var csMethod =  new CSMethod(asConstructor);
            foreach(var instruction in csMethod._methodBody)
            {
                if (instruction._instructionLine.Contains("super("))
                {
                    instruction._instructionLine = "";
                    Match m = Regex.Match(instruction._instructionLine, GlobalConf.asSuperArgs);
                    if (m.Success)
                    {
                        csMethod._super = " : base(" + m.Value+")";
                       
                    } else
                    {
                        csMethod._super = " : base()";
                        
                    }
                    
                    
                }
            }
            return csMethod;
        }

        public string convertImplements(string implements)
        {
            if (implements == "" || implements == null) return "";

            if (implements.Contains("extends")) implements.Replace("extends", ":");
            if (implements.Contains(":")) implements.Replace("implements", "");
            else implements.Replace("implements", ":");

            return implements;
        }
        public IList<AbstractMethod> convertMethods(IList<AbstractMethod> methods)
        {
            var csMethods = new List<AbstractMethod>();
            foreach (var method in methods)
            {
                csMethods.Add(new CSMethod(method));
            }
            return csMethods;
        }

        public IList<AbstractAttribute> convertAttributes(IList<AbstractAttribute> attributes)
        {
            var csAttributes = new List<AbstractAttribute>();
            foreach(var ASattribute in attributes)
            {
                csAttributes.Add(new CsAttribute(ASattribute));
            }
            return csAttributes;
        }

        public override string getClassAccessor(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override string getClassImplements(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override string getClassName(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override string getClassType(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override void initialize(string classDeclaration)
        {
            throw new NotImplementedException();
        }

        public override void setConstructor(AbstractMethod method)
        {
            throw new NotImplementedException();
        }

        public override void addMethod(AbstractMethod method)
        {
            throw new NotImplementedException();
        }
    }
}