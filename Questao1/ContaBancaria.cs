using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public int Numero { get; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int numero, string titular, double depositoInicial = 0)
        {
            Numero = numero;
            Titular = titular;
            Saldo = depositoInicial;
        }

        public void Deposito(double quantia) => Saldo += quantia;

        public void Saque(double quantia)
        {
            if (Saldo >= quantia) {
                Saldo -= quantia + 3.5;
            }
            else {
                throw new Exception("Saldo insuficiente.");
            }
        }

        public override string ToString() {
            return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo:F2}";
        }
    }
}
