using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using Course.Entities;

namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lista para armazenar os funcionários
            List<Employee> employees = new List<Employee>();

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            Console.Write("Enter salary: ");
            double salaryLimit = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            try
            {
                // Lê todas as linhas do arquivo
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    // Divide a linha pelo separador vírgula
                    string[] fields = line.Split(',');

                    // Extrai os dados de cada campo
                    string name = fields[0];
                    string email = fields[1];
                    double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);

                    // Adiciona o funcionário à lista
                    employees.Add(new Employee(name, email, salary));
                }

                // Filtra os funcionários com salário maior que o valor informado, ordena por email e seleciona só os emails
                var emails = employees
                    .Where(e => e.Salary > salaryLimit)
                    .OrderBy(e => e.Email)
                    .Select(e => e.Email);

                Console.WriteLine("\nEmail of people whose salary is more than " + salaryLimit.ToString("F2", CultureInfo.InvariantCulture) + ":");
                foreach (string email in emails)
                {
                    Console.WriteLine(email);
                }

                // Soma os salários dos funcionários cujo nome começa com 'M'
                var sum = employees
                    .Where(e => e.Name.StartsWith("M"))
                    .Sum(e => e.Salary);

                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sum.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
