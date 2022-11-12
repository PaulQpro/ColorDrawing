using System.Data;
using static System.Math;
namespace Program.Forms
{
    class Pallete : Window
    {
        readonly Button Drawbtn = new();
        readonly Label Rlabel = new();
        readonly TextBox Rbox = new();
        readonly Label Glabel = new();
        readonly TextBox Gbox = new();
        readonly Label Blabel = new();
        readonly TextBox Bbox = new();
        readonly Label ExeptionLabel = new();
        readonly Label ColorModLabel = new();
        readonly NumericUpDown CM = new();
        readonly SaveFileDialog sf = new();
        readonly Button savepallete = new();
        readonly Graphics gr;
        readonly Bitmap bmp = new(256, 256);
        public Pallete()
        {
            InitializeForm(840, 450, "Let's Draw");
            InitializeLinkControl(Drawbtn, 608, 25, 96, 25, "Let's Draw", Draw);
            InitializeLinkControl(savepallete, 608, 365, 96, 25, "Save", Save);
            InitializeStaticControl(Rlabel, 0, 0, 50, 20, "Red");
            InitializeStaticControl(Glabel, 0, 50, 50, 20, "Green");
            InitializeStaticControl(Blabel, 0, 110, 50, 20, "Blue");
            InitializeStaticControl(Rbox, 0, 20, 150, 20, "y");
            InitializeStaticControl(Gbox, 0, 70, 150, 20, "x");
            InitializeStaticControl(Bbox, 0, 130, 150, 20, "x");
            InitializeStaticControl(ExeptionLabel, 150, 70, 360, 50, "");
            InitializeStaticControl(ColorModLabel, 200, 0, 100, 40, "Color pallete\n(size 2^x)");
            InitializeStaticControl(CM, 200, 40, 70, 20, "8");
            CM.Minimum = 1;
            CM.Maximum = 8;
            CM.Value = 8;
            CM.Increment = 1;
            sf.FileName = "pallete.png";
            sf.Filter = "PNG Image|*.png|JPG Image|*.jpg; *.jpeg";
            savepallete.Enabled = false;
            gr = this.CreateGraphics();
            gr.FillRectangle(new SolidBrush(BackColor), 528, 55, 256, 256);
        }
        public void Draw(object? sender, EventArgs eventArgs)
        {
            savepallete.Enabled = true;
            int r = 0;
            int g = 0;
            int b = 0;
            gr.FillRectangle(new SolidBrush(BackColor), 528, 55, 256, 256);
            try
            {
                ExeptionLabel.Text = "";
                DataTable dt = new();
                int Border = (int)Pow(2, (double)CM.Value);
                int ColorMod = 256 / Border;
                Graphics gra = Graphics.FromImage(bmp);
                for (int y = 0; y < Border; y++)
                {
                    for (int x = 0; x < Border; x++)
                    {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                        r = (int)(double.Parse(dt.Compute(Rbox.Text.ToUpper().Replace("X", $"{x * ColorMod}").Replace("Y", $"{y * ColorMod}"), "").ToString()));
                        g = (int)(double.Parse(dt.Compute(Gbox.Text.ToUpper().Replace("X", $"{x * ColorMod}").Replace("Y", $"{y * ColorMod}"), "").ToString()));
                        b = (int)(double.Parse(dt.Compute(Bbox.Text.ToUpper().Replace("X", $"{x * ColorMod}").Replace("Y", $"{y * ColorMod}"), "").ToString()));
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                        SolidBrush br = new(Color.FromArgb(r, g, b));
                        gr.FillRectangle(br, x * ColorMod + 528, y * ColorMod + 55, ColorMod, ColorMod);
                        gra.FillRectangle(br, x * ColorMod, y * ColorMod, ColorMod, ColorMod);
                    }
                }
                //gr.DrawImage(bmp, 528, 55);
            }
            catch (System.ArgumentException)
            {
                ExeptionLabel.Text = $"Value Overflow of color's value:\nRed: {r}; Green: {g}; Blue: {b}";
            }
            catch (System.Data.SyntaxErrorException)
            {
                ExeptionLabel.Text = "Wrong Math Expression Format";
            }
            catch (System.FormatException)
            {
                ExeptionLabel.Text = "Wrong Math Expression Format";
            }
            catch (System.Data.EvaluateException)
            {
                ExeptionLabel.Text = "Expression can contain:\nNumbers: (0-9), Operations signs: (+,-,*,/),\nBrackets: '(,)', Varibles: (x,y)";
            }
        }
        public void Save(object? sender, EventArgs eventArgs)
        {
            if (sf.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(sf.FileName);
            }
        }
    }
}