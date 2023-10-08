using System;
using as2cs_home.Abstractions;
namespace as2cs_home.CsFile
{
	public class CSInterface : AbstractClass
	{
		public CSInterface(AbstractClass aSInterface) : base()
		{
            this._classAccessor = aSInterface._classAccessor;
            this._className = aSInterface._className;
            this._implements = aSInterface._implements;
		}

        public override void addMethod(AbstractMethod method)
        {
            throw new NotImplementedException();
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

        public override Task writeClassHeader(ref int index, ref string[] lines)
        {
            lines[index] = $"{_classAccessor} {_classType} interface {_className} {_implements}\n";
            index++;
            return Task.CompletedTask;
        }
    }
}

