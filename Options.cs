using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogParserIIS
{
  class Options
  {
    [Option('i', "in", HelpText = "La liste des services que l'on veut récupérer")]
    public string In { get; set; }

    [Option('n', "notin", HelpText = "La liste des noms des appels que l'on ne veut pas récupérer")]
    public string NotIn { get; set; }

    [Option('f', "file", HelpText = "Le fichier à prendre en entrée")]
    public string File { get; set; }

    [Option('e', "export", HelpText = "Export dans un fichier csv les résultats demandés")]
    public bool Export { get; set; }

    [Option('v', "verbose", HelpText = "Affiche sur la console les résultats demandés")]
    public bool Verbose { get; set; }

    [Option('t', "time", HelpText = "Filtre que les appels supérieurs à 't' ms")]
    public int Time { get; set; }
  }
}
