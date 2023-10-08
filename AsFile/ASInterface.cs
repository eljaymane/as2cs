using System;
using System.Text.RegularExpressions;
using as2cs_home.Abstractions;
using as2cs_home.config;
namespace as2cs_home.AsFile
{
	public class ASInterface : AbstractClass
    {
		public ASInterface(string declarationLine) : base()
		{
            initialize(declarationLine);
		}

        public ASInterface() : base()
        {

        }

        public override void addMethod(AbstractMethod method)
        {
            this._methods.Add(method);
        }

        public override string getClassAccessor(string declarationLine)
        {
            Match m = Regex.Match(declarationLine, GlobalConf.asClassAccessor);
            return m.Value.Trim();
        }

        public override string getClassImplements(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override string getClassName(string declarationLine)
        {
            Match m = Regex.Match(declarationLine, GlobalConf.asInterfaceName);
            return m.Value.Trim();
        }

        public override string getClassType(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override void initialize(string classDeclaration)
        {
            this._className = getClassName(classDeclaration);
            this._classAccessor = getClassAccessor(classDeclaration);
        }

        public override void setConstructor(AbstractMethod method)
        {
            throw new NotImplementedException();
        }


    }
}

