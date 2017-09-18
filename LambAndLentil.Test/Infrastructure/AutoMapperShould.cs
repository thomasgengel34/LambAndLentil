using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using LambAndLentil.Tests.Infrastructure;

namespace LambAndLentil.Test.Infrastructure
{
    [TestClass]
    public class AutoMapperShould
    {
        public static MapperConfiguration AutoMapperConfig { get; set; }
        
       [Ignore]  // currently not using AutoMapper.  Keep if I go back.
        [TestMethod]
        public void BeConfigured()
        {
            AutoMapperConfigForTests.InitializeMap();
            var config = AutoMapperConfigForTests.AMConfigForTests(); 
            config.AssertConfigurationIsValid();  
        }
    }
}
