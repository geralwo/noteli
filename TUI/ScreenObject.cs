namespace NoteLi.TUI;

public abstract class ScreenObject
{
  public string Name { get; set; }
  public virtual string Value { get; set; }

  public virtual List<string> Lines
  {
    get
    {
      var res = new List<string>();
      var s = Value.Split('\n');
      foreach (var line in s)
      {
        res.Add(line);
      }
      return res;
    }
  }

  public int ZIndex { get; set; } = 0;

  public ConsoleColor ForegroundColor = ConsoleColor.White;
  public ConsoleColor BackgroundColor = ConsoleColor.Black;

  public Vec2i Position { get; set; }

  public List<ScreenObject> Children = new List<ScreenObject>();
  public ScreenObject()
  {
    Renderer.Add(this);
    Name = string.Empty;
    Value = string.Empty;
  }

  public virtual void Render()
  {
    Console.SetCursorPosition(this.Position.x, this.Position.y);
    Console.ForegroundColor = this.ForegroundColor;
    Console.BackgroundColor = this.BackgroundColor;
    Console.Write(this.Value);
    Console.ResetColor();
  }
}