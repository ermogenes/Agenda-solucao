﻿using System;
using System.Linq;
using Agenda.db;

namespace Agenda
{
    class Program
    {
        static void Main(string[] args)
        {
            bool sair = false;
            while (!sair)
            {
                string opcao = SelecionaOpcaoEmMenu();

                Console.WriteLine($"Opção selecionada: {opcao}\n");

                switch (opcao)
                {
                    case "L":
                        ListarTodosContatos();
                        break;

                    case "T":
                        Top5Contatos();
                        break;

                    case "C":
                        ConsultarContatosPorCodigo();
                        break;

                    case "N":
                        ConsultarContatosPorNome();
                        break;

                    case "I":
                        IncluirContato();
                        break;

                    case "S":
                        Console.WriteLine("- Sair");
                        sair = true;
                        break;

                    default:
                        Console.WriteLine($"Opção não reconhecida.");
                        break;
                }

                Console.Write("\nPressione uma tecla para continuar...");
                Console.ReadKey();
            }
        }

        static string SelecionaOpcaoEmMenu()
        {
            Console.Clear();
            Console.WriteLine("-- Agenda --\n");
            Console.WriteLine("[L]istar todos os contatos");
            Console.WriteLine("[T]op 5 contatos");
            Console.WriteLine("Consultar contatos por [C]ódigo");
            Console.WriteLine("Consultar contatos por [N]ome");
            Console.WriteLine("[I]ncluir contato");
            Console.WriteLine("[S]air");
            Console.Write("\nDigite a sua opção: ");

            string entrada = Console.ReadLine().ToUpper().Trim();
            return entrada.Length < 2 ? entrada : entrada.Substring(0, 1);
        }

        static void ListarTodosContatos()
        {
            Console.WriteLine("- Listar todos os contatos:");

            using (var agenda = new agendaContext())
            {
                int qtdDeContatos = agenda.Contatos.Count();

                if (qtdDeContatos == 0)
                {
                    Console.WriteLine("Nenhum contato cadastrado.");
                    return;
                }

                Console.WriteLine($"{qtdDeContatos} contato(s) cadastrado(s):");

                foreach (var contato in agenda.Contatos)
                {
                    Console.WriteLine($"{contato.Id}: {contato.Nome}, {contato.Fone}, {contato.Estrelas} estrelas.");
                }
            }
        }

        static void Top5Contatos()
        {
            Console.WriteLine("- Top 5 contatos:");

            using (var agenda = new agendaContext())
            {
                int qtdDeContatos = agenda.Contatos.Count();

                if (qtdDeContatos == 0)
                {
                    Console.WriteLine("Nenhum contato cadastrado.");
                    return;
                }

                Console.WriteLine($"{qtdDeContatos} contato(s) cadastrado(s):");

                var top5Contatos = agenda.Contatos
                    .OrderByDescending(c => c.Estrelas)
                    .Take(5);

                int posicao = 0;
                foreach (var contato in top5Contatos)
                {
                    posicao += 1;
                    Console.WriteLine($"#{posicao} = {contato.Id}: {contato.Nome}, {contato.Fone}, {contato.Estrelas} estrelas.");
                }
            }
        }

        static void ConsultarContatosPorCodigo()
        {
            Console.WriteLine("- Consultar contatos por Código:");

            Console.Write("Código: ");
            string codigoDigitado = Console.ReadLine();

            int codigoABuscar;
            bool ehNumero = Int32.TryParse(codigoDigitado, out codigoABuscar);

            if (!ehNumero)
            {
                Console.WriteLine("Código numérico inválido.");
                return;
            }

            using (var agenda = new agendaContext())
            {
                var contato = agenda.Contatos
                    .SingleOrDefault(c => c.Id == codigoABuscar);

                if (contato is null)
                {
                    Console.WriteLine($"Nenhum contato com código {codigoABuscar} encontrado.");
                }
                else
                {
                    Console.WriteLine($"{contato.Id}: {contato.Nome}, {contato.Fone}, {contato.Estrelas} estrelas.");
                }
            }
        }

        static void ConsultarContatosPorNome()
        {
            Console.WriteLine("- Consultar contatos por Nome:");

            // Continue daqui
        }

        static void IncluirContato()
        {
            Console.WriteLine("- Incluir contato:");

            // Continue daqui
        }
    }
}
