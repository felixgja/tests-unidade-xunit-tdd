namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado {get; private set; }
        public IModalidadeAvaliacao Modalidade { get; }
        
        public Leilao(string peca, IModalidadeAvaliacao modalidade)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            Modalidade = modalidade;
        }

        private bool LanceEhAceito(Interessada cliente, double valor)
        {
            return Estado == EstadoLeilao.LeilaoEmAndamento
                && cliente != _ultimoCliente;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (LanceEhAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new System.InvalidOperationException("Não é possivel terminar o pregão sem iniciar o mesmo, utilize o método IniciaPregao() para inicia-lo!");
            }

            Ganhador = Modalidade.Avalia(this);
            
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }

}