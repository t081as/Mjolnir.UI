// The MIT License (MIT)
//
// Copyright © 2017-2020 Tobias Koch
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the “Software”), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mjolnir.Windows.Input;

namespace Mjolnir.Windows.Tests.Input
{
    /// <summary>
    /// Contains unit tests for the <see cref="RelayCommand"/> class.
    /// </summary>
    [TestClass]

    public class RelayCommandTests
    {
        /// <summary>
        /// Checks the <see cref="RelayCommand.Execute(object)"/> method.
        /// </summary>
        [TestMethod]
        public void ExecuteTest()
        {
            var success = false;
            var command = new RelayCommand((parameter) => success = true, () => true);
            command.Execute(null);

            Assert.IsTrue(success);
        }

        /// <summary>
        /// Checks the <see cref="RelayCommand.CanExecute(object)"/> method.
        /// </summary>
        [TestMethod]
        public void CanExecuteTest()
        {
            var command1 = new RelayCommand((parameter) => { }, () => true);
            Assert.IsTrue(command1.CanExecute(null));

            var command2 = new RelayCommand((parameter) => { });
            Assert.IsTrue(command2.CanExecute(null));

            var command3 = new RelayCommand((parameter) => { }, null);
            Assert.IsTrue(command3.CanExecute(null));

            var command4 = new RelayCommand((parameter) => { }, () => false);
            Assert.IsFalse(command4.CanExecute(null));
        }
    }
}
