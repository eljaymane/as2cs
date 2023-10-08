using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as2cs_home.Abstractions
{
    public abstract class AbstractMethod : IWritable
    {
        public string _methodAccessor { get; set; }
        public string _methodOverride { get; set; }
        public string _methodStatic { get; set; }
        public string _methodName { get; set; }
        public string _super { get; set; }
        public IDictionary<string,string> _methodArgs { get; set; }
        public string _returnType { get; set; }
        public IList<AbstractInstruction> _methodBody { get; set; }

        public AbstractMethod()
        {
            _methodBody = new List<AbstractInstruction>();
            _methodArgs = new Dictionary<string,string>();
            this._super = "";
            this._returnType = "";
        }

        public AbstractMethod(string accessor,string methodOverride,string _static,string _name,IDictionary<string,string> _args,string _returnType, IList<AbstractInstruction> _body)
        {
            this._methodAccessor = accessor;
            this._methodOverride = methodOverride;
            this._methodStatic = _static;
            this._methodName = _name;
            this._methodArgs = _args;
            this._returnType = _returnType;
            this._methodBody = _body;
            this._super = "";
        }

        public abstract string getMethodName(string declarationLine);

        public abstract string getMethodAccessor(string declarationLine);

        public abstract string getMethodStatic(string declarationLine);

        public abstract IDictionary<string,string> getMethodArgs(string declarationLine);

        public abstract string getReturnType(string declarationLine);

        public abstract bool isVoid();

        public Task toLines(ref int index, ref string[] lines)
        {
            lines[index] = $"{_methodAccessor} {_methodOverride} {_methodStatic} {_returnType} {_methodName}(";
            writeMethodArgs(ref index, ref lines);
            writeSuper(ref index, ref lines);
            lines[index] = "{\n";
            index++;
            writeInstructions(ref index, ref lines);
            lines[index] = "}\n";
            index++;
            return Task.CompletedTask;
        }

        public Task writeInstructions(ref int index, ref string[] lines)
        {
            if(_methodBody.Where(instruction => instruction._instructionType != Enums.InstructionTypeEnum.NOP).Count()>0)
            {
                foreach(var instruction in _methodBody)
                {
                    lines[index] = instruction._instructionLine+"\n";
                    index++;
                }
            } else
            {
                lines[index] = "\n";
                index++;
            }
            return Task.CompletedTask;
        }

        public Task writeSuper(ref int index, ref string[] lines)
        {
            if (_super != null && _super != "")
            {
                lines[index] += $"{_super}";
                index++;
            }
            else
            {
                lines[index] += "\n";
                index++;
            }
            return Task.CompletedTask;
        }

        public Task writeMethodArgs(ref int index, ref string[] lines)
        {
            if (_methodArgs.Count > 0)
            {
                foreach (var arg in _methodArgs)
                {
                    lines[index] += $"{arg.Value} {arg.Key},";
                }
                lines[index] = lines[index].Substring(0, lines[index].Length - 1);
            }
                lines[index] += ") ";

            return Task.CompletedTask;
        }

        public void addInstruction(AbstractInstruction instruction)
        {
            this._methodBody.Add(instruction);
        }
    }
}