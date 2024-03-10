﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zurvan.Core.TimeModels
{
    public class DateTimeData
    {
        public string Date { get; set; }
        public int TimeUsed { get; set; }

        public DateTimeData(string date, int timeUsed)
        {
            Date = date;
            TimeUsed = timeUsed;

        }

    }
}