using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace as2cs_home.Abstractions
{
    public abstract class AbstractFile : IWritable
    {
        public string filePath { get; set; }
        public string _packageName { get; set; }
        public IList<string> _imports { get; set; }

        public AbstractClass _class { get; set; }
        public string[] asFile { get; set; }

        public AbstractFile(string filePath, string _packageName,IList<string> imports)
        {
            this.filePath = filePath;
            this._packageName = _packageName;
            this._imports = imports;
            this.asFile = File.ReadAllLines(filePath);
        }

        public abstract void setPackageName(string name);
        public abstract void setConstructor(AbstractMethod method);
        public abstract void addImport(string importLine);
        public void addAttribute(AbstractAttribute attribute)
        {
            this._class.addAttribute(attribute);
        }
        public abstract void addMethod(AbstractMethod method);
        public abstract string getFullName();
        public abstract string getSimpleName();
        public abstract string getExtension();

        public Task<string[]> toLines()
        {
            var lines = new string[4000];
            var index = 0;
            if (_imports.Count > 0) writeImports(ref index, lines);
            lines[index] = $"namespace {_packageName}\n" + "{\n";
            index++;
            this._class.toLines(ref index, ref lines).Wait();
            return Task.FromResult(lines);
        }

        private Task writeImports(ref int index, string[] lines)
        {
            foreach (var import in _imports)
            {
                lines[index] = $"{import}\n";
                index++;
            }

            return Task.CompletedTask;
        }
    }
}