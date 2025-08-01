﻿using System;
using FluentAssertions;
using NUnit.Framework;
using Reflex.Core;
using Reflex.Exceptions;

namespace Reflex.EditModeTests
{
    public class ExtraInstallerScopeTests
    {
        [Test]
        public void ExtraInstallerScope_WillInstallOnAnyNewContainer_WhileItsNotDisposed()
        {
            using (new ExtraInstallerScope(b => b.AddSingleton(instance: 95)))
            {
                var containerOneWithExtraInstaller = new ContainerBuilder().Build();
                containerOneWithExtraInstaller.Single<int>().Should().Be(95);

                var containerTwoWithExtraInstaller = new ContainerBuilder().Build();
                containerTwoWithExtraInstaller.Single<int>().Should().Be(95);
            }

            var containerWithoutExtraInstaller = new ContainerBuilder().Build();
            Action resolveFromContainerWithoutExtraInstaller = () => containerWithoutExtraInstaller.Single<int>();
            resolveFromContainerWithoutExtraInstaller.Should().ThrowExactly<UnknownContractException>();
        }
    }
}