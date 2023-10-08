using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace as2cs_home.config
{
    public static class GlobalConf
    {
        public static string fileName_Regex = "(.+\\/)";

        public static string nestedTypes= "@(?<=\\s.*:)([a - zA - Z] _ *).*(?=<)";

        #region ActionScript attributes
        public static string asAttributeAccessor = @"^\s*([a-z]*)(?=\s)";
        public static string asAttributeType = @"(?=:)(.*)(?<!(\s|;|=|\s=|\s=\s.*))";
        public static string asAttributeName = @"\s([a-zA-Z]|_)*\:";
        public static string asAttributeValue = @"(?<==)(.*)*";
        #endregion

        #region ActionScript methods
        public static string asMethodAccessor = @"public|private|internal";
        public static string asMethodStatic = @"[a-z]*(?=\sfunction)";
        public static string asMethodName = @"([a-z]*[A-Z]*)*(?=\()";
        public static string asMethodArgs = @"(?<=\().*(?=\,).*(?=\))|(?<=\().*(?=\))";
        public static string asMethodReturnType = @"(?=:\s)(.*)$";
        public static string asMethodMatchIfNotCtor = @"(?<=function)\s([a-zA-Z]*)(.*)(?:\s:)";
        public static string asIsMethod = @"\sfunction\s";
        public static string asSuperArgs = @"(?<=\().*(?=\,).*(?=\))";
        #endregion

        #region ActionScript instructions
        public static string asIsDeclaration = "var.*;";
        #endregion

        #region ActionScript classes
        public static string asClassAccessor = @"^([a-z]*)(?=\s)";
        public static string asClassType = @"(?=\s).*(?=\sclass)";
        public static string asClassName = @"(?<=class\s)([a-zA-Z]*)";
        public static string asClassImplements = @"(?<=implements\s)([a-zA-Z]*)";

        public static string asIsImport = "^import";
        public static bool ASIsAttribute(string line){
            return line.Contains("const") || line.Contains("var") ? true : false;
        }

        public static bool ASIsFunction(string line){
            Match m = Regex.Match(line, asIsMethod);
            return m.Success;
        }

        public static bool ASIsCtor(string line){
            Match m = Regex.Match(line,asMethodMatchIfNotCtor);
            return !m.Success;
        }

        public static bool ASIsPackage(string line){
            return line.Contains("package") ? true : false;
        }

        public static bool ASIsImport(string line)
        {
            Match m = Regex.Match(line, asIsImport);
            return m.Success;
        }
        #endregion

        #region ActionScript interfaces
        public static string asInterfaceName = @"(?<=interface\s)([a-zA-Z]*)";

        public static bool asIsInterface(string line)
        {
            return line.Contains("interface") && !ASIsPackage(line) && ! ASIsAttribute(line) && !ASIsImport(line) && !line.Contains(".");
        }
        #endregion
    }
}