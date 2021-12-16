using System;
using System.Runtime;
using System.Runtime.InteropServices;
using ScheduleGenerator.Models;

namespace ScheduleGenerator.Traditions
{
    public partial class CImplementTradition: ITradition
    {
        [StructLayout(LayoutKind.Sequential)]
        struct MetaInfo
        {
            string name;
            string description;
            string unique_Id; 
        }
    }
}