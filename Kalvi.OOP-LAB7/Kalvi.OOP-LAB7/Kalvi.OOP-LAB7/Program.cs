using System;

namespace LabWork
{
    public class ZNO
    {
        public string Subject { get; set; }
        public int Points { get; set; }

        public ZNO()
        {
            Subject = "Unknown";
            Points = 0;
        }

        public ZNO(string subject, int points)
        {
            Subject = subject;
            Points = points;
        }

        public ZNO(ZNO other)
        {
            Subject = other.Subject;
            Points = other.Points;
        }

        public override string ToString()
        {
            return $"Subject: {Subject}, Points: {Points}";
        }
    }

    public class Entrant
    {
        public string FullName { get; set; }
        public string IdNum { get; set; }
        public double AvgPoints { get; set; }
        public bool IsAwarded { get; set; }
        public ZNO[] ZNOResults { get; set; }

        public double MonthlyFee { get; private set; }
        public double YearlyFee => MonthlyFee * 10;
        public double FullPeriodFee => MonthlyFee * 40;

        public Entrant()
        {
            FullName = "Unknown";
            IdNum = "000000";
            AvgPoints = 0.0;
            IsAwarded = false;
            ZNOResults = Array.Empty<ZNO>();
            MonthlyFee = 0.0;
        }

        public Entrant(string fullName, string idNum, double avgPoints, bool isAwarded, ZNO[] znoResults, double monthlyFee)
        {
            FullName = fullName;
            IdNum = idNum;
            AvgPoints = avgPoints;
            IsAwarded = isAwarded;
            ZNOResults = znoResults;
            MonthlyFee = monthlyFee;
        }

        public Entrant(Entrant other)
        {
            FullName = other.FullName;
            IdNum = other.IdNum;
            AvgPoints = other.AvgPoints;
            IsAwarded = other.IsAwarded;
            ZNOResults = (ZNO[])other.ZNOResults.Clone();
            MonthlyFee = other.MonthlyFee;
        }

        public string GetBestSubject()
        {
            if (ZNOResults.Length == 0) return "No results available.";
            ZNO best = ZNOResults[0];
            foreach (var zno in ZNOResults)
            {
                if (zno.Points > best.Points) best = zno;
            }
            return best.Subject;
        }

        public bool IsOnTopOfTheRating()
        {
            return IsAwarded && AvgPoints >= 4.9;
        }

        public void InputFee()
        {
            Console.WriteLine("Оберіть одиниці для введення вартості навчання:");
            Console.WriteLine("1 - за місяць");
            Console.WriteLine("2 - за рік");
            Console.WriteLine("3 - за весь період навчання");

            int choice = int.Parse(Console.ReadLine() ?? "1");
            Console.WriteLine("Введіть суму:");
            double amount = double.Parse(Console.ReadLine() ?? "0");

            switch (choice)
            {
                case 1:
                    MonthlyFee = amount;
                    break;
                case 2:
                    MonthlyFee = amount / 10;
                    break;
                case 3:
                    MonthlyFee = amount / 40;
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }

        public void DisplayFees()
        {
            Console.WriteLine($"Вартість навчання за місяць: {MonthlyFee} грн");
            Console.WriteLine($"Вартість навчання за рік: {YearlyFee} грн");
            Console.WriteLine($"Вартість навчання за весь період: {FullPeriodFee} грн");
        }

        public override string ToString()
        {
            return $"FullName: {FullName}, IdNum: {IdNum}, AvgPoints: {AvgPoints}, IsAwarded: {IsAwarded}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ZNO[] znoResults = new ZNO[]
            {
                new ZNO("Математика", 138),
                new ZNO("Українська мова", 156),
                new ZNO("Англійська мова", 160)
            };

            Entrant entrant = new Entrant(
                "Kalvi Eros",
                "8956723412",
                4.5,
                false,
                znoResults,
                0.0
            );

            Console.WriteLine("Entrant details:");
            Console.WriteLine(entrant);
            Console.WriteLine("ZNO Results:");
            foreach (var zno in entrant.ZNOResults)
            {
                Console.WriteLine(zno);
            }

            Console.WriteLine($"Best Subject: {entrant.GetBestSubject()}");
            Console.WriteLine($"Is on top of the rating: {entrant.IsOnTopOfTheRating()}");

            entrant.InputFee();

            entrant.DisplayFees();
        }
    }
}
