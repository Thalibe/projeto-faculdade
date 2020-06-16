using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ficha_criacao_personagem_rpg_csharp.Models;
using ficha_criacao_personagem_rpg_csharp.Repositorio;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ficha_criacao_personagem_rpg_csharp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConexao _conexao;
        public LoginController(IConexao conexao)
        {
            _conexao = conexao;
        }

        [HttpGet]
        public IActionResult UsuarioLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UsuarioLogin([FromForm] UsuarioViewModel model)
        {
            UsuarioViewModel usuario = null;

            using (var conn = _conexao.AbrirConexao())
            {
                string queryQuery = $"SELECT * FROM user WHERE login = '{model.Login}' AND senha = '{model.Senha}';";
                usuario = conn.QueryFirst<UsuarioViewModel>(queryQuery);
            }

            if (usuario != null)
            {
                var userClaims = new List<Claim>()
                {
                    //define o cookie
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email,usuario.Email)
                };

                var minhaIdentidade = new ClaimsIdentity(userClaims, "Usuario");

                var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentidade });

                //criar cookie
                await HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Credenciais inválidas...";

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}