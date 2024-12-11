using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_App
{
    class CaloriesBurned
    {
        // Constants for the calculation
        private const double oxygenConsumption = 3.5; // 3.5 ml/kg/min at rest
        private const double oxygenConversion = 200;  // Conversion factor to calories

        // MET values for different workout intensities
        private const double easyMET = 3.5;
        private const double moderateMET = 5;
        private const double vigorousMET = 6;

        //Formuala
        // Calories burned = workoutDuration(minutes) * (MET * oxygenConsumption * bodyWeight(kgs) / oxygenConversion

        double workoutDuration; //in minutes
        double bodyWeight; //kgs
        public double BurnedCalories(double workOutDuration, double bodyweight, int MET) //had to chage method to return a value for WPF 
        {
            double lbsToKgs = bodyweight * 0.45359237;

            double caloriesBurned1 = (workOutDuration * (easyMET * oxygenConsumption * Math.Round(lbsToKgs, 2))) / 200;
            double caloriesBurned2 = (workOutDuration * (moderateMET * oxygenConsumption * Math.Round(lbsToKgs, 2))) / 200;
            double caloriesBurned3 = (workOutDuration * (vigorousMET * oxygenConsumption * Math.Round(lbsToKgs, 2))) / 200;


            double caloriesBurned = 0;
            switch (MET)
            {
                case 1: // Easy intensity
                    caloriesBurned = (Math.Round(caloriesBurned1));
                    break;
                case 2: // Moderate intensity
                    caloriesBurned = (Math.Round(caloriesBurned2));
                    break;
                case 3: // Vigorous intensity
                    caloriesBurned = (Math.Round(caloriesBurned3));
                    break;
                default:
                    throw new ArgumentException("Invalid MET value. Use 1 for easy, 2 for moderate, or 3 for vigorous.");
            }

            return caloriesBurned; // Return the calculated value

        }
    }
}
