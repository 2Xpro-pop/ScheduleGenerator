using System;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace ScheduleGenerator.Genetic
{
    using Models;

    public class ScheduleOptions
    {
        public ImmutableList<Group> groups;
        public ImmutableList<Teacher> teachers; 

        public ScheduleOptions(IEnumerable<Group> groups, IEnumerable<Teacher> teachers)
        {
            this.groups = ImmutableList<Group>.Empty.AddRange(groups);
            this.teachers = ImmutableList<Teacher>.Empty.AddRange(teachers);
        }
    }
}