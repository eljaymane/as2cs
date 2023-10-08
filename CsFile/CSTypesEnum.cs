using System;
namespace as2cs_home.CsFile
{
	public static class CSTypesEnum
	{
        public static string BOOLEAN = "bool";
        public static string INT = "int";
        public static string NULL = "null";
        public static string STRING = "string";
        public static string UINT = "uint";
        public static string VOID = "void";
        public static string LIST = "List";
        public static string OBJECT = "Object";
        public static string DATE = "DateTime";
        public static string DOUBLE = "double";
        public static string TIMER = "Timer";
        public static string TYPED = @"(?<=:.*<)((.*:.*)||(.*:*),(.*:.*))(?=>)";
    }
}

