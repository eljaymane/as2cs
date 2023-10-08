using System.Text.RegularExpressions;
using as2cs_home.AsFile;
using as2cs_home.config;
using as2cs_home.Enums;
using as2cs_home.Abstractions;

namespace as2cs_home.CsFile
{
    public static class ASToCSConversionHelper
    {
        public static string AsToCsType(string asType)
        {
            var result = asType.Replace(":", "");//Purfication
            switch (result)            {
                case ASTypesEnum.ARRAY:
                    {
                        return CSTypesEnum.LIST;
                    }
                case ASTypesEnum.VECTOR:
                    {
                        return CSTypesEnum.LIST;
                    }
                case ASTypesEnum.STRING:
                    {
                        return CSTypesEnum.STRING;
                    }
                case ASTypesEnum.VOID:
                    {
                        return CSTypesEnum.VOID;
                    }
                case ASTypesEnum.UINT:
                    {
                        return CSTypesEnum.UINT;
                    }
                case ASTypesEnum.OBJECT:
                    {
                        return CSTypesEnum.OBJECT;
                    }
                case ASTypesEnum.INT:
                    {
                        return CSTypesEnum.INT;
                    }
                case ASTypesEnum.DATE:
                    {
                        return CSTypesEnum.DATE;
                    }
                case ASTypesEnum.BOOLEAN:
                    {
                        return CSTypesEnum.BOOLEAN;
                    }
                case ASTypesEnum.NUMBER:
                    {
                        return CSTypesEnum.DOUBLE;
                    }
                case ASTypesEnum.TIMER:
                    {
                        return CSTypesEnum.TIMER;
                    }
             
                default:
                    { }

                    Match m = Regex.Match(result, ASTypesEnum.TYPED);
                    if (m.Success)
                    {
                        var rootType = Regex.Match(result, GlobalConf.nestedTypes).Value;
                        result = rootType+"<";
                        var types = m.Value.Split(",");

                        
                        foreach (var type in types)
                        {
                            result += AsToCsType(type) + ",";
                        }
                        result = result.Substring(result.Length - 1)+">";
                    }
                    return result;

            }
        }

     

        public static string AsToCsInstruction(AbstractInstruction asInstruction)
        {
            var result = asInstruction;
            switch (asInstruction._instructionType)
            {
                case InstructionTypeEnum.DECLARATION | InstructionTypeEnum.ASSIGNATION:
                    {
                        result._instructionLine = convertDeclaration(result);
                        break;
                    }
                case InstructionTypeEnum.FOR:
                    {
                        result._instructionLine = convertDeclaration(result);
                        break;
                    }
                default:
                    {
                        break;
                    }   
            }
            return result._instructionLine;
        }

    private static string convertDeclaration(AbstractInstruction instruction)
    {
            Match m = Regex.Match(instruction._instructionLine, GlobalConf.asAttributeType);
            var result = m.Success ? m.Value : "";
            if (m.Success || instruction._instructionLine.Contains("var"))
            {
                return instruction._instructionLine.Replace("var", AsToCsType(m.Value.Replace(".", "").Replace($":{result}", "")));
            }
        
            else
            {
                return instruction._instructionLine;
            }
    }


    }
}

