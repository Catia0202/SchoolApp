﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class TurmasController : Controller
    {
        private readonly ITurmasRepository _turmasRepository;
        private readonly IImageHelper _imagehelper;
        private readonly IUserHelper _userHelper;
        private readonly ICursoRepository _cursoRepository;

        public TurmasController(ITurmasRepository turmasRepository, IImageHelper imagehelper,IUserHelper userHelper, ICursoRepository cursoRepository)
        {
            _turmasRepository = turmasRepository;
            _imagehelper = imagehelper;
            _userHelper = userHelper;
            _cursoRepository = cursoRepository;
        }

        [Authorize(Roles = "Funcionario")]
        public IActionResult Index()
        {
            var turmas = _turmasRepository.GetAll().Include(p=>p.Curso);
            
            return View(turmas);
        }

      

        [Authorize(Roles = "Funcionario")]
        public IActionResult Create()
        {
            var model = new TurmaViewModel
            {
                Cursos = _cursoRepository.GetComboCursos()
            };
            return View(model);
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurmaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;


               
                 if(model.DataInicio.Date > model.DataFim.Date)
                {
                    ViewBag.message= "A data do Fim do curso deve ser depois da data do Início do mesmo";
                    model.Cursos = _cursoRepository.GetComboCursos();
                    return View(model);
                }
                    var turma = new Turma
                    {
                        Nome = model.Nome,
                        CursoId = model.CursoId,
                        DataFim = model.DataFim,
                        DataInicio = model.DataInicio
                        
                    };
                try
                {
                    await _turmasRepository.CreateAsync(turma);
                    ViewBag.message = "Turma foi criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.message = "Ocurreu um Erro ";
                    model.Cursos = _cursoRepository.GetComboCursos();
                    return View(model); 
                }
                    
                
            }
            model.Cursos = _cursoRepository.GetComboCursos();
            return View(model);
        }



        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.TituloErro = "Erro ao encontrar turma";
                ViewBag.MensagemErro = "Ocurreu um erro ao encontrar a respectiva turma";
                return View("Error");
            }

            var turma = await _turmasRepository.GetByIdAsync(id.Value);
            if (turma == null)
            {
                return NotFound(); //VIEW ERRO
            }
            var model = new TurmaViewModel
            {
                Id = turma.Id,
                Nome = turma.Nome,
                CursoId = turma.CursoId,
                Cursos = _cursoRepository.GetComboCursos(),
                DataFim = turma.DataFim,
                DataInicio = turma.DataInicio

            };

            return View(model);
        }

        // POST: Turmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TurmaViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                if (model.DataInicio.Date > model.DataFim.Date)
                {
                    ViewBag.message = "A data do Fim do curso deve ser depois da data do Início do mesmo";
                    model.Cursos = _cursoRepository.GetComboCursos();
                    return View(model);
                }
                var turma = await _turmasRepository.GetByIdAsync(model.Id);

                turma.Nome = model.Nome;
                turma.CursoId = model.CursoId;
                turma.DataFim = model.DataFim;
                turma.DataInicio = model.DataInicio;

                try
                {
                    await _turmasRepository.UpdateAsync(turma);
                    model.Cursos = _cursoRepository.GetComboCursos();
                    ViewBag.message = "Turma foi atualizada com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewBag.message = "Ocurreu um Erro ";
                    model.Cursos = _cursoRepository.GetComboCursos();
                    return View(model); //view erro
                }
            
            }
            model.Cursos = _cursoRepository.GetComboCursos();
            return View(model);
        }

        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var turma = await _turmasRepository.GetByIdAsync(id.Value);
            turma.Curso = await _cursoRepository.GetAll().Where(p => p.Id == turma.CursoId).FirstOrDefaultAsync();
            if (turma == null)
            {
                return View("Error");
            }
            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _turmasRepository.GetByIdAsync(id);
            turma.Curso = await _cursoRepository.GetAll().Where(p => p.Id == turma.CursoId).FirstOrDefaultAsync();

            try
            {
                await _turmasRepository.DeleteAsync(turma);
                ViewBag.errormessage = "Turma deletada com sucesso!";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {

                    ViewBag.TituloErro = "Erro ao deletar turma";
                    ViewBag.MensagemErro = "Não foi possível deletar esta turma pois a mesma está a ser utilizada";
                    return View("Error");
                }

                return View(turma);
            }
            

            return RedirectToAction(nameof(Index));
        }

  
    }
}
