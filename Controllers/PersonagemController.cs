using System;
using System.Collections.Generic;
using ficha_criacao_personagem_rpg_csharp.Models;
using Microsoft.AspNetCore.Mvc;
using ficha_criacao_personagem_rpg_csharp.Repositorio;
using Dapper;
using System.Linq;


namespace ficha_criacao_personagem_rpg_csharp.Controllers
{
    public class PersonagemController : Controller
    {
        //Criar uma lista de personagem 
        private List<PersonagemViewModel> listPersonagem;
        private readonly IConexao _conexao;

        //Construtor da classe personagemController
        public PersonagemController(IConexao conexao)
        {
            _conexao = conexao;
        }

        [HttpGet("Personagem/Listar")]
        public IActionResult Index()
        {
            using (var conn = _conexao.AbrirConexao())
            {
                string sql = "SELECT * FROM personagem";
                listPersonagem = conn.Query<PersonagemViewModel>(sql).ToList();

            }

            return View(listPersonagem);
        }

        public IActionResult ModalEdit()
        {
            return View();
        }

        [HttpGet("Personagem/Editar/{id}")]
        public IActionResult Edit(int id)
        {
            PersonagemViewModel personagem;
            using (var conn = _conexao.AbrirConexao())
            {
                var sql = $"SELECT * FROM personagem WHERE Id = {id};";
                personagem = conn.QueryFirst<PersonagemViewModel>(sql);
            }
            return View(personagem);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost("Personagem/Salvar")]
        public IActionResult Post([FromForm] PersonagemViewModel model)
        {
            string sql = "";
            if (model.Id != 0)
            {
                sql = @"UPDATE personagem SET
                        nivel = @nivel,
                        agilidade = @agilidade,
                        ataque = @ataque,
                        carisma = @carisma,
                        defesa = @defesa,
                        destreza = @destreza,
                        inteligencia = @inteligencia,
                        sorte = @sorte,
                        vida = @vida
                        where Id = @Id";
            }
            else
            {
                sql = "INSERT INTO personagem(nome, raca, classe, nivel, agilidade, ataque, carisma, defesa, destreza, inteligencia, sorte, vida) VALUES(@nome, @raca, @classe, @nivel, @agilidade, @ataque, @carisma, @defesa, @destreza, @inteligencia, @sorte, @vida);";
            }

            using (var conn = _conexao.AbrirConexao())
            {
                conn.Execute(sql, model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using (var conn = _conexao.AbrirConexao())
            {
                var sql = $"DELETE FROM personagem WHERE Id = {id};";
                conn.Execute(sql);
            }
            return RedirectToAction("Index");
        }
    }
}