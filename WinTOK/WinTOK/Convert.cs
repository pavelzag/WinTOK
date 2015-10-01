using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace WinTOK
{
    class Convert
    {
        internal static void ConvertToAAC(string sFile, string sPath)
        {
            using (var reader = new MediaFoundationReader(sFile))
            {
                MediaFoundationEncoder.EncodeToAac(reader, sPath + "hi.m4a");
            }
        }
    }
}
