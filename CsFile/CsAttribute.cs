using System;
using as2cs_home.AsFile;
using as2cs_home.Abstractions;

namespace as2cs_home.CsFile
{
	public class CsAttribute : AbstractAttribute
    {
		public CsAttribute() :base()
		{
		}

        public CsAttribute(AbstractAttribute aSAttribute) : base()
        {
            this._accessor = aSAttribute._accessor;
            this._const = aSAttribute._const;
            this._final = aSAttribute._final;
            this._name = aSAttribute._name;
            this._value = aSAttribute._value;
            this._static = aSAttribute._static;
            this._type = ASToCSConversionHelper.AsToCsType(aSAttribute._type);
        }

        public override string getAccessor(string line)
        {
            return this._accessor;
        }

        public override string getConst(string line)
        {
            return this._const;
        }

        public override string getFinal(string line)
        {
            return this._final;
        }

        public override string getName(string line)
        {
            return this._name;
        }

        public override string getStatic(string line)
        {
            throw new NotImplementedException();
        }

        public override string getType(string line)
        {
            throw new NotImplementedException();
        }

        public override string getValue(string line)
        {
            throw new NotImplementedException();
        }

    }
}

