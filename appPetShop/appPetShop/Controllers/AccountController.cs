using appPetShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace appPetShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbPetShopContext _context;

        public AccountController(DbPetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "ReservaServico");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // 1. Busca na nova tabela de Usuários (em vez de Clientes)
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Senha == model.Senha);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Utilizador ou senha incorretos.");
                return View(model);
            }

            // 2. Cria as credenciais (Claims)
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, usuario.Codigo.ToString()),
        new Claim(ClaimTypes.Name, usuario.Username)
    };

            // 3. O nome do esquema deve ser IGUAL ao que colocou no Program.cs
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");

            await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));

            // 4. Redireciona para a página principal após o login
            return RedirectToAction("Index", "ReservaServico");
        }

        public async Task<IActionResult> Logout()
        {
            // Também precisa de usar o mesmo nome aqui
            await HttpContext.SignOutAsync("CookieAuthentication");
            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous] // Permite acessar sem estar logado
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Salva na tabela Usuario
                var novoUsuario = new Usuario
                {
                    Username = model.Username,
                    Senha = model.Senha // Lembrando que no mundo real, você deve fazer o Hash disso!
                };
                _context.Usuarios.Add(novoUsuario);

                // 2. Salva na tabela Cliente
                var novoCliente = new Cliente
                {
                    Nome = model.Nome,
                    Cpf = model.Cpf,
                    Telefone = model.Telefone
                };
                _context.Clientes.Add(novoCliente);

                // 3. Efetiva as mudanças no banco de dados
                await _context.SaveChangesAsync();

                // 4. Redireciona o usuário para fazer login com a conta recém-criada
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }
    }
}