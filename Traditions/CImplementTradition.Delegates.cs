using System;
using System.Runtime;
using System.Runtime.InteropServices;
using ScheduleGenerator.Models;

namespace ScheduleGenerator.Traditions
{
    public partial class CImplementTradition: ITradition
    {
        delegate MetaInfo GetMeta();
    }
}