// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Help{T}.cs" company="Appccelerate">
//   Copyright (c) 2008-2018 Appccelerate
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Appccelerate.CommandLineParser.Help
{
    using System.Collections.Generic;
    using System.Linq;

    using Appccelerate.CommandLineParser.Arguments;

    public abstract class Help<T> : Help
        where T : Argument
    {
        protected Help(T argument)
            : base(argument)
        {
            this.Argument = argument;
            this.Description = string.Empty;
        }

        public string Description { get; set; }

        protected new T Argument { get; private set; }

        protected string GetAliasPart(IEnumerable<string> longAliases)
        {
            string aliases = string.Join(
                ", ",
                longAliases.Select(x => "--" + x));

            return aliases != string.Empty ? " (" + aliases + ")" : string.Empty;
        }
    }
}