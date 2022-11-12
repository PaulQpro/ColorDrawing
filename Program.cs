using System;
using static System.Math;
using Program.Forms;

namespace Program.Forms
{
    abstract public class Window : Form
    {

#pragma warning disable CA1822 // Пометьте члены как статические
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
        protected void InitializeLinkControl(Control link, Control motherControl, int PosX, int PosY, int Width, int Height, string Text, EventHandler onClick)
        {
            link.Location = new Point(PosX, PosY);
            link.Size = new Size(Width, Height);
            link.Text = Text;
            link.Click += onClick;
            motherControl.Controls.Add(link);
        }
        protected void InitializeStaticControl(Control control, int PosX, int PosY, int Width, int Height, string Text)
        {
            control.Location = new Point(PosX, PosY);
            control.Size = new Size(Width, Height);
            control.Text = Text;
            this.Controls.Add(control);
        }
        protected void InitializeStaticControl(Control control, Control motherControl, int PosX, int PosY, int Width, int Height, string Text)
        {
            control.Location = new Point(PosX, PosY);
            control.Size = new Size(Width, Height);
            control.Text = Text;
            motherControl.Controls.Add(control);
        }
        protected void InitializeStaticControl(Control control, int PosX, int PosY, int Width, int Height)
        {
            control.Location = new Point(PosX, PosY);
            control.Size = new Size(Width, Height);
            this.Controls.Add(control);
        }
        protected void InitializeStaticControl(Control control, Control motherControl, int PosX, int PosY, int Width, int Height)
        {
            control.Location = new Point(PosX, PosY);
            control.Size = new Size(Width, Height);
            motherControl.Controls.Add(control);
        }

#pragma warning restore CA1822 // Пометьте члены как статические
    }
    class Menu : Window
    {
        readonly Panel MainPanel = new();
        public Menu()
        {
            InitializeForm(400, 340, "Some stupid Program");
            InitializeStaticControl(MainPanel, 0, 0, 400, 300);
            MainPanel.Dock = DockStyle.Fill;

        }
    }
}
namespace Program
{
    class Program
    {
        [STAThread] 
        static void Main()
        {
            Application.Run(new Menu());
        }
    }
}