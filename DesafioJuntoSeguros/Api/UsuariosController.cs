using DesafioJuntoSeguros.Application.DTOs;
using DesafioJuntoSeguros.Application.ViewModels;
using DesafioJuntoSeguros.Domain.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioJuntoSeguros.Api
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<UsuarioViewModel> Add(AddUsuarioDto usuario)
        {
            return Ok(_service.AddUsuario(usuario));
        }
        [HttpGet]
        public ActionResult<List<UsuarioViewModel>> Get()
        {
            return Ok(_service.Get());
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioViewModel> GetById(Guid id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return NotFound("Student not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            return _service.Remove(id);
        }

        [HttpPut("{id}")]
        public ActionResult<UsuarioViewModel> Update(Guid id, UpdateUsuarioDto usuario)
        {
            return Ok(_service.Update(id, usuario));
        }
    }
}