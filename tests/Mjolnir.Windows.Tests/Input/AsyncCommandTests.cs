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

using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mjolnir.Windows.Input;

namespace Mjolnir.Windows.Tests.Input
{
    /// <summary>
    /// Contains unit tests for the <see cref="AsyncCommand"/> class.
    /// </summary>
    [TestClass]
    public class AsyncCommandTests
    {
        /// <summary>
        /// Indicates the success of the asynchronous call.
        /// </summary>
        private bool success = false;

        /// <summary>
        /// Checks the <see cref="RelayCommand.Execute(object)"/> method.
        /// </summary>
        [TestMethod]
        public void ExecuteTest()
        {
            this.success = false;
            var command = new AsyncCommand((parameter) => this.SetSuccess(), null, null);
            command.Execute(null);

            Assert.IsTrue(this.success);
        }

        /// <summary>
        /// Checks the <see cref="AsyncCommand.CanExecute(object)"/> method.
        /// </summary>
        [TestMethod]
        public void CanExecuteTest()
        {
            var command1 = new AsyncCommand((parameter) => this.SetSuccess(), (parameter) => true);
            Assert.IsTrue(command1.CanExecute(null));

            var command2 = new AsyncCommand((parameter) => this.SetSuccess());
            Assert.IsTrue(command2.CanExecute(null));

            var command3 = new AsyncCommand((parameter) => this.SetSuccess(), null);
            Assert.IsTrue(command3.CanExecute(null));

            var command4 = new AsyncCommand((parameter) => this.SetSuccess(), (parameter) => false);
            Assert.IsFalse(command4.CanExecute(null));
        }

        /// <summary>
        /// An asynchronous method setting the <see cref="success"/> to <c>true</c>.
        /// </summary>
        /// <returns>A <see cref="Task"/> describing the asynchronous operation.</returns>
        private async Task SetSuccess()
        {
            this.success = true;
            await Task.Delay(100).ConfigureAwait(false);
        }
    }
}
