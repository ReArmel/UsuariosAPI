﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Database.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signinManager;
        private TokenService _tokenService;

        public UsuarioService(UserManager<Usuario> userManager, IMapper mapper, SignInManager<Usuario> signinManager, TokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signinManager = signinManager;
            _tokenService = tokenService;
        }

        public async Task Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário");
            }
        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var resultado = await _signinManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
            
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var usuario = _signinManager
                .UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}