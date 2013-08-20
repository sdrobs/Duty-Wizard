using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Automation;
using System.Drawing;

namespace WindowsFormsApplication1
{

    public class webPull : Form{

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        public IntPtr handle { get; set; }

        public webPull()
        {
           // Thread worker = new Thread(this.htmlPull);
            //worker.IsBackground = true;
            //worker.Start();
            
        }

        public void htmlPull()
        {
            System.Windows.Point point = new System.Windows.Point(Cursor.Position.X, Cursor.Position.Y);
            AutomationElement element = AutomationElement.FromPoint(point);
            Console.Write(point);

            try
            {
                ValuePattern value = element.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                if (value.Current.Value != "")
                {
                        Program.GUIref.Invoke((MethodInvoker)delegate
                        {
                            try
                            {
                                Program.GUIref.webBrowser1.Navigate(value.Current.Value);
                            }
                            catch { }
                        });
                }

                Program.GUIref.Invoke((MethodInvoker)delegate
                {
                    Program.GUIref.textBox1.Text = element.Current.Name;
                });
            }
            catch
            {
                Program.GUIref.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        Program.GUIref.textBox1.Text = element.Current.Name;
                    }
                    catch { }
                });
            }
            

        }

        public void cursorLoc()
        {
            MessageBox.Show(System.Windows.Forms.Cursor.Position.X + " " + System.Windows.Forms.Cursor.Position.Y);
        }

        private void htmlSearch(string html)
        {
            if (html.Contains("Duty Sheet 4"))
            {
                Program.GUIref.textBox1.Text = "true";
            }
        }
    }

    public class automation
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);


        public void emailCheck(object obg, EventArgs e)
        {
            GUI.formControls fC = new GUI.formControls();
            fC.jsInjector();
        }

        public void sheetLoaded()
        {
            Thread.Sleep(2000);
            this.sheetAutomation();
        }

        private void sheetAutomation()
        {
            int x = 135;
            int y = 525;
            string[] direction = new string[] { "{DOWN}", "{LEFT}", "{UP}", "{RIGHT}" };
            int[] r = new int[2] {579,362};
            int[] o = new int[2] {598,362};
            int[] ye = new int[2] {616,362};
            int[] g = new int[2] {634,362};
            int[] b = new int[2] {688,362};
            int[] i = new int[2] {652,362};
            int[] v = new int[2] {706,362};
            int[][] clicks = new int[][] {r,o,ye,g,b,i,v};
            Cursor.Position = new Point(x, y);

                this.click(102,395);
                Thread.Sleep(20);

                while (Program.running)
                {
                    int last = 0;
                    foreach (int[] index in clicks)
                    {
                        this.click(558, 274);
                        Thread.Sleep(50);
                        int xPos = index[0];
                        int yPos = index[1];
                        this.click(xPos, yPos);
                        Random rnd = new Random();
                        int dir = rnd.Next(4);

                        if (Program.sheetPos[0] <= 0 && dir == 1)
                        {
                            dir = 3;
                        }
                        if (Program.sheetPos[0] >= 10 && dir == 3)
                        {
                            dir = 1;
                        }
                        if (Program.sheetPos[1] <= 0 && dir == 2)
                        {
                            dir = 0;
                        }
                        if (Program.sheetPos[1] >= 21 && dir == 0)
                        {
                            dir = 2;
                        }

                        if (last == dir)
                        {
                            dir = rnd.Next(4);
                        }

                        last = dir;

                        if (dir == 0) { Program.sheetPos[1] += 1; }
                        if (dir == 1) { Program.sheetPos[0] -= 1; }
                        if (dir == 2) { Program.sheetPos[1] -= 1; }
                        if (dir == 3) { Program.sheetPos[0] += 1; }

                        System.Windows.Forms.SendKeys.SendWait(direction[dir]);
                    }
                }
                //(558,274)
                //10,21

                //149,514
        }

        private void click(int x, int y)
        {
            Cursor.Position = new Point(x, y);
            MouseEvent(automation.MouseEventFlags.LeftDown);
            Thread.Sleep(1);
            MouseEvent(automation.MouseEventFlags.LeftUp);
        }

        public void MouseEvent(MouseEventFlags value)
        {
            mouse_event
                ((int)value,
                 103,72,
                 0,
                 0)
                ;
        }
        //[DllImport("user32.dll")] static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        //
    }

    public static class Program
    {
        public static GUI GUIref;
        public static bool activated = false;
        public static bool firstLoad = true;
        public static System.Windows.Forms.Timer sW;
        public static int[] sheetPos = new int[] { 0, 0 };
        public static bool running = false;

        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Program.GUIref = new GUI());
        }
    }
}
