using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using Microsoft.Extensions.Logging;
using as2cs_home.config;
using as2cs_home.AsFile;
using as2cs_home.Parser;
using System.Collections.Concurrent;
using as2cs_home.CsFile;

namespace as2cs_home.Reader
{
    public class ASConverter
    {
        private ILoggerFactory _loggerFactory;
        private ASClassParser asParser;
        private ConcurrentQueue<ASFile> jobQueue;
        private int jobCount=0;
        private int jobDone=0;

        private string source;
        private string destination;

        public ASConverter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            jobQueue = new ConcurrentQueue<ASFile>();
        }

        public void setSource(string source){
            this.source = source;
        }

        public void setDestination(string destination){
            this.destination = destination;
        }

        public async void startReading(){
            asParser = new(_loggerFactory.CreateLogger<ASClassParser>());
            readRecursive(source).Wait();
            asParser.startProcessing();
            foreach (var file in asParser.asFiles)
            {
                jobQueue.Enqueue(file);
                jobCount++;
            }
        }

        public Task startConverting()
        {
            while(jobQueue.TryDequeue(out var asFile)){
                var csFile = new CSFile(asFile);
                var targetPath = csFile.filePath.Replace("scripts", "scripts_cs").Replace(".as", ".cs");
                _loggerFactory.CreateLogger<ASConverter>().LogInformation($"[{jobDone}/{jobCount}] Writing converted CS file to : " +targetPath);
                csFile.WriteToDisk(targetPath).Wait();
                jobDone++;
            }
            return Task.CompletedTask;
        }

        public Task readRecursive(string src){
            FileAttributes a = File.GetAttributes(src);
            if (a.HasFlag(FileAttributes.Directory)) {
                var dirSrc = new DirectoryInfo(src);

                var files = dirSrc.GetFiles("*.as");
                foreach (var file in files)
                {
                    asParser.addJob(new ASFile(file.FullName));
                }

                foreach (var subdir in dirSrc.GetDirectories())
                {
                    var dirDest = new DirectoryInfo(subdir.FullName.Replace("scripts","scripts_cs"));
                    var dest = new DirectoryInfo(dirDest.FullName);
                    if(!dest.Exists){
                        dest.Create();
                    }
                    readRecursive(dirSrc.FullName + "/" + subdir.Name).Wait();
                    
                }
                
            }
            return Task.CompletedTask;
        }

        public int getJobCount(){
            return asParser._jobQueue.Count;
        }

       


    }
}