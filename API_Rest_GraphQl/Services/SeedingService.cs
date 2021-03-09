using API_Rest_GraphQl.Models;
using API_Rest_GraphQl.Models.Context;
using API_Rest_GraphQl.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest_GraphQl.Services
{
    public class SeedingService : ISeedingService
    {
        private readonly BibliotecaContext _context;

        public SeedingService()
        {

        }

        public SeedingService(BibliotecaContext context)
        {
            _context = context;
        }

        public void ObterLivros()
        {
            try
            {
                if (_context.Livros.Any())
                    return;

                List<Livro> livros = new List<Livro>();
                livros.Add(new Livro()
                {
                    Id = 1,
                    Nome = "Poderoso Chefão",
                    Autor = "Mario Puzo",
                    Lido = true
                });

                livros.Add(new Livro()
                {
                    Id = 2,
                    Nome = "O Alquimista",
                    Autor = "Paulo Coelho",
                    Lido = false
                });

                livros.Add(new Livro()
                {
                    Id = 3,
                    Nome = "Revolução dos Bichos",
                    Autor = "George Orwell",
                    Lido = true
                });

                livros.Add(new Livro()
                {
                    Id = 4,
                    Nome = "O Segundo Sexo",
                    Autor = "Simone de Beauvoir",
                    Lido = false
                });

                livros.Add(new Livro()
                {
                    Id = 5,
                    Nome = "Memórias Póstumas de Brás Cubas",
                    Autor = "Machado de Assis",
                    Lido = false
                });

                livros.Add(new Livro()
                {
                    Id = 6,
                    Nome = "Crepúsculo",
                    Autor = "Stephanie Meyer",
                    Lido = false
                });

                livros.Add(new Livro()
                {
                    Id = 7,
                    Nome = "Drácula de Bram Stoker",
                    Autor = "Bram Stoker",
                    Lido = true
                });

                livros.Add(new Livro()
                {
                    Id = 8,
                    Nome = "O Corvo",
                    Autor = "Edgar Allan Poe",
                    Lido = false
                });

                livros.Add(new Livro()
                {
                    Id = 9,
                    Nome = "Grande Sertão Veredas",
                    Autor = "GUimarães Rosa",
                    Lido = false
                });

                livros.Add(new Livro()
                {
                    Id = 10,
                    Nome = "Fahrenreit 451",
                    Autor = "Ray Bradbury",
                    Lido = true
                });

                _context.Livros.AddRange(livros);
                _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
