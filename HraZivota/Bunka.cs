using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HraZivota
{
    public enum Stavy
    {
        Ziva,
        Mrtva
    }
    class Bunka
    {
        public Stavy PredchoziStav { get; set; }
        public Stavy AktualniStav { get { return aktualniStav; } set { aktualniStav = value; ZmenBarvuBunky(); } }
        private Stavy aktualniStav;
        private Color barvaBunky = Color.FromRgb(73, 158, 191);
        private Random random = new Random();
        public Rectangle rectangle;
        public Bunka()
        {
            rectangle = new Rectangle();
            //rectangle.StrokeThickness = 0.5;
            //rectangle.Stroke = Brushes.Gray;
            if (random.Next(5) == 1)
            {
                PredchoziStav = Stavy.Ziva;
                AktualniStav = Stavy.Ziva;
            } 
            else
            {
                PredchoziStav = Stavy.Mrtva;
                AktualniStav = Stavy.Mrtva;
            }
                
        }
        private void ZmenBarvuBunky()
        {
            rectangle.Fill = (AktualniStav == Stavy.Ziva) ? new SolidColorBrush(barvaBunky) : Brushes.Transparent;
        }
    }
}
