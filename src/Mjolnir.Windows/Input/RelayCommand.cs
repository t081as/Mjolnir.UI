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
using System.Windows.Input;

namespace Mjolnir.Windows.Input
{
    /// <summary>
    /// A basic implementation of the <see cref="ICommand"/> interface executing an <see cref="Action{T}"/>.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The <see cref="Action{T}"/> that shall be executed.
        /// </summary>
        private Action<object?> execute;

        /// <summary>
        /// Indicates whether the action can be executed.
        /// </summary>
        private Func<bool>? canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The <see cref="Action{T}"/> that shall be executed.</param>
        public RelayCommand(Action<object?> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The <see cref="Action{T}"/> that shall be executed.</param>
        /// <param name="canExecute">A <see cref="Func{TResult}"/> evaluating whether the action can be executed.</param>
        public RelayCommand(Action<object?> execute, Func<bool>? canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes to the command source are detected by the command manager. These changes
        /// often affect whether the command should execute on the current command target.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// The data used by the command. If the command does not require data to be passed,
        /// this object can be set to <c>null</c>.
        /// </param>
        public void Execute(object? parameter) => this.execute.Invoke(parameter);

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// The data used by the command. If the command does not require data to be passed,
        /// this object can be set to <c>null</c>.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object? parameter) => this.canExecute?.Invoke() ?? true;
    }
}