using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kimsufi
{
  public class ServerPair
  {
    public bool m_bValid = false;

    public string m_strName = "";
    public CheckBox m_check = null;
    public Label m_label = null;

    public bool m_bAvailable = false;

    public ServerPair(string strReference)
    {
      switch (strReference) {
        case "160sk1":
          m_strName = "KS-1";
          m_check = Program.MainForm.checkWantKS1;
          m_label = Program.MainForm.labelKS1;
          break;

        case "160sk2":
          m_strName = "KS-2A";
          m_check = Program.MainForm.checkWantKS2A;
          m_label = Program.MainForm.labelKS2A;
          break;

        case "161sk2":
          m_strName = "KS-2E";
          m_check = Program.MainForm.checkWantKS2E;
          m_label = Program.MainForm.labelKS2E;
          break;

        case "160sk3":
          m_strName = "KS-3A";
          m_check = Program.MainForm.checkWantKS3A;
          m_label = Program.MainForm.labelKS3A;
          break;

        case "160sk31":
          m_strName = "KS-3B";
          m_check = Program.MainForm.checkWantKS3B;
          m_label = Program.MainForm.labelKS3B;
          break;

        case "160sk4":
          m_strName = "KS-4A";
          m_check = Program.MainForm.checkWantKS4A;
          m_label = Program.MainForm.labelKS4A;
          break;

        case "160sk5":
          m_strName = "KS-5";
          m_check = Program.MainForm.checkWantKS5;
          m_label = Program.MainForm.labelKS5;
          break;
      }

      m_bValid =
        m_strName != ""
        && m_label != null
        && m_check != null;
    }
  }
}
