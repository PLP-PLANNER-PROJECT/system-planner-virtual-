using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Planner.Service;

namespace Planner.Controllers
{
    //controller  = Usuario
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        // Injetar o serviço de usuário
        private readonly UsuarioService _usuarioService;

        // Construtor do controlador
        public UsuarioController(UsuarioService usuarioService)
        {
            // Inicializar o serviço de usuário
            _usuarioService = usuarioService;
        }

        // Método para obter um usuário pelo ID, Task: Representa uma operação assíncrona que retorna um resultado
        // A função vai retornar um tipo assincrono, que é um ActionResult do tipo Usuario, eventualmente reusltara
        // em uma respota HTTP
        //ActionResult: Representa o resultado de uma ação de um método de um controlador
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            // await: espera a conclusão da tarefa assíncrona (GetUsuarioByIdAsync) e atribui o resultado(obj usuario or null)
            // a variável usuario
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            // Retorna um objeto do tipo OkObjectResult com o objeto usuario
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> AddUsuario(Usuario usuario)
        {
            // Adiciona um novo usuário
            await _usuarioService.AddUsuarioAsync(usuario);

            // CreatedAtAction: Retorna um objeto do tipo CreatedAtActionResult que representa uma resposta HTTP 201
            // CreatedAtAction(string actionName, object routeValues, object value), inclui no cabeçalho da resposta a URL e o objeto
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }
    }
}
