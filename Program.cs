using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public partial class AnimForm : Form
{
    private float angle;
    private Timer timer = new Timer();
    private BufferedGraphics bufferedGraphics;

    public AnimForm()
    {
        BufferedGraphicsContext context = BufferedGraphicsManager.Current;
        context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
        bufferedGraphics = context.Allocate(this.CreateGraphics(),
        new Rectangle(0, 0, this.Width, this.Height));
        timer.Enabled = true;
        timer.Tick += OnTimer;
        timer.Interval = 20; // 50 images per second.
        timer.Start();
    }

    private void OnTimer(object sender, System.EventArgs e)
    {
        angle++;
        if (angle > 359)
            angle = 0;
        Graphics g = bufferedGraphics.Graphics;
        g.Clear(Color.Black);
        Matrix matrix = new Matrix();
        matrix.Rotate(angle, MatrixOrder.Append);
        matrix.Translate(this.ClientSize.Width / 2,
            this.ClientSize.Height / 2, MatrixOrder.Append);
        g.Transform = matrix;
        g.FillRectangle(Brushes.Azure, -100, -100, 200, 200);
        bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
    }

    [System.STAThread]
    public static void Main()
    {
        Application.Run(new AnimForm());
    }
}