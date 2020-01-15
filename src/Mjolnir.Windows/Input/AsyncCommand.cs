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

using System;
using System.Threading.Tasks;

namespace Mjolnir.Windows.Input
{
    /// <summary>
    /// Defines an asynchronous command.
    /// </summary>
    public class AsyncCommand : AsyncCommandBase
    {
        /// <summary>
        /// The method that determines whether the command can execute in its current state.
        /// </summary>
        private Func<object?, Task> execute;

        /// <summary>
        /// The method to be called when the command is invoked.
        /// </summary>
        private Func<object?, bool>? canExecute;

        /// <summary>
        /// A flag indicating whether the method is currently executed.
        /// </summary>
        private bool isExecuting;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommand"/> class.
        /// </summary>
        /// <param name="execute">The method to be called when the command is invoked.</param>
        /// <param name="canExecute">A flag indicating whether the method is currently executed.</param>
        /// <param name="handler">The handler taking care of exceptions occuring during the asynchronous operation.</param>
        public AsyncCommand(
            Func<object?, Task> execute,
            Func<object?, bool>? canExecute = null,
            IExceptionHandler? handler = null)
            : base(handler)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object? parameter)
        {
            return !this.isExecuting && (this.canExecute?.Invoke(parameter) ?? true);
        }

        /// <inheritdoc/>
        public async override Task ExecuteAsync(object? parameter)
        {
            if (this.CanExecute(parameter))
            {
                try
                {
                    this.isExecuting = true;
                    await this.execute(parameter).ConfigureAwait(false);
                }
                finally
                {
                    this.isExecuting = false;
                }

                OnCanExecuteChanged();
            }
        }
    }
}
