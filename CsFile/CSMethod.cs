global using as2cs_home.AsFile;
using System;
using System.Reflection;
using as2cs_home.Abstractions;

namespace as2cs_home.CsFile
{
    public class CSMethod : AbstractMethod
    {
        public CSMethod(AbstractMethod asMethod) : base()
        {
            this._methodAccessor = asMethod._methodAccessor;
            this._methodArgs = convertArgsTypes(asMethod._methodArgs);
            this._methodBody = convertBodyLines(asMethod._methodBody);
            this._methodName = asMethod._methodName;
            this._methodStatic = asMethod._methodStatic;
            this._returnType = ASToCSConversionHelper.AsToCsType(asMethod._returnType);
        }

        private IList<AbstractInstruction> convertInstructions(IList<AbstractInstruction> asInstruction)
        {
            var instructions = new List<AbstractInstruction>();

            foreach(var instruction in asInstruction)
            {
                var csInstruction = new CSInstruction(instruction);
                instructions.Add(csInstruction);
            }

            return instructions;
        }

        private IList<AbstractInstruction> convertBodyLines(IList<AbstractInstruction> instructions)
        {
            var lines = new List<AbstractInstruction>();
            foreach(var instruction in instructions)
            {
                lines.Add(new CSInstruction((ASInstruction)instruction));   
            }
            return lines;
        }

        private Dictionary<string,string> convertArgsTypes(IDictionary<string, string> args)
        {
            var argsList = new Dictionary<string, string>();

            foreach (var arg in args.Keys)
            {
                args.TryGetValue(arg, out string value);
                argsList.Add(arg, ASToCSConversionHelper.AsToCsType(value));
            }

            return argsList;
        }


        public override string getMethodAccessor(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, string> getMethodArgs(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override string getMethodName(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override string getMethodStatic(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override string getReturnType(string declarationLine)
        {
            throw new NotImplementedException();
        }

        public override bool isVoid()
        {
            throw new NotImplementedException();
        }
    
    }
}