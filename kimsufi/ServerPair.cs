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
        case "150sk10":
          m_strName = "KS-1";
          m_check = Program.MainForm.checkWantKS1;
          m_label = Program.MainForm.labelKS1;
          break;

        case "150sk20":
          m_strName = "KS-2";
          m_check = Program.MainForm.checkWantKS2;
          m_label = Program.MainForm.labelKS2;
          break;

        case "150sk22":
          m_strName = "KS-2 SSD";
          m_check = Program.MainForm.checkWantKS2SSD;
          m_label = Program.MainForm.labelKS2SSD;
          break;

        case "150sk30":
          m_strName = "KS-3";
          m_check = Program.MainForm.checkWantKS3;
          m_label = Program.MainForm.labelKS3;
          break;

        case "150sk40":
        case "150sk41": // are these essentially all the same?
        case "150sk42":
          m_strName = "KS-4";
          m_check = Program.MainForm.checkWantKS4;
          m_label = Program.MainForm.labelKS4;
          break;

        case "150sk50":
          m_strName = "KS-5";
          m_check = Program.MainForm.checkWantKS5;
          m_label = Program.MainForm.labelKS5;
          break;

        case "150sk60":
          m_strName = "KS-6";
          m_check = Program.MainForm.checkWantKS6;
          m_label = Program.MainForm.labelKS6;
          break;
      }

      m_bValid =
        m_strName != ""
        && m_label != null
        && m_check != null;
    }
  }
}
