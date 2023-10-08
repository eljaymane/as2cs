using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as2cs_home.Abstractions
{
    public abstract class AbstractAttribute : IWritable
    {
        public string _accessor{ get; set; }
        public string _static{ get; set; }
        public string _const { get; set; }
        public string _final{ get; set; }
        public string _type{ get; set; }
        public string _name{ get; set; }
        public string _value{ get; set; }

        public AbstractAttribute()
        {

        }

        public AbstractAttribute(string accessor,string _static,string _const,string final,string type,string _ame,string value)
        {
            this._accessor = accessor;
            this._static = _static == null ? "" : _static;
            this._const = _const == null ? "" : _const;
            this._final = final == null ? "" : final;
            this._type = type;
            this._name = _name;
            this._value = value== null? "" : value;
        }

        public Task toLines(ref int index, ref string[] lines)
        {
            lines[index] = $"{_accessor} {_final} {_static} {_const} {_type} {_name}";
            if (_value != null && _value != "") lines[index] += $"= {_value}";
            lines[index] += ";\n";
            index++;
            return Task.CompletedTask;
        }

        public abstract string getAccessor(string line);
        public abstract string getStatic(string line);
        public abstract string getFinal(string line);

        public abstract string getConst(string line);

        public abstract string getType(string line);

        public abstract string getName(string line);

        public abstract string getValue(string line);
    }
}