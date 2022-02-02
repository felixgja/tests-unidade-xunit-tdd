using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(2, new double[]{800, 900})]
        [InlineData(3, new double[]{800, 900, 1200})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeLanceEsperado, double[] ofertas)
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
            
            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(fulano, 1000);

            //Assert

            var qtdeLanceObtido = leilao.Lances.Count();

            Assert.Equal(qtdeLanceEsperado, qtdeLanceObtido);
        }    
    
        [Fact]
        public void NaoAceitaLanceCasoUltimoLanceSejaMesmoInteressado()
        {
            // Given
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano,800);
        
            // When
            leilao.RecebeLance(fulano,900);

            // Then

            var qtdeLanceEsperado = 1;
            var qtdeLanceObtido = leilao.Lances.Count();
            Assert.Equal(qtdeLanceEsperado, qtdeLanceObtido);
        }
    }
}