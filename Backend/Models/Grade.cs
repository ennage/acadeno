namespace Acadeno.Backend.Models
{
    public class Grade
    {
        public string GradedID { get; set; } = string.Empty;
        public double RawGrade { get; set; }

        public double GetRawGrade() => RawGrade;
        public void SetRawGrade(double rawGrade)
        {
            if(rawGrade >= 0 && rawGrade <= 100)
            {
                RawGrade = rawGrade;
            }
        }
        public double getGWA()
        {
            if (RawGrade >= 97 && RawGrade <= 100) return 1.0;
            else if (RawGrade >= 94 && RawGrade < 97) return 1.25;
            else if (RawGrade >= 91 && RawGrade < 94) return 1.5;
            else if (RawGrade >= 88 && RawGrade < 91) return 1.75;
            else if (RawGrade >= 85 && RawGrade < 88) return 2.0;
            else if (RawGrade >= 82 && RawGrade < 85) return 2.25;
            else if (RawGrade >= 79 && RawGrade < 82) return 2.5;
            else if (RawGrade >= 76 && RawGrade < 79) return 2.75;
            else if (RawGrade >= 75 && RawGrade < 76) return 3.0;
            else return 5.0;
        }
    
    }
}