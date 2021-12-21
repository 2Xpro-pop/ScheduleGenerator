using System;
using System.Collections.Generic;

using Accord.Genetic;

namespace ScheduleGenerator.Genetic
{
    using Models;
    public class ScheduleFittness : IFitnessFunction
    {
        public ScheduleOptions Options { get; }
        public const int eps = 1000;
        public ScheduleFittness(ScheduleOptions options)
        {

        }

        public double Evaluate(IChromosome chromosome)
        {
            ClearAllConflicts();

            Span<ushort> shortArray = new Span<ushort>(((ShortArrayChromosome)chromosome).Value);
            double result = 0;

            for(int i=0; i < Options.groups.Count; i++)
            {
                result += EvaluateSchedule(shortArray.Slice(i * 48, 48), Options.groups[i]);
            }
            result /= Options.groups.Count;

            return result;
        }

        private double EvaluateSchedule(Span<ushort> schedule, Group group)
        {
            var badClocks = 0d;
            var badTimes = 0d;
            var extraLesson = 0d;
            var conflicts = 0d;
            var needLessons = new Dictionary<string,int>(group.NeedLessons);

            for(int i=0; i < 48; i++)
            {
                if(schedule[i] < Options.teachers.Count)
                {
                    var teacher = Options.teachers[schedule[i]];
                    if(teacher.BadTimes.Contains(i))
                    {
                        badTimes+=1;
                    }
                    if(teacher.Conflicts.Contains(i))
                    {
                        conflicts +=1;
                    }
                    else
                    {
                        teacher.Conflicts.Add(i);
                    }
                    if(needLessons.ContainsKey(teacher.Lesson))
                    {
                        needLessons[teacher.Lesson] -= 1;
                    }
                    else
                    {
                        extraLesson += 1;
                    }
                    if(group.BadClock.Contains(i % 8))
                    {
                        badClocks += 1;
                    }
                }
            }

            foreach(int value in needLessons.Values)
            {
                extraLesson += Math.Abs(value);
            }

            badClocks = BetweenZeroAndOne(badClocks, 48);
            badTimes = BetweenZeroAndOne(badTimes, 48);
            extraLesson = BetweenZeroAndOne(extraLesson, 48);
            conflicts = BetweenZeroAndOne(conflicts, 48);

            //Console.WriteLine($"badClocks - {badClocks}");
            //Console.WriteLine($"badTimes - {badTimes}");
            //Console.WriteLine($"extraLesson - {extraLesson}");
            //Console.WriteLine($"conflicts - {conflicts}");

            return Activation(
                (badClocks+badTimes+extraLesson+conflicts)/4, 
                eps
            );
        }

        private void ClearAllConflicts()
        {
            foreach(var teacher in Options.teachers)
            {
                teacher.Conflicts.Clear();
            }
        }

        private static double Activation(double value, int eps)
        {
            return Math.Floor(value*eps)/eps;
        }

        private static double BetweenZeroAndOne(double value, double divider)
        {
            return (divider-value)/divider;
        }
    }
}