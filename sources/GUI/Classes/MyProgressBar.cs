using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JetpackGUI
{
    public enum ProgressBarDisplayMode { NoText,  Percentage,  CurrProgress,  CustomText, TextAndPercentage, TextAndCurrProgress }

    public class MyProgressBar : ProgressBar
    {
        [Description("Font of the text on ProgressBar"), Category("Additional Options")]
        public Font TextFont { get; set; } = new Font(FontFamily.GenericSerif, 11, FontStyle.Bold|FontStyle.Italic);
        
       SolidBrush _textColourBrush = (SolidBrush) Brushes.Black;
        [Category("Additional Options")]
        public Color TextColor 
        {
            get { return _textColourBrush.Color; }
            set { _textColourBrush.Dispose(); _textColourBrush = new SolidBrush(value); }
        }

       SolidBrush _progressColourBrush = (SolidBrush) Brushes.LightGreen;
        [Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color ProgressColor
        {
            get { return _progressColourBrush.Color; }
            set { _progressColourBrush.Dispose(); _progressColourBrush = new SolidBrush(value); }
        }

       ProgressBarDisplayMode _visualMode = ProgressBarDisplayMode.CurrProgress;
        [Category("Additional Options"), Browsable(true)]
        public ProgressBarDisplayMode VisualMode 
        {
            get { return _visualMode; }
            set { _visualMode = value; Invalidate(); }
        }

       string _text = string.Empty;

        [Description("If it's empty, % will be shown"), Category("Additional Options"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string CustomText
        {
            get { return _text; }
            set { _text = value; Invalidate(); }
        }

       string _textToDraw
        {
            get
            {
                string text = CustomText;
                switch (VisualMode)
                {
                    case ProgressBarDisplayMode.Percentage:
                        text = _percentageStr;
                        break;
                    case ProgressBarDisplayMode.CurrProgress:
                        text = _currProgressStr;
                        break;
                    case ProgressBarDisplayMode.TextAndCurrProgress:
                        text = $"{CustomText}: {_currProgressStr}";
                        break;
                    case ProgressBarDisplayMode.TextAndPercentage:
                        text = $"{CustomText}: {_percentageStr}";
                        break;
                }
                return text;
            }
            set { }
        }
        
       public MyProgressBar() { Value = Minimum; FixComponentBlinking(); }
       string _currProgressStr { get { return $"{Value}/{Maximum}"; } }
       void FixComponentBlinking() { SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); }
       string _percentageStr { get { return $"{(int)((float)Value - Minimum) / ((float)Maximum - Minimum) * 100 } %"; } }
       protected override void OnPaint(PaintEventArgs e) { Graphics g = e.Graphics; DrawProgressBar(g); DrawStringIfNeeded(g); }

       void DrawProgressBar(Graphics g)
        {
            Rectangle rect = ClientRectangle;
            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            if (Value > 0)
            {
                Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                g.FillRectangle(_progressColourBrush, clip);
            }
        }

       void DrawStringIfNeeded(Graphics g)
        {
            if (VisualMode != ProgressBarDisplayMode.NoText)
            {
                string text = _textToDraw;
                SizeF len = g.MeasureString(text, TextFont);
                Point location = new Point(((Width / 2) - (int)len.Width / 2), ((Height / 2) - (int)len.Height / 2));
                g.DrawString(text, TextFont, (Brush)_textColourBrush, location);
            }
        }
        
        public new void Dispose() { _textColourBrush.Dispose(); _progressColourBrush.Dispose(); base.Dispose(); }
       void InitializeComponent() { this.SuspendLayout(); this.BackColor = Color.FromArgb(69, 73, 74); this.ResumeLayout(false); }
    }
}