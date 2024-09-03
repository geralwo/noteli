using NoteLi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NoteLi.TUI;

public class Table : ScreenObject
{
  public bool ShowHeaders = false;
  public Vec2i Size = Vec2i.ZERO;
  public List<string> Headers = [];
  public List<Row> Rows = new();

  public string FilePath;

  public static Table FromFile(string path)
  {

    var table = new Table(path);
    table.fromFile(path);
    return table;
  }

  public static void ChangeHeader(Table t, int index, string changeValue)
  {
    foreach (var row in t.Rows)
    {
      row.Values.Add(changeValue, row.Values[t.Headers[index]]);
      row.Values.Remove(t.Headers[index]);
    }
    t.Headers[index] = changeValue;
  }

  public Table(string filePath = "tmp.notedb")
  {
    FilePath = filePath;
  }

  public override string Value => this.ToString();

  public void Add(Row row)
  {
    Rows.Add(row);
  }

  public override string ToString()
  {
    var sb = new StringBuilder();
    if (Headers.Count > 0)
    {
      sb.Append('|');
      for (int i = 0; i < Headers.Count; i++)
      {
        sb.Append(Headers[i]);
        if (i < Headers.Count - 1)
        {
          sb.Append(',');
        }
      }
      sb.Append('\n');
    }
    foreach (var row in Rows)
    {
      foreach (var col in row.Values)
      {
        sb.Append(col.ToString());
      }
      sb.Append('\n');
    }
    return sb.ToString();
  }

  private void fromFile(string path)
  {
    var _rows = FileManager.ReadAllLines(path);
    if (_rows.Length == 0)
      throw new Exception(":: FileManager returned no lines");
    if (_rows[0].StartsWith('|'))
    {
      Headers = _rows[0].Trim().Substring(1).Split(',').ToList();
      for (int i = 1; i < _rows.Length; i++)
      {
        var row_values = _rows[i].Split(',');
        var tr = new Table.Row();
        for (int j = 0; j < row_values.Length; j++)
        {
          if (j < Headers.Count)
          {
            tr.Values.Add(Headers[j], row_values[j]);
          }
        }
        this.Add(tr);
      }
    }
    else
    {
      throw new Exception($":: The loaded file contained no headers. Error.\n\nRowCount:{_rows.Length}");
    }
  }

  public void SaveFile(string path = "tmp.notedb")
  {
    FilePath = path;
    if (FilePath == "tmp.notedb")
      return;
    var sb = new StringBuilder();
    if (Headers.Count > 0)
    {
      // add header line with prefix
      sb.Append('|');
      for (int i = 0; i < Headers.Count; i++)
      {
        sb.Append(Headers[i]);
        if (i < Headers.Count - 1)
        {
          sb.Append(',');
        }
      }
      sb.Append('\n');
    }
    for (int i = 0; i < Rows.Count; i++)
    {
      foreach (var _value in Rows[i].Values.Values)
      {
        sb.Append(_value);
        sb.Append(',');
      }
      sb.Append('\n');
    }
    if (path == null)
      FileManager.WriteAllLines(this.FilePath, sb.ToString());
    else
    {
      FilePath = path;
      FileManager.WriteAllLines(path, sb.ToString());
    }
  }

  public override void Render()
  {
    var row_index = 0;
    List<int> rowWidths = new();
    if (ShowHeaders)
    {
      Console.SetCursorPosition(this.Position.x, this.Position.y);
      Console.ForegroundColor = ConsoleColor.Black;
      Console.BackgroundColor = ConsoleColor.White;
      foreach (var h in Headers)
      {
        var render_value = h + '\t';
        rowWidths.Add(render_value.Length);
        Console.Write(render_value);
      }
    }
    foreach (var row in Rows)
    {
      Console.SetCursorPosition(this.Position.x, this.Position.y + row_index + 1);
      Console.ForegroundColor = this.ForegroundColor;
      Console.BackgroundColor = this.BackgroundColor;
      foreach (var val in row.Values)
      {
        Console.Write($"{val.Value}\t");
        Console.ResetColor();
      }
      row_index++;
    }
  }

  public void ChangeRow(int idx, string column_name, string value)
  {
    Rows[idx].Values[column_name] = value;
  }
  public class Row : ScreenObject
  {
    public bool IsHeader = false;
    public Dictionary<string, string> Values = new();
    public Row()
    {
    }
  }

  public struct RowValue
  {
    public int RowOffset;
    public string Value;

  }
}

