using System;
using as2cs_home.Abstractions;
using as2cs_home.AsFile;
using as2cs_home.Enums;

namespace as2cs_home.CsFile
{
	public class CSInstruction : AbstractInstruction
	{
		public CSInstruction(AbstractInstruction asInstruction) : base()
		{
            this._instructionLine = ASToCSConversionHelper.AsToCsInstruction(asInstruction);
            this._instructionType = asInstruction._instructionType;
		}


        public override Task toLines(ref int index, ref string[] lines)
        {
            lines[index] += _instructionLine;
            index++;
            return Task.CompletedTask;
        }
    }
}

