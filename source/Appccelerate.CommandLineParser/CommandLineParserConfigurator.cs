﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineParserConfigurator.cs" company="Appccelerate">
//   Copyright (c) 2008-2015
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

namespace Appccelerate.CommandLineParser
{
    using System;
    using System.Collections.Generic;

    public class CommandLineParserConfigurator
    {
        private readonly List<NamedArgument> named = new List<NamedArgument>();
        private readonly List<UnnamedArgument> unnamed = new List<UnnamedArgument>();
        private readonly List<Switch> switches = new List<Switch>();
        private readonly List<Argument> required = new List<Argument>();
        private readonly Dictionary<string, Argument> longAliases = new Dictionary<string, Argument>();

        private Argument current;

        public static CommandLineParserConfigurator Create()
        {
            return new CommandLineParserConfigurator();
        }

        public CommandLineParserConfigurator WithUnnamed(Action<string> callback)
        {
            var unnamedArgument = new UnnamedArgument(callback);
            this.unnamed.Add(unnamedArgument);

            this.current = unnamedArgument;

            return this;
        }
        
        public CommandLineParserConfigurator WithNamed(string name, Action<string> callback)
        {
            var namedArgument = new NamedArgument(name, callback);
            this.named.Add(namedArgument);

            this.current = namedArgument;

            return this;
        }

        public CommandLineParserConfigurator HavingLongAlias(string longAlias)
        {
            this.longAliases.Add(longAlias, this.current);

            return this;    
        }

        public CommandLineParserConfigurator WithSwitch(string name, Action callback)
        {
            var argument = new Switch(name, callback);

            this.switches.Add(argument);

            this.current = argument;

            return this;
        }

        public CommandLineConfiguration BuildConfiguration()
        {
            return new CommandLineConfiguration(
                this.named, 
                this.unnamed, 
                this.switches, 
                this.required,
                this.longAliases);
        }

        public CommandLineParserConfigurator Required()
        {
            this.required.Add(this.current);

            return this;
        }
    }
}