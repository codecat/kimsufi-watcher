using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Nimble.JSON;
using System.Diagnostics;

/* {
 *   answer: {
 *     availability: [
 *       {
 *         reference: "150sk20", (sk22 is KS-2 SSD)
 *         metaZones: [
 *           {
 *             zone: "fr",
 *             availability: "unknown" (or "unavailable", "1H-high", "1H-low", or anything else = available)
 *           }
 *         ]
 *       }
 *     ]
 *   }
 * }
 */

namespace kimsufi
{
  public partial class FormMain : Form
  {
    Thread m_pThread = null;
    bool m_bContinueChecking = true;

    public FormMain()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      m_pThread = new Thread(new ThreadStart(delegate
      {
        Random rnd = new Random();
        while (m_bContinueChecking) {
          WebClient wc = new WebClient();
          wc.Proxy = null;
          try {
            string strContent = wc.DownloadString("https://ws.ovh.com/dedicated/r2/ws.dispatcher/getAvailability2");

            dynamic obj = Json.JsonDecode(strContent);
            dynamic servers = obj["answer"]["availability"];

            int ctFoundServers = 0;

            List<ServerPair> serverPairs = new List<ServerPair>();

            for (int i = 0; i < servers.Count; i++) {
              dynamic server = servers[i];

              ServerPair pair = new ServerPair(server["reference"]);
              if (!pair.m_bValid) {
                continue;
              }

              dynamic zones = server["metaZones"];
              for (int j = 0; j < zones.Count; j++) {
                dynamic zone = zones[j];
                string strAvail = zone["availability"];
                if (strAvail != "unknown" && strAvail != "unavailable") {
                  pair.m_bAvailable = true;
                  ctFoundServers++;
                }
              }

              ServerPair existingPair = serverPairs.Find((ServerPair other) => other.m_strName == pair.m_strName);
              if (existingPair != null) {
                if (pair.m_bAvailable) {
                  existingPair.m_bAvailable = true;
                }
                continue;
              }

              serverPairs.Add(pair);
            }

            if (ctFoundServers > 0) {
              SetStatus(ctFoundServers + " server(s) available");
            } else {
              SetStatus("No servers");
            }

            this.Invoke(new Action(delegate
            {
              foreach (ServerPair pair in serverPairs) {
                if (pair.m_bAvailable) {
                  pair.m_label.Text = "Available";
                } else {
                  pair.m_label.Text = "Unavailable";
                }
                Flash(pair.m_label, pair.m_check);
              }
            }));
          } catch {
            SetStatus("Request failed!");
          }

          Thread.Sleep(5000 + rnd.Next(-500, 500));
        }
      }));
      m_pThread.Start();
    }

    private void timerFlash_Tick(object sender, EventArgs e)
    {
      this.BackColor = Color.Black;
      Flash(labelKS1, checkWantKS1);
      Flash(labelKS2, checkWantKS2);
      Flash(labelKS2SSD, checkWantKS2SSD);
      Flash(labelKS3, checkWantKS3);
      Flash(labelKS4, checkWantKS4);
      Flash(labelKS5, checkWantKS5);
      Flash(labelKS6, checkWantKS6);
    }

    void Flash(Label label, CheckBox check)
    {
      if (label.Text == "Available") {
        if (label.BackColor == Color.Green) {
          label.BackColor = Color.Red;
        } else {
          label.BackColor = Color.Green;
        }
        if (check.Checked) {
          this.BackColor = label.BackColor;
        }
      } else {
        label.BackColor = Color.Transparent;
      }
    }

    void SetStatus(string str)
    {
      this.Invoke(new Action(delegate
      {
        labelStatus.Text = str + DateTime.Now.ToString(" [HH:mm:ss]");
      }));
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      m_bContinueChecking = false;
    }

    void OpenWebsite()
    {
      Process.Start("http://www.kimsufi.com/nl/");
    }

    private void labelKS6_Click(object sender, EventArgs e) { OpenWebsite(); }
    private void labelKS5_Click(object sender, EventArgs e) { OpenWebsite(); }
    private void labelKS4_Click(object sender, EventArgs e) { OpenWebsite(); }
    private void labelKS3_Click(object sender, EventArgs e) { OpenWebsite(); }
    private void labelKS2SSD_Click(object sender, EventArgs e) { OpenWebsite(); }
    private void labelKS2_Click(object sender, EventArgs e) { OpenWebsite(); }
    private void labelKS1_Click(object sender, EventArgs e) { OpenWebsite(); }
  }
}
