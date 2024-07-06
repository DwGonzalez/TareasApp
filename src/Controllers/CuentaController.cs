using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasApp.Data.DTO.Cuenta;
using TareasApp.Entities;
using TareasApp.Service;

namespace TareasApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Usuario> _signInManager;

        public CuentaController(UserManager<Usuario> userManager, ITokenService tokenService, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Registro de usuario
        /// </summary>
        /// <param name="registerDto">Datos de usuario a registrar</param>
        /// <returns></returns>
        [HttpPost("registro")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUsuario = new Usuario
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                };

                var createdUser = await _userManager.CreateAsync(appUsuario, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUsuario, "Usuario");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDTO
                            {
                                UserName = appUsuario.UserName,
                                Email = appUsuario.Email,
                                Token = _tokenService.CreateToken(appUsuario)
                            });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, 0);
            }
        }

        /// <summary>
        /// Inicio de sesión
        /// </summary>
        /// <param name="loginDto">Datos de usuario para iniciar sesión</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUsuario = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (appUsuario == null) return Unauthorized("Usuario Invalido!");

            var result = await _signInManager.CheckPasswordSignInAsync(appUsuario, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Usuario no encontrado y/o contraseña incorrecta");

            return Ok(
                new NewUserDTO
                {
                    UserName = appUsuario.UserName,
                    Email = appUsuario.Email,
                    Token = _tokenService.CreateToken(appUsuario)
                }
            );
        }
    }
}
