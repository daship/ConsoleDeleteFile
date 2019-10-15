using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDeleteFile.Model
{
    public class DeletePatternModel
    {
        public string Path { set; get; }

        public string DeleteFilePatern { set; get; }

        public bool IsRecursive { set; get; }

        public TimeSpan KeepTimeSpan { set; get; }
    }
}
