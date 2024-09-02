using System.Globalization;
using System.Text;

namespace NoteCLI.UI;

public class Table
{
  public List<string> Headers { get; set; } = new List<string>();

  private readonly List<TableRow> Rows = [];
  public int RowCount { get; set; } = 0;
  public string Name { get; set; }


  public Table()
  {
    Name = "";
  }

  public bool Add(string value, int idx = -1)
  {
    var row = new TableRow();
    var tc = new TableCell();
    var id = new TableCell();
    id.Index = 0;
    id.Value = RowCount++.ToString();
    tc.Value = value;
    tc.Index = idx;
    row.Values.Add(id);
    row.Values.Add(tc);
    Rows.Add(row);
    return true;
  }

  public bool Add(TableRow tr)
  {
    Rows.Add(tr);
    return true;
  }

  public override string ToString()
  {
    var result = new StringBuilder();
    for (var i = 0; i < Headers.Count; i++)
    {
      result.Append(Headers[i]);
      if (i < Headers.Count - 1)
        result.Append(',');
    }

    if (Headers.Count > 0)
    {
      result.Append('\n');
    }


    for (var i = 0; i < Rows.Count; i++)
    {
      for (var j = 0; j < Rows[i].Values.Count; j++)
      {
        result.Append(Rows[i].Values[j].Value);
        if (j < Rows[i].Values.Count - 1)
          result.Append(',');
      }
      result.Append('\n');
    }

    return result.ToString();
  }

  internal bool SaveToFile(string path, string data)
  {
    var sb = new StringBuilder();
    sb.Append(data.ToString());
    File.WriteAllText(path, data);
    return true;
  }

  public bool FromFile(string path)
  {
    if (File.Exists(path))
    {
      var contents = File.ReadAllLines(path);
      for (int i = 0; i < contents.Length; i++)
      {
        if (i == 0)
        {
          if (contents[i].StartsWith('|'))
          {
            var _headers = contents[i].Split(',');
            foreach (var _header in _headers)
            {
              Headers.Add(_header);
            }
          }
        }
        var vals = contents[i].Split(',');
        TableRow tr = new();
        foreach (var val in vals)
        {
          var tc = new TableCell();
          tc.Index = i;
          tc.Value = val;
          tr.Values.Add(tc);
        }
        this.Add(tr);
        if (i > 0)
          RowCount++;
      }
    }
    return true;
  }

  public void Print()
  {
    Console.Clear();
    Console.Write(this.ToString());
  }
}

public class TableRow
{
  public int RowLength { get; set; }
  public List<TableCell> Values { get; set; } = new();

}

public class TableCell
{
  public int Index = -1;
  public string? Value = null;
}

public class ScreenElement
{
  public int ZIndex = 0;
  public string Value = string.Empty;
  public Vec2i Position = Vec2i.ZERO;
}