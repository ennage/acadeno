using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Simulation
{
    public class GradeConverter
    {
        // --- INPUT REVERSE-PARSING ---
        public double ConvertInputToPercentage(string input, GradeScaleType scale)
        {
            input = input.Trim().ToUpper();

            if (scale == GradeScaleType.Percentage && double.TryParse(input.Replace("%", ""), out double pct))
                return pct;

            if (scale == GradeScaleType.GWA && double.TryParse(input, out double gwa))
            {
                if (gwa <= 1.00) return 98;
                if (gwa <= 1.25) return 94;
                if (gwa <= 1.50) return 91;
                if (gwa <= 1.75) return 88;
                if (gwa <= 2.00) return 85;
                if (gwa <= 2.25) return 82;
                if (gwa <= 2.50) return 79;
                if (gwa <= 2.75) return 76;
                if (gwa <= 3.00) return 75;
                return 0; // 5.00 Failing
            }

            if (scale == GradeScaleType.Letter)
            {
                return input switch {
                    "A+" => 97, "A" => 94, "A-" => 91,
                    "B+" => 88, "B" => 85, "B-" => 82,
                    "C+" => 79, "C" => 76, "C-" => 75,
                    _ => 0
                };
            }

            // Fallback for GPA
            if (scale == GradeScaleType.GPA && double.TryParse(input, out double gpa))
            {
                if (gpa >= 4.0) return 97;
                if (gpa >= 3.0) return 94;
                if (gpa >= 2.75) return 91;
                if (gpa >= 2.5) return 88;
                if (gpa >= 1.75) return 85;
                if (gpa >= 1.5) return 82;
                if (gpa >= 1.25) return 79;
                return 0;
            }

            return 0;
        }

        // --- CONVERSION LOGIC ---

        // Helper 1: Returns the formatted string for the UI (e.g., "1.25", "A-", "88.5%")
        public string FormatGrade(double rawPercentage, GradeScaleType scale)
        {
            return scale switch
            {
                GradeScaleType.GWA => ConvertToGWA(rawPercentage).ToString("0.00"),
                GradeScaleType.GPA => ConvertToGPA(rawPercentage).ToString("0.00"),
                GradeScaleType.Letter => ConvertToLetter(rawPercentage),
                GradeScaleType.Percentage => rawPercentage.ToString("0.00") + "%",
                _ => rawPercentage.ToString("0.00") + "%"
            };
        }

        // Helper 2: Returns the actual number needed for Term Math
        public double GetNumericalGradePoint(double rawPercentage, GradeScaleType scale)
        {
            if (scale == GradeScaleType.GWA) return ConvertToGWA(rawPercentage);
            if (scale == GradeScaleType.GPA) return ConvertToGPA(rawPercentage);
            return rawPercentage; // Default fallback
        }

        // --- CONVERSION METHODS ---
        private double ConvertToGPA(double rawGrade)
        {
            if (rawGrade >= 97) return 4.00; // Adjusted max GPA to standard 4.0
            if (rawGrade >= 94) return 3.00;
            if (rawGrade >= 91) return 2.75;
            if (rawGrade >= 88) return 2.50;
            if (rawGrade >= 85) return 1.75;
            if (rawGrade >= 82) return 1.50;
            if (rawGrade >= 79) return 1.25;
            return 0.00;
        }

        private double ConvertToGWA(double rawGrade)
        {
            if (rawGrade >= 98) return 1.00;
            if (rawGrade >= 94) return 1.25;
            if (rawGrade >= 91) return 1.50;
            if (rawGrade >= 88) return 1.75;
            if (rawGrade >= 85) return 2.00;
            if (rawGrade >= 82) return 2.25;
            if (rawGrade >= 79) return 2.50;
            if (rawGrade >= 76) return 2.75;
            if (rawGrade >= 75) return 3.00;
            return 5.00;
        }

        private string ConvertToLetter(double rawGrade) 
        {
            if (rawGrade >= 97) return "A+";
            if (rawGrade >= 94) return "A";
            if (rawGrade >= 91) return "A-";
            if (rawGrade >= 88) return "B+";
            if (rawGrade >= 85) return "B";
            if (rawGrade >= 82) return "B-";
            if (rawGrade >= 79) return "C+";
            if (rawGrade >= 76) return "C";
            if (rawGrade >= 75) return "C-";
            return "F";
        }
    }
}