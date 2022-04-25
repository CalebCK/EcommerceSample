using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Extensions
{
    public static class FileStreamLogger
    {
        public static void Log(string content)
        {
            string directory = $@"{Directory.GetCurrentDirectory()}\wwwroot\Logs\ProductResponses";
            string fileDirectory = $@"{directory}\Response-{DateTime.Now.ToString("yyyy-dd-M")}-{Guid.NewGuid().ToString()}.txt";
            var timeOfDay = DateTime.Now;
            try
            {

                //create directory
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                //create file

                using (StreamWriter fs = File.AppendText(fileDirectory))
                {
                    fs.WriteLine($"Time of Day--{timeOfDay.TimeOfDay}");
                    fs.WriteLine($"{content}");
                    fs.WriteLine("");
                    fs.WriteLine("====================================================");
                    fs.WriteLine("");

                }
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
