﻿using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.Service.Mappings;

namespace Sample.ServiceTests
{
    [TestClass]
    public class TestHooks
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Mapper.Initialize
            (
                cfg =>
                {
                    cfg.AddProfile<ServiceMappingProfile>();
                }
            );
        }
    }
} 