using System;
using System.IO;
using Scriban;


namespace EmailGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            Console.WriteLine("Specify recipient email:");
            string recipient = Console.ReadLine();

            Console.WriteLine("Specify email body: ");
            string body = Console.ReadLine();

            var template = Template.Parse(
                @"<!DOCTYPE html>
<html>
<head>
<title>Generated Email</title>
</head>
<body>
<h1>Hello {{receiver}}!</h1>
<p>{{emailbody}}</p>
<p />
<p>Regards, Console.</p>
</body>");
            var result = template.Render(new { receiver = recipient, emailbody = body });

            string path = string.Format(directory + "{0}.html", recipient);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            if(File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, result.ToString());

            Console.WriteLine("Finished saving file. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
