using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, new double[] {800, 900, 1000, 1200})]
        [InlineData(1000, new double[] {800, 900, 1000, 990})]
        [InlineData(800, new double[] {800})]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmValor(double valorEsperado, double[] ofertas)
        {
            //Arranje
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();
            
            for (int i = 0; i < ofertas.Length; i++)
            {
                if ((i%2) == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }
            
            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado,valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            // Given
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
        
            // Assert
            var excecaoObtida = Assert.Throws<System.InvalidOperationException>(
                // When
                () => leilao.TerminaPregao()
            );

            var msgEsperado = "Não é possivel terminar o pregão sem iniciar o mesmo, utilize o método IniciaPregao() para inicia-lo!";
            Assert.Equal(msgEsperado, excecaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado,valorObtido);
        }
    
        [Theory]
        [InlineData(1200, 1250, new double[]{800, 1150, 1400, 1250})]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] ofertas)
        {
            // Given
            var modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i%2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            // When
            leilao.TerminaPregao();

            // Then
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);

        }
    }
}