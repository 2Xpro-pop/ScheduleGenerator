using System;

namespace ScheduleGenerator.Traditions
{
    public class TraditonException: Exception
    {
        public ITradition Tradition { get; }
        public Exception InnerException { get; set; }
        public TraditonException(ITradition tradition, Exception innerException)
        {
            Tradition = tradition;
            InnerException = innerException;
        }

    }
}