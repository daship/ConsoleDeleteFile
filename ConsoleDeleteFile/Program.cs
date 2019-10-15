using ConsoleDeleteFile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleDeleteFile
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var json = Properties.Settings.Default.DeletePatterns;
                var inputModel = JsonConvert.DeserializeObject<InputModel>(json);

                foreach (var deleteModel in inputModel.DeleteModels)
                    DeleteFile(deleteModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static void DeleteFile(DeletePatternModel inputModel)
        {
            var pattern = "*";
            if (!string.IsNullOrEmpty(inputModel.DeleteFilePatern))
                pattern = inputModel.DeleteFilePatern;


            var files = Directory.GetFiles(inputModel.Path, pattern);

            //delete files first
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                var duration = DateTime.Now - fileInfo.LastWriteTime;
                if (duration.TotalSeconds >= inputModel.KeepTimeSpan.TotalSeconds)
                    File.Delete(file);
            }

            if (!inputModel.IsRecursive)
                return;

            var dirs = Directory.GetDirectories(inputModel.Path);
            foreach (var dir in dirs)
            {
                DeleteFile(new DeletePatternModel
                {
                    IsRecursive = inputModel.IsRecursive,
                    DeleteFilePatern = inputModel.DeleteFilePatern,
                    Path = dir,
                    KeepTimeSpan = inputModel.KeepTimeSpan
                });
            }
        }

    }
}
