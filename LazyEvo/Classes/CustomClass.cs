﻿namespace LazyEvo.Classes
{
    using System;
    using System.Runtime.CompilerServices;

    internal class CustomClass
    {
        public CustomClass(string assemblyName, string assemblyFunctionName)
        {
            this.AssemblyName = assemblyName;
            this.AssemblyFunctionName = assemblyFunctionName;
        }

        public override string ToString() => 
            this.AssemblyFunctionName;

        public string AssemblyName { get; private set; }

        public string AssemblyFunctionName { get; private set; }
    }
}

