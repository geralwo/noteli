namespace NoteCLI.TUI;

public class Renderer()
{
  public static Renderer? Instance;
  public List<ScreenObject> SObjects = new List<ScreenObject>();

  public static void Init()
  {
    if (Instance == null)
    {
      Console.Write(":: Init Renderer");
      Instance = new Renderer();
    }
  }

  public static void Add(ScreenObject obj)
  {
    if (Instance == null)
      throw new Exception($"[!] Renderer was not inited when trying to add {obj}");
    Instance.SObjects.Add(obj);
  }



  public static void Remove(ScreenObject obj)
  {
    if (Instance == null)
      throw new Exception($"[!] Renderer was not inited when trying to remove {obj}");
    Instance.SObjects.Remove(obj);
  }

  public static void Render()
  {
    Console.Clear();
    if (Instance == null)
      throw new Exception("[!] Renderer was not inited when trying to call this.Render().");
    foreach (ScreenObject obj in Instance.SObjects)
    {
      obj.Render();
    }
  }
}