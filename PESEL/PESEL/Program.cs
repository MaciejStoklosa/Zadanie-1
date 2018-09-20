using System;
using System.Text.RegularExpressions;

namespace PESEL
{
    class Program
    {

        static void Main(string[] args)
        {
            // Zadanie 1 
            while (true)
            {
                // Wprowadzam pesel

                Console.WriteLine("Wprowadź PESEL");
                string pesel = Console.ReadLine();

                // poprawna liczba znaków 
                if (pesel.Length != 11)
                {
                    Console.WriteLine("Niepoprawna ilość znaków");
                    Console.WriteLine();
                    continue;
                }
                //właściwy format 
                if (Regex.Matches(pesel, @"[a-zA-Z]").Count > 0)
                {
                    Console.WriteLine("Zly format");
                    Console.WriteLine();
                    continue;
                }
                //Obliczamy sumę kontrolną 
                int suma = 0;
                int factor = 11;
                string peselv2 = pesel;
                for (int i = 0; i < pesel.Length - 1; i++)
                {
                    factor -= 2;
                    if (factor == 5)
                    {
                        factor = 3;
                    }
                    else if (factor == -1)
                    {
                        factor = 9;
                    }
                    int c_int = (int)pesel[i] - '0';
                    suma += factor * c_int;
                }
                if (suma % 10 != pesel[10] - '0')
                {
                    Console.WriteLine("Twój pesel jest nie poprawny, niewłaściwa suma kontrolna. ");
                    Console.WriteLine();
                    continue;
                }
                
                
                // poprawna liczba dni 
                string days = "" + pesel[4] + pesel[5];
                int days_int = Int32.Parse(days);
                // nie chce mi się uwzględniać ile dany miesiąc ma dni, czy rok jest przestępny 

                if (!(days_int > 0 && days_int < 31))
                {
                    Console.WriteLine("Nie ta ilość dni, kolego");
                    Console.WriteLine();
                    break;
                }

                // porawna liczba miesiąc, sprawdzam czy miesiąc należy do włąsciwego przedziału
                string month = "" + pesel[2] + pesel[3];
                int month_int = Int32.Parse(month);
                int from = 1;
                int to = 12;
                bool isValid = false;
                for (int i = 0; i < 5; i++)
                {
                    if ((from + i * 20 < month_int) && (to + i * 20 > month_int))
                    {
                        isValid = true;
                        break;
                    }
                }
                if (!isValid)
                {
                    Console.WriteLine("Twój PESEL jest niepoprawny. Nie ma tyle miesięcy ");
                    Console.WriteLine();
                    continue;
                }

                // sprawdzam stulecie na podstawie trzeciej i czwartej cyfry peselu 
                var key = (month_int - 1) / 12;
                int caseSwitch = key;
                string century;
                switch (caseSwitch)
                {
                    case 1:
                        century = "20";
                        month_int -= 20;
                        break;
                    case 2:
                        century = "20";
                        month_int -= 20;
                        break;
                    case 3:
                        century = "21";
                        month_int -= 40;
                        break;
                    case 4:
                        century = "21";
                        month_int -= 40;
                        break;
                    case 5:
                        century = "22";
                        month_int -= 60;
                        break;
                    case 6:
                        century = "18";
                        month_int -= 80;
                        break;
                    case 7:
                        century = "18";
                        month_int -= 80;
                        break;
                    default:
                        century = "19";
                        break;
                }
                string m = "" + month_int;
                if (month_int < 10)
                {
                    m = "0" + month_int;
                }
                else
                {
                    m += month_int;
                }
                string pesel_date = century + pesel[0] + pesel[1] + "-" + m + "-" + pesel[4] + pesel[5];
                string sex;
                //sprawdzam płeć na podstawie dziesiątej cyfry peselu
                if ((pesel[9]-'0')%2 == 0)
                {
                    sex = "Kobieta";
                }
                else
                {
                    sex = "Mężczyzna";
                }
                Console.WriteLine("Data urodzenia to " + pesel_date + "\npłeć: " + sex);
                Console.WriteLine();
            }
        }
    }
}

