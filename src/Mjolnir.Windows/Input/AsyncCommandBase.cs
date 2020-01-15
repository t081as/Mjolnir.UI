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
using System.Windows.Input;

namespace Mjolnir.Windows.Input
{
    /// <summary>
    /// Defines the abstract base class for asynchronous commands.
    /// </summary>
    public abstract class AsyncCommandBase : IAsyncCommand
    {
        /// <summary>
        /// The handler taking care of exceptions occuring during the asynchronous operation.
        /// </summary>
        private IExceptionHandler? handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommandBase"/> class.
        /// </summary>
        /// <param name="handler">The handler taking care of exceptions occuring during the asynchronous operation.</param>
        public AsyncCommandBase(IExceptionHandler? handler)
        {
            this.handler = handler;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// The data used by the command. If the command does not require
        /// data to be passed, this object can be set to <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if this command can be executed; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool CanExecute(object? parameter);

        /// <inheritdoc/>
        public abstract Task ExecuteAsync(object? parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// The data used by the command. If the command does not require data to be passed, this object can be set to <c>null</c>.
        /// </param>
        public void Execute(object? parameter)
        {
            this.ExecuteAsync(parameter).Invoke(this.handler);
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        protected static void OnCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}