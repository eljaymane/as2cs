using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using as2cs_home.Abstractions;
using as2cs_home.config;
namespace as2cs_home.AsFile
{
    public class ASClass : AbstractClass
    {

        public ASClass() : base()
        {

        }

        public ASClass(string classDeclaration) : base()
        {
            this._classAccessor = getClassAccessor(classDeclaration);
            this._classType = getClassType(classDeclaration);
            this._className = getClassName(classDeclaration);
            this._implements = getClassImplements(classDeclaration);
        }

        public override void initialize(string classDeclaration)
        {
            this._classAccessor = getClassAccessor(classDeclaration);
            this._classType = getClassType(classDeclaration);
            this._className = getClassName(classDeclaration);
            this._implements = getClassImplements(classDeclaration);
        }

        public override void addMethod(AbstractMethod method){
            this._methods.Add(method);
        }

        public override void setConstructor(AbstractMethod method){
            this._constructor = method;
        }

        public override string getClassAccessor(string declarationLine){
            Match m = Regex.Match(declarationLine,GlobalConf.asClassAccessor);
            return m.Value.Trim() ;
        }

        public override string getClassType(string declarationLine){
            Match m = Regex.Match(declarationLine,GlobalConf.asClassType);
            return m.Value.Trim();
        }

        public override string getClassName(string declarationLine){
            Match m = Regex.Match(declarationLine,GlobalConf.asClassName);
            return m.Value.Trim();
        }

        public override string getClassImplements(string declarationLine){
            Match m = Regex.Match(declarationLine,GlobalConf.asClassImplements);
            return m.Value.Trim();
        }

    }
}