using Core.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Core.Entities
{
    [StructLayout(LayoutKind.Sequential, Size = 96)]
    unsafe public struct LocationFixed
    {
        public fixed char country[8];    // название страны (случайная строка с префиксом "cou_")  [8]; 

        public fixed char region[12];         // название области (случайная строка с префиксом "reg_") [12];

        public fixed char postal[12];      // почтовый индекс (случайная строка с префиксом "pos_") [12]; 

        public fixed char city[24];        // название города (случайная строка с префиксом "cit_") [24]; 

        public fixed char organization[32];  // название организации (случайная строка с префиксом "org_") [32];

        public fixed float latitude[4];     // широта

        public fixed float longitude[4];   // долгота

    }

}


