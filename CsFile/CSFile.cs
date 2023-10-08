using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using as2cs_home.Abstractions;

namespace as2cs_home.CsFile
{
    public class CSFile : AbstractFile
    {


        public CSFile(ASFile aSFile) : base(aSFile.filePath,aSFile._packageName,aSFile._imports)
        {
            this._packageName = _packageName.Replace("import", "using");
            this._imports = convertImports(aSFile._imports);
             this._class = aSFile._class.GetType() == typeof(ASInterface) ? new CSInterface(aSFile._class): new CsClass(aSFile._class);
        }

        private IList<string> convertImports(IList<string> imports)
        {
            var importList = new List<string>();
            foreach (var import in imports)
            {
                importList.Add(import.Replace("import", "using").Replace(".*",""));
            }
            return importList;
        }

        public override void addImport(string importLine)
        {
            throw new NotImplementedException();
        }

        public override void addMethod(AbstractMethod method)
        {
            throw new NotImplementedException();
        }

        public override string getExtension()
        {
            throw new NotImplementedException();
        }

        public override string getFullName()
        {
            throw new NotImplementedException();
        }

        public override string getSimpleName()
        {
            throw new NotImplementedException();
        }

        public override void setConstructor(AbstractMethod method)
        {
            throw new NotImplementedException();
        }

        public override void setPackageName(string name)
        {
            throw new NotImplementedException();
        }

      

        public Task WriteToDisk(string destination)
        {
            var data = toLines().Result;
            File.WriteAllLines(destination, data);
            return Task.CompletedTask;
        }
    }
}