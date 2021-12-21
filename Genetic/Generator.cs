using System;
using System.Collections;

using Accord.Genetic;

namespace ScheduleGenerator.Genetic
{
    public class Generator:Population
    {
        public Generator(
            int size, 
            IChromosome ancestor, 
            IFitnessFunction fitnessFunction, 
            ISelectionMethod selectionMethod):base(size, ancestor, fitnessFunction, selectionMethod)
        {

        }

        public static Generator Build(ScheduleOptions scheduleOptions, int size = 20)
        {
            var generator = new Generator(
                size, 
                new ShortArrayChromosome(scheduleOptions.groups.Count*48, scheduleOptions.teachers.Count+10), 
                new ScheduleFittness(scheduleOptions),
                new EliteSelection()
            ); 

            return generator;
        }
    }
}