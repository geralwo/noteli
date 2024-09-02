using System.Text;

namespace NoteLi;

public class FileManager
{
  public static string[] ReadAllLines(string path)
  {
    if (path == "tmp.notedb")
      return ["|id,title"];
    if (!File.Exists(path))
    {
      var f = File.Create(path);
      f.Close();
      File.WriteAllText(path, "|id,title");
      return ["|id,title"];
    }
    return File.ReadAllLines(path);
  }

  public static void WriteAllLines(string path, string data)
  {
    File.WriteAllText(path, data);
  }
}