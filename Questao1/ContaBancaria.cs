using System.Globalization;

namespace Questao1
{
    public class ContaBancaria
    {
        // Campos privados
        private readonly int numero;
        private string titular;
        private double saldo;

        // Construtor
        public ContaBancaria(int numero, string titular, double saldo = 0.0)
        {
            this.numero = numero;
            this.titular = titular;
            this.saldo = saldo;
        }

        // Propriedades somente leitura
        public int Numero => numero;
        public double Saldo => saldo;

        // Propriedade com set para o titular
        public string Titular
        {
            get { return titular; }
            set { titular = value; }
        }

        // Métodos públicos
        public void Deposito(double valor)
        {
            saldo += valor;
        }

        public void Saque(double valor)
        {
            double valorComTaxa = valor + 3.50;

            saldo -= valorComTaxa;
        }

        public override string ToString()
        {
            return $"Conta {numero}, Titular: {titular}, Saldo: {saldo:C}";
        }
    }
}
