using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogParserIIS
{
  class Program
  {
    private const string DefaultFile = "logIIS.log";
    public static int HEADERSKIP = 4;
    static void Main(string[] args)
    {
      Options options = new Options();
      Parser.Default.ParseArguments<Options>(args)
        .WithParsed<Options>(o =>
      {
        Run(o);
      }); ;

    }

    public static void Run(Options options)
    {
      string file = DefaultFile;
      if (!string.IsNullOrEmpty(options.File))
        file = options.File;

      string[] allFile = File.ReadAllLines(file);
      List<IISData> data = new List<IISData>();
      int i = 0;
      foreach (string s in allFile)
      {
        if (i < HEADERSKIP)
        {
          i += 1;
          continue;
        }

        string[] splitData = s.Split(' ');

        IISData d = new IISData();
        d.Date = splitData[0];
        d.Heure = splitData[1];
        d.Verbe = splitData[3];
        d.Service = splitData[4];
        d.Query = splitData[5]; // '-' = rien
        d.Reponse = splitData[11];
        d.TimeSpan = int.Parse(splitData[14]);

        bool check = false;
        if (options.In != null && options.In.Any())
        {
          check = options.In.Split(',').Any(x => d.Service.Contains(x));
        }
        else if (options.NotIn != null && options.NotIn.Any())
        {
          check = !options.NotIn.Split(',').Any(x => d.Service.Contains(x));
        }
        else
          check = true;

        if (check && d.TimeSpan > options.Time)
          data.Add(d);
      }

      if (options.Verbose)
      {
        foreach (var d in data)
        {
          Console.WriteLine(d.ToString());
        }
      }

      if (options.Export)
      {
        StringBuilder csv = new StringBuilder();
        csv.AppendLine("Date;Heure;Verbe;Service;Query;Reponse;ms");
        foreach (var d in data)
        {
          csv.AppendLine(d.ToCsv());
        }

        File.WriteAllText("export.csv", csv.ToString());
      }
    }
  }
}
