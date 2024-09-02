namespace NoteCLI.TUI;

public class MenuBar : ScreenObject
{
  public string[] MenuTitles { get; set; }
  public MenuBar(string[] menuTitles)
  {
    MenuTitles = menuTitles;
  }
}