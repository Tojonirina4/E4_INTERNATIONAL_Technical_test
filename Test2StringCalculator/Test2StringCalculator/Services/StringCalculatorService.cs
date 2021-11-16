﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2StringCalculator.Services
{
    public class StringCalculatorService
    {
        public int Add(string stringToCompute)
        {
            if (string.IsNullOrEmpty(stringToCompute)) return 0;
            return int.Parse(stringToCompute);
        }
    }
}
