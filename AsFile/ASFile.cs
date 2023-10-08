using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using as2cs_home.config;
using as2cs_home.Abstractions;

namespace as2cs_home.AsFile
{
    public class ASFile : AbstractFile
    {
        public ASFile(string asFilePath) : base(asFilePath,"",new List<string>())
        {
            this._class = new ASClass();
        }

        public override void setPackageName(string name){
            this._packageName = name;
        }

        public override void setConstructor(AbstractMethod method){
            this._class.setConstructor(method);
        }
        public override void addImport(string importLine){
            this._imports.Add(importLine);
        }
        public override void addMethod(AbstractMethod method){
            this._class._methods.Add(method);
        }
        public override string getFullName(){
            return Regex.Replace(filePath,GlobalConf.fileName_Regex,"");
        }

        public override string getSimpleName(){
            return getFullName().Split(".")[0];
        }

        public override string getExtension(){
            return getFullName().Split(".")[1];
        }

        
    }
}