using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            LeilaoComVariosLances();
            LeilaoComApenasUmLance();
            
        }

        private static void Verifica(double valorEsperado, double valorObtido)
        {
            if (valorEsperado.Equals(valorObtido))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Teste OK"!);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Teste Falhou!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void LeilaoComApenasUmLance()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);

            leilao.TerminaPregao();

            var valorEsperado = 800.0;
            var valorObtido = leilao.Ganhador.Valor;
            Verifica(valorEsperado, valorObtido);
        }

        private static void LeilaoComVariosLances()
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            leilao.TerminaPregao();

            var valorEsperado = 1000.0;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado,valorObtido);
        }
    }
}