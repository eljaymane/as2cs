using System;
using as2cs_home.Abstractions;
using as2cs_home.CsFile;

namespace as2cs_home.AsFile
{
	public class ASInstruction : AbstractInstruction
	{
		public ASInstruction(string instructionLine) : base (instructionLine)
		{
            this._instructionType = getInstructionType(instructionLine);
            if (_instructionType == Enums.InstructionTypeEnum.NOP && instructionLine.Trim() == "}" || instructionLine.Trim() == "{") this._instructionLine="";
		}

        public string convertLine(string line)
        {
            return line;
        }

        public override Task toLines(ref int index, ref string[] lines)
        {
            return Task.CompletedTask;
        }
    }
}

