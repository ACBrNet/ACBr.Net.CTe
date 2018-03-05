using System;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using ACBr.Net.CTe;

namespace ACbr.Net.CTe.Demo.NetCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var acbrCte = new ACBrCTe();
            acbrCte.Configuracoes.Certificados.Certificado = "certificado.pfx";
            acbrCte.Configuracoes.Certificados.Senha = "1234";
            var ret = acbrCte.ConsultarSituacaoServico();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine(ret.XmlEnvio);
            Console.WriteLine(new string('=', 50));
            Console.WriteLine(ret.XmlRetorno);

            Console.ReadKey();
        }
    }
}