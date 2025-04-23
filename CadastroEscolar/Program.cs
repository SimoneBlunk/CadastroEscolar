using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Aluno
{
    public string Nome { get; set; }
    public Dictionary<string, List<double>> NotasPorDisciplina { get; set; } = new Dictionary<string, List<double>>();
}

class Programa
{
    static List<Aluno> alunos = new List<Aluno>();
    static List<string> disciplinas = new List<string>();

    static void Main()
    {
        bool executando = true;

        while (executando)
        {
            Console.Clear();
            Console.WriteLine("=== Sistema Escolar ===");
            Console.WriteLine("1. Cadastrar aluno");
            Console.WriteLine("2. Cadastrar disciplina");
            Console.WriteLine("3. Adicionar nota");
            Console.WriteLine("4. Ver boletim");
            Console.WriteLine("5. Exportar boletim");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarAluno();
                    break;
                case "2":
                    CadastrarDisciplina();
                    break;
                case "3":
                    AdicionarNota();
                    break;
                case "4":
                    VerBoletim();
                    break;
                case "5":
                    ExportarBoletim();
                    break;
                case "0":
                    executando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }
    }

    static void CadastrarAluno()
    {
        Console.Write("Nome do aluno: ");
        string nome = Console.ReadLine();
        alunos.Add(new Aluno { Nome = nome });
        Console.WriteLine("Aluno cadastrado com sucesso!");
    }

    static void CadastrarDisciplina()
    {
        Console.Write("Nome da disciplina: ");
        string nome = Console.ReadLine();
        disciplinas.Add(nome);
        Console.WriteLine("Disciplina cadastrada com sucesso!");
    }

    static void AdicionarNota()
    {
        Console.Write("Nome do aluno: ");
        string nomeAluno = Console.ReadLine();
        Aluno aluno = alunos.FirstOrDefault(a => a.Nome.Equals(nomeAluno, StringComparison.OrdinalIgnoreCase));

        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado.");
            return;
        }

        Console.WriteLine("Disciplinas disponíveis:");
        foreach (var d in disciplinas) Console.WriteLine($"- {d}");
        Console.Write("Escolha a disciplina: ");
        string disciplina = Console.ReadLine();

        if (!disciplinas.Contains(disciplina))
        {
            Console.WriteLine("Disciplina não encontrada.");
            return;
        }

        Console.Write("Nota: ");
        if (double.TryParse(Console.ReadLine(), out double nota))
        {
            if (!aluno.NotasPorDisciplina.ContainsKey(disciplina))
                aluno.NotasPorDisciplina[disciplina] = new List<double>();

            aluno.NotasPorDisciplina[disciplina].Add(nota);
            Console.WriteLine("Nota adicionada com sucesso!");
        }
        else
        {
            Console.WriteLine("Nota inválida.");
        }
    }

    static void VerBoletim()
    {
        Console.Write("Nome do aluno: ");
        string nomeAluno = Console.ReadLine();
        Aluno aluno = alunos.FirstOrDefault(a => a.Nome.Equals(nomeAluno, StringComparison.OrdinalIgnoreCase));

        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado.");
            return;
        }

        Console.WriteLine($"\n--- Boletim de {aluno.Nome} ---");
        foreach (var item in aluno.NotasPorDisciplina)
        {
            var media = item.Value.Average();
            string status = media >= 7 ? "Aprovado" : "Reprovado";
            Console.WriteLine($"{item.Key}: Média = {media:F2} | {status}");
        }
    }

    static void ExportarBoletim()
    {
        Console.Write("Nome do aluno: ");
        string nomeAluno = Console.ReadLine();
        Aluno aluno = alunos.FirstOrDefault(a => a.Nome.Equals(nomeAluno, StringComparison.OrdinalIgnoreCase));

        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado.");
            return;
        }

        string caminho = $"{aluno.Nome}_Boletim.txt";
        using (StreamWriter writer = new StreamWriter(caminho))
        {
            writer.WriteLine($"Boletim de {aluno.Nome}");
            writer.WriteLine("========================");

            foreach (var item in aluno.NotasPorDisciplina)
            {
                var media = item.Value.Average();
                string status = media >= 7 ? "Aprovado" : "Reprovado";
                writer.WriteLine($"{item.Key}: Média = {media:F2} | {status}");
            }
        }

        Console.WriteLine($"Boletim exportado para: {caminho}");
    }
}
