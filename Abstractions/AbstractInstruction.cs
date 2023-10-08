using System;
using as2cs_home.config;
using System.Text.RegularExpressions;
using as2cs_home.Enums;

namespace as2cs_home.Abstractions
{
	public abstract class AbstractInstruction : IWritable
	{
        public InstructionTypeEnum _instructionType { get; set; }
		public string _instructionLine { get; set; }

		public AbstractInstruction()
		{
					
		}

        public AbstractInstruction(string instructionLine) : base()
		{
			_instructionLine = instructionLine;
            
		}

        public InstructionTypeEnum getInstructionType(string line)
        {
            line = line.Trim();

            if ((Regex.Match(line, GlobalConf.asIsDeclaration).Success || line.Contains("var"))&& line.Contains(":") && line.Contains("="))
            {
                return InstructionTypeEnum.DECLARATION;
            }
            else if ((line.Contains("=") || line.Contains(":")) && !line.Contains("var") && !line.Contains("for") && !line.Contains("if") && !line.Contains("where"))
            {
                return InstructionTypeEnum.ASSIGNATION;
            }
            else if (line.Contains(".") && !line.Contains("=") && !line.Contains("var") && !line.Contains("for") && !line.Contains("if") && !line.Contains("where"))
            {
                return InstructionTypeEnum.CALL;
            }
            else if (line.Contains("if("))
            {
                return InstructionTypeEnum.IF;
            }
            else if (line.Contains("else if("))
            {
                return InstructionTypeEnum.ELSEIF;
            }
            else if (line.Contains("while("))
            {
                return InstructionTypeEnum.WHILE;
            }
            else if (line.Contains("for("))
            {
                return InstructionTypeEnum.FOR;
            } else
            {
                return InstructionTypeEnum.NOP;
            }
        }

        public abstract Task toLines(ref int index, ref string[] lines);

    }
}

