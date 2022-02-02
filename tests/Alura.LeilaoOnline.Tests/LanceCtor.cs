using System;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            // Given
            var valorNegativo = -100d;
            
            // Then
            Assert.Throws<ArgumentException>(
                // When
                () => new Lance(null, valorNegativo)
            );
        }
    }
}