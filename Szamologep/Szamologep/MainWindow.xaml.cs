using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        //Kis András készítette
        private string jelenlegiSzam = "";
        private string elozoSzam = "";
        private string muvelet = "";

        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += new System.Windows.Input.KeyEventHandler(FoAblak_BillentyuLenyomas);
        }

        private void SzamGomb_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (button.Content.ToString() == ".")
                {
                    if (!jelenlegiSzam.Contains("."))
                    {
                        jelenlegiSzam += ".";
                    }
                }
                else
                {
                    jelenlegiSzam += button.Content.ToString();
                }
                Kijelzo.Text = jelenlegiSzam;
            }
        }

        private void MuveletijelGomb_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (jelenlegiSzam != "")
                {
                    if (!string.IsNullOrEmpty(jelenlegiSzam))
                    {
                        if (!string.IsNullOrEmpty(elozoSzam))
                        {
                            EredmenyKiszamitas();
                        }
                        elozoSzam = jelenlegiSzam;
                        jelenlegiSzam = "";
                    }
                    
                    muvelet = button.Content.ToString();
                }
            }
        }

        private void EgyenlosegjelGomb_Click(object sender, RoutedEventArgs e)
        {
            if (elozoSzam != "" && jelenlegiSzam != "")
            {
                // '.' és ','-vel is működjön a tizedes tört
                string num1Str = elozoSzam.Replace('.', ',');
                string num2Str = jelenlegiSzam.Replace('.', ',');

                double num1;
                double num2;

                if (double.TryParse(num1Str, out num1) && double.TryParse(num2Str, out num2))
                {
                    double eredmeny = 0;

                    switch (muvelet)
                    {
                        case "+":
                            eredmeny = num1 + num2;
                            break;
                        case "-":
                            eredmeny = num1 - num2;
                            break;
                        case "*":
                            eredmeny = num1 * num2;
                            break;
                        case "/":
                            if (num2 != 0)
                            {
                                eredmeny = num1 / num2;
                            }
                            else
                            {
                                MessageBox.Show("A nullával való osztás nem engedélyezett!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            break;
                    }

                    Kijelzo.Text = eredmeny.ToString();
                    jelenlegiSzam = eredmeny.ToString();
                    elozoSzam = "";
                    muvelet = "";
                }
                else
                {
                    MessageBox.Show("Érvénytelen számformátum", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void TorlesGomb_Click(object sender, RoutedEventArgs e)
        {
            jelenlegiSzam = "";
            elozoSzam = "";
            muvelet = "";
            Kijelzo.Text = "0";
        }
        //Tóth Kende készítette
        private void FoAblak_BillentyuLenyomas(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Számok gombjai
                case System.Windows.Input.Key.D0:
                case System.Windows.Input.Key.NumPad0:
                    SzamMellekeles("0");
                    break;
                case System.Windows.Input.Key.D1:
                case System.Windows.Input.Key.NumPad1:
                    SzamMellekeles("1");
                    break;
                case System.Windows.Input.Key.D2:
                case System.Windows.Input.Key.NumPad2:
                    SzamMellekeles("2");
                    break;
                case System.Windows.Input.Key.D3:
                case System.Windows.Input.Key.NumPad3:
                    SzamMellekeles("3");
                    break;
                case System.Windows.Input.Key.D4:
                case System.Windows.Input.Key.NumPad4:
                    SzamMellekeles("4");
                    break;
                case System.Windows.Input.Key.D5:
                case System.Windows.Input.Key.NumPad5:
                    SzamMellekeles("5");
                    break;
                case System.Windows.Input.Key.D6:
                case System.Windows.Input.Key.NumPad6:
                    SzamMellekeles("6");
                    break;
                case System.Windows.Input.Key.D7:
                case System.Windows.Input.Key.NumPad7:
                    SzamMellekeles("7");
                    break;
                case System.Windows.Input.Key.D8:
                case System.Windows.Input.Key.NumPad8:
                    SzamMellekeles("8");
                    break;
                case System.Windows.Input.Key.D9:
                case System.Windows.Input.Key.NumPad9:
                    SzamMellekeles("9");
                    break;
                case System.Windows.Input.Key.OemPeriod:
                case System.Windows.Input.Key.Decimal:
                    SzamMellekeles(".");
                    break;

                // Műveleti gombok
                case System.Windows.Input.Key.Add:
                case System.Windows.Input.Key.OemPlus when Keyboard.Modifiers == ModifierKeys.None:
                    SetMuvelet("+");
                    break;
                case System.Windows.Input.Key.Subtract:
                case System.Windows.Input.Key.OemMinus:
                    SetMuvelet("-");
                    break;
                case System.Windows.Input.Key.Multiply:
                    SetMuvelet("*");
                    break;
                case System.Windows.Input.Key.Divide:
                    SetMuvelet("/");
                    break;

                // Egyenlőség gomb
                case System.Windows.Input.Key.Enter:
                    EredmenyKiszamitas();
                    break;

                // Törlő gombok
                case System.Windows.Input.Key.Back:
                case System.Windows.Input.Key.Delete:
                    SzamologepTorlese();
                    break;
            }
        }
        //Kis András készítette
        private void SzamMellekeles(string szam)
        {
            if (szam == ".")
            {
                if (!jelenlegiSzam.Contains("."))
                {
                    jelenlegiSzam += ".";
                }
            }
            else
            {
                jelenlegiSzam += szam;
            }
            Kijelzo.Text = jelenlegiSzam;
        }

        private void SetMuvelet(string op)
        {
            if (!string.IsNullOrEmpty(jelenlegiSzam))
            {
                if (!string.IsNullOrEmpty(elozoSzam))
                {
                    EredmenyKiszamitas();
                    elozoSzam = jelenlegiSzam;
                    jelenlegiSzam = "";
                }
                else
                {
                    elozoSzam = jelenlegiSzam;
                    jelenlegiSzam = "";
                }
            }
            muvelet = op;
        }

        private void EredmenyKiszamitas()
        {
            if (elozoSzam != "" && jelenlegiSzam != "")
            {
                string num1Str = elozoSzam.Replace('.', ',');
                string num2Str = jelenlegiSzam.Replace('.', ',');

                double num1;
                double num2;

                if (double.TryParse(num1Str, out num1) && double.TryParse(num2Str, out num2))
                {
                    double eredmeny = 0;

                    switch (muvelet)
                    {
                        case "+":
                            eredmeny = num1 + num2;
                            break;
                        case "-":
                            eredmeny = num1 - num2;
                            break;
                        case "*":
                            eredmeny = num1 * num2;
                            break;
                        case "/":
                            if (num2 != 0)
                            {
                                eredmeny = num1 / num2;
                            }
                            else
                            {
                                MessageBox.Show("A nullával való osztás nem engedélyezett.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            break;
                    }

                    Kijelzo.Text = eredmeny.ToString();
                    jelenlegiSzam = eredmeny.ToString();
                    elozoSzam = "";
                    muvelet = "";
                }
                else
                {
                    MessageBox.Show("Érvénytelen számformátum", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SzamologepTorlese()
        {
            jelenlegiSzam = "";
            elozoSzam = "";
            muvelet = "";
            Kijelzo.Text = "0";
        }
    }
}
