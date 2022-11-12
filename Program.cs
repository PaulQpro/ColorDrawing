using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Program;
using Program.Forms;
using Program.Forms.Elements;

namespace Program.Forms
{
    abstract class Window : Form
    {
        protected void InitializeForm(int Widht, int Height, string Title)
        {
            this.Size = new Size(Widht, Height);
            this.Text = Title;
        }
        protected void InitializeLinkControl(Control link,int PosX,int PosY,int Width, int Height, string Text, EventHandler onClick)
        {
            link.Location = new Point(PosX, PosY);
            link.Size = new Size(Width, Height);
            link.Text = Text;
            link.Click += onClick;
            this.Controls.Add(link);
        }
        protected void InitializeStaticControl(Control control, int PosX, int PosY, int Width, int Height, string Text)
        {
            control.Location = new Point(PosX, PosY);
            control.Size = new Size(Width, Height);
            control.Text = Text;
            this.Controls.Add(control);
        }
    }
    class MainWindow : Window
    {
        Button Drawbtn = new();
        Label Rlabel = new();
        TextBox Rbox = new();
        Label Glabel = new();
        TextBox Gbox = new();
        Label Blabel = new();
        TextBox Bbox = new();
        Graphics gr;
        public MainWindow()
        {
            InitializeForm(800, 450, "Let's Draw");
            InitializeLinkControl(Drawbtn, 608, 30, 96, 25, "Let's Draw", Draw);
            InitializeStaticControl(Rlabel, 0, 0, 50, 20, "Red");
            InitializeStaticControl(Glabel, 50, 0, 50, 20, "Green");
            InitializeStaticControl(Blabel, 110, 0, 50, 20, "Blue");
            InitializeStaticControl(Rbox, 0, 20, 50, 20, "y");
            InitializeStaticControl(Gbox, 50, 20, 50, 20, "x");
            InitializeStaticControl(Bbox, 110, 20, 50, 20, "(y+x)/2");
            gr = this.CreateGraphics();
        }
        public void Draw(object? sender, EventArgs eventArgs)
        {
            (sender as Control).Enabled = false;
            for (int y = 0; y < 256; y++)
            {
                for (int x = 0; x < 256; x++)
                {
                    DataTable dt = new();
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                    int r = (int)(double.Parse(dt.Compute(Rbox.Text.ToUpper().Replace("X", $"{x}").Replace("Y", $"{y}"), "").ToString()));
                    int g = (int)(double.Parse(dt.Compute(Gbox.Text.ToUpper().Replace("X", $"{x}").Replace("Y", $"{y}"), "").ToString()));
                    int b = (int)(double.Parse(dt.Compute(Bbox.Text.ToUpper().Replace("X", $"{x}").Replace("Y", $"{y}"), "").ToString()));
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                    dt.Dispose();
                    SolidBrush br = new(Color.FromArgb(r,g,b));
                    gr.FillRectangle(br, x+544-16, y + 55, 1, 1);
                }
            }
            (sender as Control).Enabled = true;
        }
    }
}
namespace Program
{
    class Program
    {
        static void Main()
        {
            Application.Run(new MainWindow());
        }
    }
}