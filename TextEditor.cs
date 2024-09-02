using System.Text;

namespace NoteLi;

public class TextEditor
{
  private string currentFilePath;
  private string textContent;
  public TextEditor()
  {
    currentFilePath = string.Empty;
    textContent = string.Empty;
  }

  public void NewFile(string? filePath = null)
  {
    if (filePath == null)
    {
      currentFilePath = string.Empty;
    }
    else
      currentFilePath = filePath;
    textContent = string.Empty;
  }

  public void SaveFile(string filePath)
  {
    try
    {
      if (File.Exists(filePath))
      {
        Console.WriteLine(":: File already exists");
      }
      File.WriteAllText(filePath, textContent);
      currentFilePath = filePath;
      Console.WriteLine($"File saved to: {filePath}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error saving file: {ex.Message}");
    }
  }

}