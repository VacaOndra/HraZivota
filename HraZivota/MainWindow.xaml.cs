using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HraZivota
{
    public partial class MainWindow : Window
    {
        private Bunka[,] poleBunek;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            NaplPole(160,90);
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            PrepocitejBunky();
        }

        private void NaplPole(int sirka, int vyska)
        {
            poleBunek = new Bunka[sirka, vyska];

            for (int i = 0; i < poleBunek.GetLength(0); i++)
            {
                for (int j = 0; j < poleBunek.GetLength(1); j++)
                {
                    poleBunek[i, j] = new Bunka();
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VykresliPlatno();
            timer.Start();
        }
        private void VykresliPlatno()
        {
            canvas.Children.Clear();
            double sire = canvas.ActualWidth / poleBunek.GetLength(0);
            double vyska = canvas.ActualHeight / poleBunek.GetLength(1);
            for (int i = 0; i < poleBunek.GetLength(0); i++)
            {
                for (int j = 0; j < poleBunek.GetLength(1); j++)
                {
                    poleBunek[i, j].rectangle.Width = sire;
                    poleBunek[i, j].rectangle.Height = vyska;
                    Canvas.SetTop(poleBunek[i, j].rectangle, vyska * j);
                    Canvas.SetLeft(poleBunek[i, j].rectangle, sire * i);
                    canvas.Children.Add(poleBunek[i, j].rectangle);
                }
            }
        }
        private void PrepocitejBunky()
        {
            for (int i = 0; i < poleBunek.GetLength(0); i++)
            {
                for (int j = 0; j < poleBunek.GetLength(1); j++)
                {
                    poleBunek[i, j].PredchoziStav = poleBunek[i, j].AktualniStav;
                    int ziveBunky = 0;
                    //nahore vlevo
                    if (i > 0 && j > 0)
                    {
                        if (poleBunek[i - 1, j - 1].PredchoziStav == Stavy.Ziva)
                        {
                            ziveBunky++;
                        }
                    }
                    //nahore
                    if (j > 0)
                    {
                        if (poleBunek[i, j - 1].PredchoziStav == Stavy.Ziva)
                        {
                            ziveBunky++;
                        }
                    }
                    //nahore vpravo
                    if (i < poleBunek.GetLength(0) - 1 && j > 0)
                    {
                        if (poleBunek[i + 1, j - 1].PredchoziStav == Stavy.Ziva)
                        {
                            ziveBunky++;
                        }
                    }
                    //vlevo
                    if (i > 0)
                    {
                        if (poleBunek[i - 1, j].PredchoziStav == Stavy.Ziva)
                        {
                            ziveBunky++;
                        }
                    }
                    //vpravo
                    if (i < poleBunek.GetLength(0) - 1)
                    {
                        if (poleBunek[i + 1, j].PredchoziStav == Stavy.Ziva)
                        {
                            ziveBunky++;
                        }
                    }
                    //dole vlevo
                    if (i > 0 && j < poleBunek.GetLength(1) - 1)
                    {
                        if (poleBunek[i - 1, j + 1].PredchoziStav == Stavy.Ziva)
                        {
                            ziveBunky++;
                        }
                    }
                    //dole
                    if (j < poleBunek.GetLength(1) - 1)
                    {
                        if (poleBunek[i, j + 1].PredchoziStav == Stavy.Ziva)
                        {
                            ziveBunky++;
                        }
                    }
                    //dole vpravo
                    if (i < poleBunek.GetLength(0) - 1 && j < poleBunek.GetLength(1) - 1)
                    {
                        if (poleBunek[i + 1, j + 1].PredchoziStav == Stavy.Ziva)
                        {
                            ziveBunky++;
                        }
                    }
                    if (poleBunek[i, j].AktualniStav == Stavy.Ziva && ziveBunky < 2)
                    {
                        poleBunek[i, j].AktualniStav = Stavy.Mrtva;
                    }
                    else if (poleBunek[i, j].AktualniStav == Stavy.Ziva && ziveBunky >= 2 && ziveBunky <= 3)
                    {
                        poleBunek[i, j].AktualniStav = Stavy.Ziva;
                    }
                    else if (poleBunek[i, j].AktualniStav == Stavy.Ziva && ziveBunky > 3)
                    {
                        poleBunek[i, j].AktualniStav = Stavy.Mrtva;
                    }
                    else if (poleBunek[i, j].AktualniStav == Stavy.Mrtva && ziveBunky == 3)
                    {
                        poleBunek[i, j].AktualniStav = Stavy.Ziva;
                    }
                }
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            VykresliPlatno();
        }
    }
}