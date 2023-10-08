using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using as2cs_home.AsFile;
using as2cs_home.config;
namespace as2cs_home.Parser
{
    public class ASClassParser : IContext
    {
        private ILogger<ASClassParser> _logger;
        public IState state = null;
        private SemaphoreSlim _queueSem = new(1);
        public ConcurrentQueue<ASFile> _jobQueue = new();
        private int jobCount = 0;
        private int jobDone = 0;
        public string _currentLine {get; set;}
        public ASFile _currentFile {get; set;}

        public ICollection<ASFile> asFiles { get; set; }

        public int _iterator {get;set;}


        public ASClassParser(ILogger<ASClassParser> logger)
        {
            _logger = logger;
            _iterator = -1;
            asFiles = new List<ASFile>();
        }

        public void addJob(ASFile asFile){
            _queueSem.Wait();
            _logger.LogInformation("Adding file : " + asFile.getFullName());
            _jobQueue.Enqueue(asFile);
            jobCount++;
            _queueSem.Release();
        }

        public void startProcessing(){
            ASFile target;
            _queueSem.Wait();
            while (_jobQueue.TryDequeue(out target)){
                _logger.LogInformation("["+jobDone+"/"+jobCount+"]"+"Processing file : " + target.getFullName());
                parse(target).Wait();

            }
            _queueSem.Release();
        }

        private void setState(IState state){
            this.state = state;
            state.handle(this);
        }

        public void stopParsing(){
            this.state = new ASParserIdleState();
            _iterator = -1;

        }

        private Task parse(ASFile file){
            jobDone++;
            _currentFile = file;
            _iterator = -1;
            setState(new ASParserInitState());
            return Task.CompletedTask;
        }

        public Task parseNext(){
            _iterator++;
             if(_iterator >= _currentFile.asFile.Length){
                setState(new ASParserEOFState());
            } else {
                _currentLine = _currentFile.asFile[_iterator].TrimStart();
                if (GlobalConf.ASIsAttribute(_currentLine))
                {
                    setState(new ASParserAttributeState());
                }
                else if (GlobalConf.ASIsFunction(_currentLine))
                {
                    setState(new ASParserFunctionState());
                }
                else if (GlobalConf.ASIsPackage(_currentLine))
                {
                    setState(new ASParserPackageState());
                }
                else if (GlobalConf.asIsInterface(_currentLine))
                {
                    setState(new ASParserInterfaceState());
                }
                else if (_currentLine.Contains("class"))
                {
                    setState(new ASParserClassState());
                }
                else if (_currentLine.Contains("import "))
                {
                    setState(new ASParserImportState());
                }
                else
                {
                    //Whitlines,{}....
                    parseNext();
                }
                
            }
            return Task.CompletedTask;
            
        }
    }
}