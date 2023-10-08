using System;
namespace as2cs_home.AsFile
{
	public static class ASTypesEnum
	{
		public const string BOOLEAN = "Boolean";
		public const string INT = "int";
		public const string NULL = "Null";
		public const string STRING = "String";
		public const string UINT = "uint";
        public const string NUMBER = "Number";
        public const string VOID = "Function";
		public const string ARRAY = "Array";
		public const string VECTOR = "Vector";
		public const string OBJECT = "Object";
		public const string DATE = "Date";
        public const string TIMER = "Timer";
		public const string TYPED = @"(?<=:.*<)((.*:.*)||(.*:*),(.*:.*))(?=>)";
    }
}

