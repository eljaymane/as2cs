using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as2cs_home.Abstractions
{
    public abstract class AbstractClass : IWritable
    {
        public string _classAccessor { get; set;}
        public string _classType { get; set; }
        public string _className { get; set; }

        public string _implements { get; set;} 

        public IList<AbstractAttribute> _attributes { get; set; }

        public AbstractMethod _constructor { get; set; }

        public IList<AbstractMethod> _methods {get; set;}

        public AbstractClass()
        {
            this._attributes = new List<AbstractAttribute>();
            this._methods = new List<AbstractMethod>();
        }

        public AbstractClass(string accessor, string classType, string className,string implements)
        {
            this._classAccessor = accessor;
            this._classType = classType;
            this._className = className;
            this._implements = implements;
            this._attributes = new List<AbstractAttribute>();
            this._methods = new List<AbstractMethod>();
        }

        public AbstractClass(string accessor, string classType, string className,string implements, IList<AbstractAttribute> attributes, AbstractMethod constructor, IList<AbstractMethod> methods)
        {
            this._classAccessor = accessor;
            this._classType = classType;
            this._className = className;
            this._implements = implements;
            this._attributes = attributes;
            this._constructor = constructor;
            this._methods = methods;
        }

        public abstract void initialize(string classDeclaration);
        public void addAttribute(AbstractAttribute attribute)
        {
            this._attributes.Add(attribute);
        }
        public abstract void addMethod(AbstractMethod method);
        public abstract void setConstructor(AbstractMethod method);
        public abstract string getClassAccessor(string declarationLine);
        public abstract string getClassType(string declarationLine);
        public abstract string getClassName(string declarationLine);
        public abstract string getClassImplements(string declarationLine);


        public Task toLines(ref int index, ref string[] lines)
        {
            writeClassHeader(ref index, ref lines).Wait();
            lines[index] += "{\n";
            index++;
            writeClassAttributes(ref index, ref lines);
            if(_constructor != null)_constructor.toLines(ref index, ref lines);
            if (_methods.Count > 0)
            {
                foreach (var method in _methods)
                    method.toLines(ref index, ref lines);
            }
            lines[index] = "}\n";
            index++;
            lines[index] = "}\n";
            return Task.CompletedTask;
        }

        private Task writeClassAttributes(ref int index, ref string[] lines)
        {
            foreach(var attribute in _attributes)
            {
                attribute.toLines(ref index, ref lines).Wait();
            }
            return Task.CompletedTask;
        }

        public virtual Task writeClassHeader(ref int index, ref string[] lines)
        {
            lines[index] = $"{_classAccessor} {_classType} class {_className} {_implements}\n";
            index++;
            return Task.CompletedTask;
        }
    }

    
}