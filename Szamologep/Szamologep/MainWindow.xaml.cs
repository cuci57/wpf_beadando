using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        private string jelenlegiSzam = "";
        private string elozoSzam = "";
        private string muvelet = "";

        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += new System.Windows.Input.KeyEventHandler(MainWindow_KeyDown);
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
                    elozoSzam = jelenlegiSzam;
                    jelenlegiSzam = "";
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
        
        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                // SZámok gombjai
                case System.Windows.Input.Key.D0:
                case System.Windows.Input.Key.NumPad0:
                    AppendNumber("0");
                    break;
                case System.Windows.Input.Key.D1:
                case System.Windows.Input.Key.NumPad1:
                    AppendNumber("1");
                    break;
                case System.Windows.Input.Key.D2:
                case System.Windows.Input.Key.NumPad2:
                    AppendNumber("2");
                    break;
                case System.Windows.Input.Key.D3:
                case System.Windows.Input.Key.NumPad3:
                    AppendNumber("3");
                    break;
                case System.Windows.Input.Key.D4:
                case System.Windows.Input.Key.NumPad4:
                    AppendNumber("4");
                    break;
                case System.Windows.Input.Key.D5:
                case System.Windows.Input.Key.NumPad5:
                    AppendNumber("5");
                    break;
                case System.Windows.Input.Key.D6:
                case System.Windows.Input.Key.NumPad6:
                    AppendNumber("6");
                    break;
                case System.Windows.Input.Key.D7:
                case System.Windows.Input.Key.NumPad7:
                    AppendNumber("7");
                    break;
                case System.Windows.Input.Key.D8:
                case System.Windows.Input.Key.NumPad8:
                    AppendNumber("8");
                    break;
                case System.Windows.Input.Key.D9:
                case System.Windows.Input.Key.NumPad9:
                    AppendNumber("9");
                    break;
                case System.Windows.Input.Key.OemPeriod:
                case System.Windows.Input.Key.Decimal:
                    AppendNumber(".");
                    break;

                // Műveleti gombok
                case System.Windows.Input.Key.Add:
                case System.Windows.Input.Key.OemPlus when Keyboard.Modifiers == ModifierKeys.None:
                    Setmuvelet("+");
                    break;
                case System.Windows.Input.Key.Subtract:
                case System.Windows.Input.Key.OemMinus:
                    Setmuvelet("-");
                    break;
                case System.Windows.Input.Key.Multiply:
                    Setmuvelet("*");
                    break;
                case System.Windows.Input.Key.Divide:
                    Setmuvelet("/");
                    break;

                // Egyenlőség gomb
                case System.Windows.Input.Key.Enter:
                    EredmenyKiszamitas();
                    break;

                // Törlő gombok
                case System.Windows.Input.Key.Back:
                case System.Windows.Input.Key.Delete:
                    ClearCalculator();
                    break;
            }
        }
        private void AppendNumber(string szam)
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

        private void Setmuvelet(string op)
        {
            if (jelenlegiSzam != "")
            {
                elozoSzam = jelenlegiSzam;
                jelenlegiSzam = "";
                muvelet = op;
            }
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

        private void ClearCalculator()
        {
            jelenlegiSzam = "";
            elozoSzam = "";
            muvelet = "";
            Kijelzo.Text = "0";
        }
    }
}
