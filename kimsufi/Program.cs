using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kimsufi
{
  public static class Program
  {
    public static FormMain MainForm = null;

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(MainForm = new FormMain());
    }
  }
}
