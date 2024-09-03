using System;
using System.Threading;
using System.Timers;
using NoteLi.TUI;
namespace NoteLi
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Renderer.Init();
      var table = Table.FromFile("tmp.notedb");
      table.ShowHeaders = true;
      while (true)
      {
        Renderer.Render();
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        Console.Write("> ");
        var input = Console.ReadLine();
        if (input != null)
        {
          if (input.StartsWith("exit"))
          {
            table.SaveFile();
            Console.ResetColor();
            Console.Clear();
            Environment.Exit(0);
          }
          else if (input.StartsWith("save:"))
          {
            table.SaveFile(input.Split(':')[1]);
            continue;
          }
          else if (input.StartsWith("open:"))
          {
            // open file to import as a table
            var newTable = Table.FromFile(input.Split(':')[1]);
            table = newTable;
            continue;
          }
          else if (input.StartsWith("new:"))
          {
            table.SaveFile(input.Split(':')[1]);
            continue;
          }
          else if (input.StartsWith("change:"))
          {
            var change_req = input.Substring("change:".Length).Split(':');
            var type = change_req[0];
            if (type == "header")
            {
              var id = change_req[1];
              var val = change_req[2];
              // header:0:<name>
              if (int.TryParse(id, out var _id))
                Table.ChangeHeader(table, _id, val);
            }
            else if (type == "row")
            {
              // row:0:id:1337
              // row:id=1337:0
              var id = change_req[1];
              var key = change_req[2];
              var new_val = change_req[3];
              if (int.TryParse(id, out var _idx))
                table.ChangeRow(_idx, key, new_val);
            }
            continue;
          }
          else if (input.Length > 0)
          {
            var _row = new Table.Row();
            long unixTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            _row.Values.Add("id", "%id%");
            _row.Values.Add("updated", unixTime.ToString());
            _row.Values.Add("content", input);
            table.Add(_row);
          }
        }
      }

    }
  }
}