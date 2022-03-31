using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filazor.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filazor.Core.Data.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void GetDirectoryInfosTest()
        {
            var service = new FileSystemService();

            var dirInfos = service.GetDirectoryInfos(@"C:\");
            Assert.IsNull(dirInfos, $"wow: {dirInfos.Result[0].Name}");
        }
    }
}