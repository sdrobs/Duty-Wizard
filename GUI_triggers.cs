using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using mshtml;

namespace spreadAuto
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
            keyReceive kr = new keyReceive();
        }

        public class formControls
        {

            public void jsInjector()
            {
                string js = 
                "var tS = document.getElementById(\'sr\').innerText; " +
                "var sender = document.getElementById(\'vr\').innerText; " +
                "tS = tS.toLowerCase(); " +
                "sender = sender.toLowerCase(); " +
                "if(sender.indexOf('suglian') > -1 && tS.indexOf('duty') > -1){" +
                "var m = document.getElementById('txtBdy').innerText; " +
                "var url = m.substring(m.indexOf('<a href=\"')+9,m.length-1); " +
                "var url = url.substring(0,url.indexOf('\"')); " +
                "window.location = url;} else{document.getElementById('checkmessages').click()}; ";

                try
                {
                    if (Program.firstLoad)
                    {
                        Program.GUIref.browserViewBox.Navigate(
                            "javascript: " + js
                        );
                    }
                }
                catch { }
            }
        }

        public class keyReceive : Form
        {
            [DllImport("user32.dll")]
            public static extern bool RegisterHotKey(IntPtr hWnd,
                int id, int fsModifiers, int vlc);

            [DllImport("user32.dll")]
            public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            public keyReceive()
            {
                //UnregisterHotKey(Program.rstHandle, 2); -just documentation for unregistering in case needed later
                RegisterHotKey(this.Handle, 1, 1, (int)'Z');
                RegisterHotKey(this.Handle, 2, 1, (int)'X');
                RegisterHotKey(this.Handle, 3, 1, (int)'C');
                RegisterHotKey(this.Handle, 4, 1, (int)'V');
                RegisterHotKey(this.Handle, 5, 1, (int)'A');
                RegisterHotKey(this.Handle, 6, 1, (int)'Q');
                RegisterHotKey(this.Handle, 7, 1, (int)'S');
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 0x0312)
                {
                    int id = m.WParam.ToInt32();
                    webPull wp = new webPull();

                    if (id == 1)
                    {
                        wp.htmlPull();
                    }
                    if (id == 2)
                    {
                        wp.cursorLoc();
                    }
                    if (id == 3)
                    {
                        Program.GUIref.browserViewBox.Navigate("http://owa.mit.edu/");
                    }
                    if (id == 4)
                    {
                        Program.GUIref.browserViewBox.Navigate("http://www.google.com");
                    }
                    if (id == 5)
                    {
                        Program.running = true;
                        automation au = new automation();
                        au.sheetLoaded();
                    }

                    if (id == 7) 
                    {
                        Program.running = false;
                    }

                    if (id == 6)
                    {
                        automation au = new automation();

                        Program.sW = new System.Windows.Forms.Timer();
                        Program.sW.Interval = 10000;
                        Program.sW.Tick += new EventHandler(au.emailCheck);
                        Program.activated = true;
                        Program.sW.Start();
                    }
                    
                }

                base.WndProc(ref m);
            }

        }

        private void browserViewBox_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (Program.activated)
            {
                if (Program.firstLoad)
                {
                    WindowState = FormWindowState.Maximized;
                    this.TopMost = true;
                    Program.firstLoad = false;
                    Program.sW.Stop();
                    automation au = new automation();
                    Thread auT = new Thread(au.sheetLoaded);
                    auT.IsBackground = true;
                    auT.Start();
                }
            }
        }
    }
}
