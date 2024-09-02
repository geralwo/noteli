namespace NoteCLI.TUI;

public class Text : ScreenObject
{
  public Vec2i Padding = Vec2i.ZERO;
  public Text(string text = "")
  {
    Value = text;
  }
}