﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using SchoolApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Controllers
{
    public class CursosController : Controller
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IImageHelper _imagehelper;

        public CursosController(ICursoRepository cursoRepository, IImageHelper imagehelper)
        {
            _cursoRepository = cursoRepository;
            _imagehelper = imagehelper;
        }
        [Authorize(Roles="Admin")]
        public IActionResult Index()
        {
            return View (_cursoRepository.GetAll().OrderBy(p=>p.Nome));
        }

        public IActionResult Create()
        {
            return View();
        }
      
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CursoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imagehelper.UploadImageAsync(model.ImageFile, "cursos");
                }
                var curso = new Curso
                {
                    Nome = model.Nome,
                    Duracao = model.Duracao,
                    Fotourl = path,
                    Descricao =model.Descricao

                };
                try
                {
                    
                    await _cursoRepository.CreateAsync(curso);
                    ViewBag.message = "Curso criado com sucesso";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {

                    ViewBag.TituloErro = "Erro ao Criar Curso";
                    ViewBag.MensagemErro = "Ocurreu um erro ao criar o seu curso";
                    return View("Error");
                }
            
            }
            return View(model);
        }

  
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var curso = await _cursoRepository.GetByIdAsync(id.Value);
            if (curso == null)
            {
                return NotFound();
            }

            var model = new CursoViewModel
            {
                Id = curso.Id,
                Duracao = curso.Duracao,
                Fotourl = curso.Fotourl,
                Nome = curso.Nome,
                Descricao = curso.Descricao
            };
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CursoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.Fotourl;
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imagehelper.UploadImageAsync(model.ImageFile, "cursos");
                }
                var curso = await _cursoRepository.GetByIdAsync(model.Id);
                curso.Nome = model.Nome;
                curso.Duracao = model.Duracao;
                curso.Fotourl = path;
                curso.Descricao = model.Descricao;
                
                try
                {
                    await _cursoRepository.UpdateAsync(curso);
                    ViewBag.message = "Curso foi atualizado com sucesso!";
                    return RedirectToAction("Index","Cursos");
                }
                catch 
                {
                    ViewBag.TituloErro = "Erro ao Atualizar o seu Curso";
                    ViewBag.MensagemErro = "Ocurreu um erro ao atualizar o seu Curso";
                    return View("Error");
                }
                
            }

            return View(model);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _cursoRepository.GetByIdAsync(id.Value);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var curso= await _cursoRepository.GetByIdAsync(id);
            try
            {
                await _cursoRepository.DeleteAsync(curso);
                ViewBag.message = "Curso deletados sucesso!";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.TituloErro = "Erro ao apagar o seu Curso";
                    ViewBag.MensagemErro = "Não pode apagar o curso pois o mesmo está a ser utilizado";
                    return View("Error");
                }
                ViewBag.TituloErro = "Erro ao apagar o seu Curso";
                ViewBag.MensagemErro = "Não pode apagar o curso pois o mesmo está a ser utilizado";
                return View("Error");

            }


            return RedirectToAction(nameof(Index));


        }


    }
}
