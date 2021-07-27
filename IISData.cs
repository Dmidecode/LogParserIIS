using System;
using System.Collections.Generic;
using System.Text;

namespace LogParserIIS
{
  public class IISData
  {
    public string Date { get; set; }
    
    public string Heure { get; set; }
    
    public string Verbe { get; set; }

    public string Service { get; set; }

    public string Query { get; set; }

    public string Reponse { get; set; }

    public int TimeSpan { get; set; }

    public override string ToString()
    {
      StringBuilder res = new StringBuilder();

      res.Append($"[Date:{this.Date} {this.Heure} - Verbe:{this.Verbe} - Service: {this.Service}");
      if (this.Query != "-")
        res.Append($" - Query: {this.Query}");
      res.Append($"- Reponse:{this.Reponse} - Temps: {this.TimeSpan}ms]");

      return res.ToString();
    }

    public string ToCsv()
    {
      return $"{this.Date};{this.Heure};{this.Verbe};{this.Service};{this.Query};{this.Reponse};{this.TimeSpan}";
    }
  }
}
