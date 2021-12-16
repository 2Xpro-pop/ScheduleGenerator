using System;
using System.Runtime;
using System.Runtime.InteropServices;
using ScheduleGenerator.Models;

namespace ScheduleGenerator.Traditions
{
    public partial class CImplementTradition: ITradition
    {

        #region Windows

        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern int LoadLibrary(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern IntPtr GetProcAddress( int hModule,
            [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        static extern bool FreeLibrary(int hModule);

        #endregion
        private TraditionMetaInfo _metaInfo;
        private int _hModule;

        public void Load(string path)
        {
            _hModule = LoadLibrary(path);
            if(_hModule == 0)
            {
                throw new NotSupportedException();
            }
        }

        public TraditionMetaInfo GetMetaInfo()
        {
            if(_metaInfo == null)
            {
                IntPtr ptr = GetProcAddress(_hModule, "get_meta_info");
                var getter = Marshal.GetDelegateForFunctionPointer<GetMeta>(ptr);
            }

            return _metaInfo;
        }

        public TraditionMarkup GetMarkup()
        {
            throw new NotImplementedException();
        }

        public bool SaveOptions(string[] values)
        {
            throw new NotImplementedException();
        }

        ~CImplementTradition()
        {
            if(_hModule !=0)
            {
              FreeLibrary(_hModule);
            }
        }

    }
}